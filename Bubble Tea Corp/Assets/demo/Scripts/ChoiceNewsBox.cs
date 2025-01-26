using System;
using TMPro;
using UnityEngine;

public class ChoiceNewsBox : MonoBehaviour
{
    public TMP_Text tmpText;
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void AddNews(NewsEvent newsEvent)
    {
        if (newsEvent == null || string.IsNullOrEmpty(newsEvent.newsText))
        {
            panel.SetActive(false);
            return;
        }

        Debug.Log("Displaying news: " + newsEvent.newsText);

        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
            tmpText.text = newsEvent.newsText;
        }
    }
}
