using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class NewsTicker : MonoBehaviour
{
    public TickerITem tickerItemPrefab;
    public float itemDuration = 3.0f;
    public List<NewsEvent> newsEvents;

    float width;
    float pixelPerSecond;
    TickerITem currentItem;

    float speed = 4.0f; //less is faster

    private void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelPerSecond = width / itemDuration / speed;
    }

    private void Update()
    {
        if (newsEvents.Count > 0)
        {
            if (currentItem == null || currentItem.GetXPosition <= -currentItem.GetWidth)
            {
                string newsText = newsEvents[0].newsText;
                string newsTitle = "News item: " + newsEvents[0].newsTitle;
                newsEvents.RemoveAt(0);
                AddTickerItem(newsText, newsTitle);
            }
        }
    }

    public void AddNewItemToTicker(NewsEvent newsEvent, int howMany)
    {

        Debug.Log("Adding " + howMany + " news events to the ticker");
        //newsEvents.Add(newsEvent)
        //if (newsEvents.Count<=1){
            //NewsEvent news = new NewsEvent();
            //news.newsTitle = "breaking news";
            //newsEvents.
        //}
        // if (howMany < 1)
        // {
        //     howMany = 2;
        // }

        // // Add X number of news events based on the howMany parameter, but every time in between existing news events
        // for (int i = 0; i < howMany; i++)
        // {
        //     int insertIndex = Random.Range(0, newsEvents.Count + 1);
        //     newsEvents.Insert(insertIndex, newsEvent);
        // }

        // Shuffle the newsEvents list to ensure randomness
        ShuffleList(newsEvents);
    }

    // Fisher-Yates shuffle algorithm for better randomness
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void AddTickerItem(string message, string title)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.name = title;
        currentItem.Initialize(width, pixelPerSecond, message);
    }
}
