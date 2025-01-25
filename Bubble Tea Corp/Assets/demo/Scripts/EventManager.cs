using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Event
{
    public string name;
    public float weight;

    public int eventType;

    // This dictoanary will store the weights it will give to other events, the more event weight an event has the more likely it is to happen
    [SerializeField] public List<WeightToGive> weightToGive;

    // This dictionary will store the stock changes that will happen when this event happens
    public List<Event> stockToChange;

    public List<Traits> traitsToChange;

    // Will be used for the news when this event happens.
    public NewsEvent newsEvent;

}
[System.Serializable]
public class WeightToGive
{
    public string name;
    public float weight;    
}

[System.Serializable]
public class Traits
{
    public string name;
    public float value;

    public void SetTrait(float value)
    {
        this.value += value;
        //Debug.Log("trait: " + value);
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
    private float elapsedTimeChoice;
    private float intervalChoice = 5.0f;
    public bool pauseWorldEvents = false;

    public float choiceTime = 10.0f;
    public float choiceTimeElapsed;

    public GameObject eventHappendPrefab;
    public Transform eventHappendPrefabParent;
    public GameObject choiceButton;
    public Transform choiceButtonParent;
    public GameObject choicePanel;

    public List<Event> events;

    public List<Event> eventsThatHappened = new List<Event>();
    public List<Event> choicesMade = new List<Event>();
    public List<Traits> traits = new List<Traits>();

    private void Start()
    {
        EventData eventData = new EventData();
        eventData.FIllList();
        events = eventData.events;
    }


    void Update()
    {
        if (!pauseWorldEvents)
        {
            // Increment elapsed time by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Check if the interval has passed
            if (elapsedTime >= interval)
            {
                // Reset elapsed time
                elapsedTime = 0f;

                // Trigger a random event from the list
                TriggerWorldEvent();
            }

        }

        if (choiceTimeElapsed <= 0)
        {
            elapsedTimeChoice += Time.deltaTime;
            if (elapsedTimeChoice >= intervalChoice)
            {
                elapsedTimeChoice = 0f;

                TriggerChoice();
            }
        }

        if (choiceTimeElapsed > 0)
        {
            choiceTimeElapsed -= Time.deltaTime;
        }
        else
        {
            CloseChoicePanel();
        }
    }

    private void TriggerChoice()
    {
        pauseWorldEvents = true;

        choiceTimeElapsed = choiceTime;
        choicePanel.SetActive(true);

        List<Event> choices = new List<Event>();
        events.Sort((a, b) => b.weight.CompareTo(a.weight));

        foreach (Event e in events)
        {
            if (e.eventType == 1 && e.weight > 0)
            {
                choices.Add(e);
            }

            if (choices.Count >= 5)
            {
                break;
            }
        }

        // Choose 3 random events from the list
        List<Event> randomChoices = new List<Event>();
        for (int i = 0; i < 3; i++)
        {
            randomChoices.Add(choices[UnityEngine.Random.Range(0, choices.Count)]);
        }

        foreach (Event e in randomChoices)
        {
            // Add choicebutton to the choice panel
            GameObject choiceButtonInstance = Instantiate(choiceButton, choiceButtonParent);
            choiceButtonInstance.GetComponentInChildren<TMP_Text>().text = e.name;
            choiceButtonInstance.GetComponent<Button>().onClick.AddListener(() => MakeChoice(e));
        }
    }

    public void MakeChoice(Event choice)
    {
        ProcessEvent(choice);
        eventsThatHappened.Add(choice);
        choicesMade.Add(choice);
        events.Remove(choice);
        CloseChoicePanel();
    }

    public void CloseChoicePanel()
    {
        pauseWorldEvents = false;

        // destroy all child in the choice panel
        for (int i = 0; i < choiceButtonParent.childCount; i++)
        {
            Destroy(choiceButtonParent.GetChild(i).gameObject);
        }

        choicePanel.SetActive(false);
    }

    private void TriggerWorldEvent()
    {
        if (events.Count > 0)
        {
            List<Event> worldEvents = new List<Event>();
            events.Sort((a, b) => b.weight.CompareTo(a.weight));

            foreach (Event e in events)
            {
                if (e.eventType == 0 && e.weight > 0)
                {
                    worldEvents.Add(e);
                }
            }

            // Choose a random event from the list
            //int randomIndex = UnityEngine.Random.Range(0, events.Count);
            //Event chosenEvent = events[randomIndex];

            Event chosenEvent = worldEvents[UnityEngine.Random.Range(0, 10)];
            ProcessEvent(chosenEvent);

            // Add the chosen event to the list of events that happened
            eventsThatHappened.Add(chosenEvent);
            events.Remove(chosenEvent);

            GameObject eventHappendLog = Instantiate(eventHappendPrefab, eventHappendPrefabParent);
            eventHappendLog.GetComponentInChildren<TMP_Text>().text = chosenEvent.name;
            if (eventHappendPrefabParent.childCount > 10)
            {
                Destroy(eventHappendPrefabParent.GetChild(0).gameObject);
            }


            // Trigger the chosen event (for now, just log it)
            //Debug.Log($"Event triggered: {chosenEvent.name}");
        }
    }

    public void ProcessEvent(Event chosenEvent)
    {
        // Update the Stock values based on the chosen event
        foreach (Event stock in chosenEvent.stockToChange)
        {
            // Get all the stocks that are in the game
            // Update the values of the stocks that the event will invluence

        }

        // Update the events weights based on the chosen event
        foreach (WeightToGive weightToGive in chosenEvent.weightToGive)
        {
            foreach (Event e in events)
            {
                if (e.name == weightToGive.name)
                {
                    e.weight += weightToGive.weight;
                    //Debug.Log("Event name: " + weightToGive.name);
                    //Debug.Log("New Event weight: " + weightToGive.weight);
                }
            }
        }

        // Update the Trait values based on the chosen event
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
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
