using UnityEngine;
using System.Collections.Generic;

public class ModelRotate : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject model;

    [Header("Pivot")]
    [SerializeField] private GameObject RotatePivot;

    [Header("Circle Settings")]
    [SerializeField] private float radius = 5f;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed = 5f;

    [Header("Scale")]
    [SerializeField] private float selectedScale = 1.5f;
    [SerializeField] private float scaleSpeed = 5f;

    // 生成数
    private int randomValue;

    // 生成モデル保存
    private List<GameObject> models = new List<GameObject>();

    // 現在選択
    private int currentIndex = 0;

    // 目標角度
    private float targetAngle = 0f;

    void Start()
    {
        // ランダム生成数
        randomValue = Random.Range(3, 7);

        // 均等角度
        float angleStep = 360f / randomValue;

        // 惑星生成
        for (int i = 0; i < randomValue; i++)
        {
            float angle = angleStep * i;

            // ラジアン変換
            float rad = angle * Mathf.Deg2Rad;

            // 円形配置
            Vector3 pos = new Vector3(
                Mathf.Sin(rad) * radius,
                0,
                Mathf.Cos(rad) * radius
            );

            // 生成
            GameObject obj = Instantiate(
                model,
                RotatePivot.transform
            );

            obj.transform.localPosition = pos;

            // 中心を見る
            obj.transform.LookAt(RotatePivot.transform.position);

            models.Add(obj);
        }

        // 初期角度設定
        SetTargetAngle();

        // 初期回転反映
        RotatePivot.transform.rotation =
            Quaternion.Euler(0, targetAngle, 0);
    }

    void Update()
    {
        //--------------------------------
        // 右キー
        //--------------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentIndex++;

            if (currentIndex >= randomValue)
            {
                currentIndex = 0;
            }

            SetTargetAngle();
        }

        //--------------------------------
        // 左キー
        //--------------------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentIndex--;

            if (currentIndex < 0)
            {
                currentIndex = randomValue - 1;
            }

            SetTargetAngle();
        }

        //--------------------------------
        // なめらか回転
        //--------------------------------
        Quaternion targetRot =
            Quaternion.Euler(0, targetAngle, 0);

        RotatePivot.transform.rotation =
            Quaternion.Slerp(
                RotatePivot.transform.rotation,
                targetRot,
                rotateSpeed * Time.deltaTime
            );

        //--------------------------------
        // 選択中拡大
        //--------------------------------
        for (int i = 0; i < models.Count; i++)
        {
            Vector3 targetScale;

            if (i == currentIndex)
            {
                targetScale = Vector3.one * selectedScale;
            }
            else
            {
                targetScale = Vector3.one;
            }

            models[i].transform.localScale =
                Vector3.Lerp(
                    models[i].transform.localScale,
                    targetScale,
                    scaleSpeed * Time.deltaTime
                );
        }
    }

    //--------------------------------
    // 選択角度設定
    //--------------------------------
    void SetTargetAngle()
    {
        float angleStep = 360f / randomValue;

        float offset = 0f;

        // 奇数なら半分ずらす
        if (randomValue % 2 != 0)
        {
            offset = angleStep / 2f;
        }

        targetAngle = -angleStep * currentIndex + offset;
    }
}