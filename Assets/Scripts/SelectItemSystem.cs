using UnityEngine;

public class CarouselManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] RectTransform[] items;

    [Header("Layout")]
    [SerializeField] float spacing = 300f;
    [SerializeField] float yPosition = 0f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Scale")]
    [SerializeField] float normalScale = 1f;
    [SerializeField] float selectedScale = 1.3f;
    [SerializeField] float scaleSpeed = 10f;

    float targetOffset = 0f;
    float currentOffset = 0f;

    float totalWidth;

    // 元サイズ保存
    Vector2[] baseSize;

    void Start()
    {
        totalWidth = items.Length * spacing;

        // 元サイズ配列作成
        baseSize = new Vector2[items.Length];

        // 中央配置
        float startX = -((items.Length - 1) * spacing) / 2f;

        // 偶数補正
        if (items.Length % 2 == 0)
            startX += spacing / 2f;

        for (int i = 0; i < items.Length; i++)
        {
            // 初期X座標
            float x = startX + i * spacing;

            // 配置
            items[i].anchoredPosition =
                new Vector2(x, yPosition);

            // 元サイズ保存
            baseSize[i] = items[i].sizeDelta;
        }
    }

    void Update()
    {
        InputMove();
        MoveItems();
        UpdateScale();
    }

    void InputMove()
    {
        // 右
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetOffset -= spacing;
        }

        // 左
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetOffset += spacing;
        }
    }

    void MoveItems()
    {
        // スムーズ移動
        currentOffset = Mathf.Lerp(
            currentOffset,
            targetOffset,
            Time.deltaTime * moveSpeed
        );

        float half = totalWidth / 2f;

        for (int i = 0; i < items.Length; i++)
        {
            // 基本位置
            float x = (i * spacing) + currentOffset;

            // 無限ループ
            x = Mathf.Repeat(
                x + half,
                totalWidth
            ) - half;

            items[i].anchoredPosition =
                new Vector2(x, yPosition);
        }
    }

    void UpdateScale()
    {
        int closestIndex = 0;

        float closestDist = float.MaxValue;

        // 中央に一番近いUI探す
        for (int i = 0; i < items.Length; i++)
        {
            float dist =
                Mathf.Abs(items[i].anchoredPosition.x);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        // サイズ変更
        for (int i = 0; i < items.Length; i++)
        {
            // 選択中だけ倍率変更
            float targetScale =
                (i == closestIndex)
                ? selectedScale
                : normalScale;

            // 元サイズ基準で拡大
            Vector2 targetSize =
                baseSize[i] * targetScale;

            // スムーズサイズ変更
            items[i].sizeDelta = Vector2.Lerp(
                items[i].sizeDelta,
                targetSize,
                Time.deltaTime * scaleSpeed
            );
        }
    }
}