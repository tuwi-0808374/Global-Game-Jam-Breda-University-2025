using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceNewsBox : MonoBehaviour
{
    public TMP_Text tmpText;
    public GameObject panel;
    public Image newsImage;

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

        newsImage.enabled = false;
        if (newsEvent.image != null)
        {
            newsImage = newsEvent.image;
            newsImage.enabled = true;
        }

        Debug.Log("Displaying news: " + newsEvent.newsText);

        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
            tmpText.text = newsEvent.newsText;
        }
    }
}
