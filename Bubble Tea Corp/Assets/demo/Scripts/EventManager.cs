using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Event
{
    public string name;

    public int eventType;

    // This dictoanary will store the weights it will give to other events, the more event weight an event has the more likely it is to happen
    [SerializeField] public List<Event> eventWeights;

    // This dictionary will store the stock changes that will happen when this event happens
    public List<Event> stockToChange;

    public List<Traits> traitsToChange;

    // Will be used for the news when this event happens.
    public NewsEvent newsEvent;

}

[System.Serializable]
public class Traits
{
    public string name;
    public float value;

    public void SetTrait(float value)
    {
        this.value += value;
        Debug.Log("trait: " + value);
    }
}

[System.Serializable]
public class NewsEvent
{
    public string newsTitle;
    public string newsText;
}

[System.Serializable]
public class EventManager : MonoBehaviour
{
    private float elapsedTime;
    private float interval = 1.0f;

    public List<Event> events;


    public List<Event> eventsThatHappened = new List<Event>();
    public List<Event> choicesMade = new List<Event>();
    public List<Traits> traits = new List<Traits>();

    void Update()
    {
        // Increment elapsed time by the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // Check if the interval has passed
        if (elapsedTime >= interval)
        {
            // Reset elapsed time
            elapsedTime = 0f;

            // Trigger a random event from the list
            TriggerRandomEvent();
        }
    }

    private void TriggerRandomEvent()
    {
        if (events.Count > 0)
        {
            // Choose a random event from the list
            int randomIndex = UnityEngine.Random.Range(0, events.Count);
            Event chosenEvent = events[randomIndex];

            // Add the chosen event to the list of events that happened
            eventsThatHappened.Add(chosenEvent);

            foreach (Traits trait in chosenEvent.traitsToChange)
            {
                foreach (Traits t in traits)
                {
                    if (t.name == trait.name)
                    {
                        t.SetTrait(trait.value);
                    }
                }
            }

            // Trigger the chosen event (for now, just log it)
            Debug.Log($"Event triggered: {chosenEvent.name}");
        }
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
