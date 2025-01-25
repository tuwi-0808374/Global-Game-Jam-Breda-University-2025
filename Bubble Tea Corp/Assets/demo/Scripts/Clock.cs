using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public TMP_Text timeText;
    public EventManager eventManager;
    private float elapsedTime;

    private void Update()
    {
        if (timeText == null || eventManager == null)
        {
            Debug.LogError("timeText or eventManager is not assigned.");
            return;
        }

        // Increment elapsed time by the time passed since the last frame
        elapsedTime += eventManager.GetElapsedTime();

        // Get the current date in real life
        System.DateTime currentTime = System.DateTime.Now;

        // Add the elapsed time to the current date, multiplied to make the time go faster
        currentTime = currentTime.AddSeconds(elapsedTime * 60);

        // Set the text to the current date and time
        timeText.text = currentTime.ToString("HH:mm");
        timeText.text += " " + currentTime.DayOfWeek;
    }
}
