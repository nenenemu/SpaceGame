using UnityEngine;

public class CarouselManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    RectTransform[] items;

    [Header("Layout")]
    [SerializeField]
    float spacing = 300f;

    [SerializeField]
    float yPosition = 0f;

    [Header("Loop")]
    [SerializeField]
    float loopX = 1000f;

    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 10f;

    [SerializeField]
    float scrollSpeed = 300f;

    [Header("Scale")]
    [SerializeField]
    float selectedScale = 1.3f;

    [SerializeField]
    float normalScale = 1f;

    float startX;

    void Start()
    {
        // 全体幅
        float totalWidth = (items.Length - 1) * spacing;

        // 中央基準
        startX = -totalWidth / 2f;

        // ★偶数補正（ここが重要）
        if (items.Length % 2 == 0)
        {
            startX += spacing / 2f;
        }

        // 初期配置
        for (int i = 0; i < items.Length; i++)
        {
            float x = startX + i * spacing;
            items[i].anchoredPosition = new Vector2(x, yPosition);
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
        float dir = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
            dir = -1f;

        if (Input.GetKey(KeyCode.LeftArrow))
            dir = 1f;

        for (int i = 0; i < items.Length; i++)
        {
            Vector2 pos = items[i].anchoredPosition;

            pos.x += dir * scrollSpeed * Time.deltaTime;

            items[i].anchoredPosition = pos;
        }
    }

    void MoveItems()
    {
        float totalWidth = spacing * items.Length;

        for (int i = 0; i < items.Length; i++)
        {
            Vector2 pos = items[i].anchoredPosition;

            float targetX = pos.x;

            // なめらか補間（軽い追従）
            pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * moveSpeed);

            // 右に行きすぎたら左へ
            if (pos.x > loopX)
            {
                pos.x -= totalWidth;
            }

            // 左に行きすぎたら右へ
            if (pos.x < -loopX)
            {
                pos.x += totalWidth;
            }

            pos.y = yPosition;

            items[i].anchoredPosition = pos;
        }
    }

    void UpdateScale()
    {
        // 一番中央に近いやつを選択扱い
        int closestIndex = 0;
        float closestDist = float.MaxValue;

        for (int i = 0; i < items.Length; i++)
        {
            float dist = Mathf.Abs(items[i].anchoredPosition.x);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            float target = (i == closestIndex)
                ? selectedScale
                : normalScale;

            items[i].localScale = Vector3.Lerp(
                items[i].localScale,
                Vector3.one * target,
                Time.deltaTime * moveSpeed
            );
        }
    }
}