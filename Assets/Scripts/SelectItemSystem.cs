using UnityEngine;

public class CarouselManager : MonoBehaviour
{
    [SerializeField] RectTransform[] items;

    [SerializeField] float spacing = 300f;
    [SerializeField] float yPosition = 0f;

    [SerializeField] float moveSpeed = 10f;

    [Header("Scale")]
    [SerializeField] float normalScale = 1f;
    [SerializeField] float selectedScale = 1.3f;
    [SerializeField] float scaleSpeed = 10f;

    float targetOffset = 0f;
    float currentOffset = 0f;

    float totalWidth;

    void Start()
    {
        totalWidth = items.Length * spacing;

        float startX = -((items.Length - 1) * spacing) / 2f;

        if (items.Length % 2 == 0)
            startX += spacing / 2f;

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
        if (Input.GetKeyDown(KeyCode.RightArrow))
            targetOffset -= spacing;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            targetOffset += spacing;
    }

    void MoveItems()
    {
        currentOffset = Mathf.Lerp(
            currentOffset,
            targetOffset,
            Time.deltaTime * moveSpeed
        );

        float half = totalWidth / 2f;

        for (int i = 0; i < items.Length; i++)
        {
            float x = (i * spacing + currentOffset);

            x = Mathf.Repeat(x + half, totalWidth) - half;

            items[i].anchoredPosition = new Vector2(x, yPosition);
        }
    }

    void UpdateScale()
    {
        int closestIndex = 0;
        float closestDist = float.MaxValue;

        // 中央に一番近いやつを探す
        for (int i = 0; i < items.Length; i++)
        {
            float dist = Mathf.Abs(items[i].anchoredPosition.x);

            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        // スケール適用
        for (int i = 0; i < items.Length; i++)
        {
            float target = (i == closestIndex)
                ? selectedScale
                : normalScale;

            items[i].localScale = Vector3.Lerp(
                items[i].localScale,
                Vector3.one * target,
                Time.deltaTime * scaleSpeed
            );
        }
    }
}