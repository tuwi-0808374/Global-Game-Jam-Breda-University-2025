using TMPro;
using UnityEngine;

public class TickerITem : MonoBehaviour
{
    float tickerWidth;
    float pixelPerSecond;
    RectTransform rt;
    TMP_Text tmpText;

    public float GetXPosition { get { return rt.anchoredPosition.x; } }
    public float GetWidth { get { return tmpText.GetPreferredValues().x; } }

    public void Initialize(float tickerWidth, float pixelPerSecond, string message)
    {
        this.tickerWidth = tickerWidth;
        this.pixelPerSecond = pixelPerSecond;
        rt = GetComponent<RectTransform>();
        tmpText = GetComponentInChildren<TMP_Text>();
        tmpText.text = message;
    }

    private void Update()
    {
        rt.position += Vector3.left * pixelPerSecond * Time.deltaTime;
        if (GetXPosition <= 0 - tickerWidth - GetWidth)
        {
            Destroy(gameObject);
        }
    }
}
