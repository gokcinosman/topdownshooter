using UnityEngine;

public class CrosshairAnimation : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform crosshairContainer;
    public RectTransform topPart;
    public RectTransform bottomPart;
    public RectTransform leftPart;
    public RectTransform rightPart;

    public float animationSpeed = 2f;
    public float maxDistance = 20f;
    public float minDistance = 10f;

    private Vector2 topInitialPosition;
    private Vector2 bottomInitialPosition;
    private Vector2 leftInitialPosition;
    private Vector2 rightInitialPosition;

    private void Start()
    {
        Cursor.visible = false;
        // Başlangıç pozisyonlarını kaydet
        topInitialPosition = topPart.anchoredPosition;
        bottomInitialPosition = bottomPart.anchoredPosition;
        leftInitialPosition = leftPart.anchoredPosition;
        rightInitialPosition = rightPart.anchoredPosition;
    }

    private void Update()
    {
        // Mouse pozisyonunu Canvas koordinatlarına dönüştür
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out mousePosition);

        // Crosshair'i mouse pozisyonuna taşı
        crosshairContainer.anchoredPosition = mousePosition;

        // Sinüs dalgası kullanarak animasyon yap
        float wave = (Mathf.Sin(Time.time * animationSpeed) + 1) * 0.5f;
        float currentDistance = Mathf.Lerp(minDistance, maxDistance, wave);

        // Her parçayı hareket ettir
        topPart.anchoredPosition = topInitialPosition + Vector2.up * currentDistance;
        bottomPart.anchoredPosition = bottomInitialPosition + Vector2.down * currentDistance;
        leftPart.anchoredPosition = leftInitialPosition + Vector2.left * currentDistance;
        rightPart.anchoredPosition = rightInitialPosition + Vector2.right * currentDistance;
    }
}