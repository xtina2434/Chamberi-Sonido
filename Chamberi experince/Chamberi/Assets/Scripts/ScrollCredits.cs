using UnityEngine;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed = 20f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}