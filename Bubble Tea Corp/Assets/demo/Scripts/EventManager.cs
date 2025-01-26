using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using static StockMarket;

[System.Serializable]
public class Choice
{
    public string ChoiceIdentifier;
    public Image image;
    public float weight;
    public List<Event> events;
    public string flavourText;

    // Will be used for the news when this event happens.
    public NewsEvent newsEvent;
}

[System.Serializable]
public class Event
{
    public string EventIdentifier;
    public float weight;
    public List<StockChange> StockModification;
    public string Afterdelay = "This text is a spacer";
    public StockChange StockModificationAfterDelay;
    public List<Traits> traitsToChange;
    public List<WeightToGive> OtherEventWeightModifiers;
    public string FlavourText;

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
public class StockChange
{
    public string name;
    public StockMarket.StockMods valueMod;
    public float value;
    public StockMarket.StockMods changeRateMod;
    public float changeRate;
    public StockMarket.StockMods DeltaMod;
    public float changeRateDelta;
    public float? elapsedTimeCrash;
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
    public int numberOfTimesShownInNewsTicker;
    public Image image;
}

[System.Serializable]

public class EventManager : MonoBehaviour
{
    public StockMarket stockMarket;

    private float elapsedTime;
    private float interval = 1f;
    //private float elapsedTimeChoice;
    //private float intervalChoice = 5.0f;
    public bool pauseWorldEvents = false;

    //public float choiceTime = 10.0f;
    //public float choiceTimeElapsed;

    public GameObject eventHappendPrefab;
    public Transform eventHappendPrefabParent;
    public AudioSource AudioComponent;


    public GameObject choiceButton;
    public Transform choiceButtonParent;
    public GameObject choicePanel;
    //public GameObject mainPanel;


    public List<Event> events;
    public List<Event> eventsCopied;
    public List<Choice> choices;

    public List<Event> eventsThatHappened = new List<Event>();
    public List<Choice> choicesMade = new List<Choice>();
    public List<Traits> traits = new List<Traits>();

    public float crashRecoveryDelay = 10;

    private void Start()
    {
        eventsCopied = new List<Event>(events);
        //EventData eventData = new EventData();
        //eventData.FIllList();
        //events = eventData.events;
        //choices = eventData.choices;
        CloseChoicePanel();
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
                interval = UnityEngine.Random.Range(15,20);

                // Trigger a random event from the list
                TriggerWorldEvent();
            }

        }

        foreach (Event e in events)
        {
            foreach (StockChange stockModification in e.StockModification)
            {
                if (!stockModification.elapsedTimeCrash.IsUnityNull())
                {
                    float timeSinceCrash = (float)(elapsedTime - stockModification.elapsedTimeCrash);
                    if (timeSinceCrash >= crashRecoveryDelay)
                    {
                        StockChange delayStockMod = e.StockModificationAfterDelay;
                        stockMarket.UpdateStock(delayStockMod.name, delayStockMod.valueMod, delayStockMod.value, delayStockMod.changeRateMod, delayStockMod.changeRate, delayStockMod.DeltaMod, delayStockMod.changeRateDelta);
                    }    
                }
            }
        }

        //if (choiceTimeElapsed <= 0)
        //{
        //    elapsedTimeChoice += Time.deltaTime;
        //    if (elapsedTimeChoice >= intervalChoice)
        //    {
        //        elapsedTimeChoice = 0f;

        //        TriggerChoice();
        //    }
        //}

        //if (choiceTimeElapsed > 0)
        //{
        //    choiceTimeElapsed -= Time.deltaTime;
        //}
        //else
        //{
        //    CloseChoicePanel();
        //}
    }

    public void TriggerChoice()
    {
        pauseWorldEvents = true;

        //choiceTimeElapsed = choiceTime;
        choicePanel.SetActive(true);

        List<Choice> newChoicesList = new List<Choice>();
        choices.Sort((a, b) => b.weight.CompareTo(a.weight));

        foreach (Choice c in choices)
        {
            if (c.weight > 0)
            {
                newChoicesList.Add(c);
                Debug.Log(c.ChoiceIdentifier);
            }

            if (newChoicesList.Count >= 5)
            {
                break;
            }
        }


        // Choose a random Choice from newChoicesList
        int randomIndex = UnityEngine.Random.Range(0, newChoicesList.Count);
        Choice chosenChoice = newChoicesList[randomIndex];
        choicesMade.Add(chosenChoice);
        choices.Remove(chosenChoice);

        if (chosenChoice.newsEvent != null)
        {
            FindObjectOfType<ChoiceNewsBox>().AddNews(chosenChoice.newsEvent);
        }

        foreach (Event e in chosenChoice.events)
        {
            // Add choicebutton to the choice panel
            GameObject choiceButtonInstance = Instantiate(choiceButton, choiceButtonParent);
            choiceButtonInstance.GetComponentInChildren<TMP_Text>().text = e.EventIdentifier;
            choiceButtonInstance.GetComponent<Button>().onClick.AddListener(() => MakeChoice(e));
        }
    }

    public void MakeChoice(Event e)
    {
        if (e.newsEvent != null)
        {
            FindObjectOfType<ChoiceNewsBox>().AddNews(e.newsEvent);
        }
        pauseWorldEvents = false;
        ProcessEvent(e);
        eventsThatHappened.Add(e);
        events.Remove(e);
        //mainPanel.SetActive(false);
        CloseChoicePanel();
        FindObjectOfType<BubbleSpawners>().spawnBubbles = true;

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
        AudioComponent.Play();
    }

    private void TriggerWorldEvent()
    {
        if (events.Count > 0)
        {
            List<Event> worldEvents = new List<Event>();
            events.Sort((a, b) => b.weight.CompareTo(a.weight));

            foreach (Event e in events)
            {
                if (e.weight > 0)
                {
                    worldEvents.Add(e);
                }

                if (worldEvents.Count >= 90)
                {
                    break;
                }
            }

            // Choose a random event from the list
            //int randomIndex = UnityEngine.Random.Range(0, events.Count);
            //Event chosenEvent = events[randomIndex];

            Event chosenEvent = worldEvents[UnityEngine.Random.Range(0, worldEvents.Count)];
            ProcessEvent(chosenEvent);

            if (chosenEvent.newsEvent != null)
            {
                FindObjectOfType<NewsTicker>().AddNewItemToTicker(chosenEvent.newsEvent, chosenEvent.newsEvent.numberOfTimesShownInNewsTicker);
            }

            GameObject eventHappendLog = Instantiate(eventHappendPrefab, eventHappendPrefabParent);
            string s = chosenEvent.EventIdentifier;
            if (chosenEvent.newsEvent != null && chosenEvent.newsEvent.newsTitle != "")
            {
                s = chosenEvent.newsEvent.newsTitle;
            }
            eventHappendLog.GetComponentInChildren<TMP_Text>().text = s;
            if (eventHappendPrefabParent.childCount > 10)
            {
                Destroy(eventHappendPrefabParent.GetChild(0).gameObject);
            }


            // Trigger the chosen event (for now, just log it)
            //Debug.Log($"Event triggered: {chosenEvent.name}");
        }

        if (events.Count < 6){
            events = new List<Event>(eventsCopied);
        }
    }

    public void ProcessEvent(Event chosenEvent)
    {
        // Update the Stock values based on the chosen event
        foreach (StockChange stock in chosenEvent.StockModification)
        {
            // Get all the stocks that are in the game
            // Update the values of the stocks that the event will invluence
            
            stockMarket.UpdateStock(stock.name, stock.valueMod, stock.value, stock.changeRateMod, stock.changeRate, stock.DeltaMod, stock.changeRateDelta);

        }

        // Update the events weights based on the chosen event
        foreach (WeightToGive weightToGive in chosenEvent.OtherEventWeightModifiers)
        {
            foreach (Event e in events)
            {
                if (e.EventIdentifier == weightToGive.name)
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
            if (trait.name == "Add money")
            {
                //Debug.Log("Add money: " + trait.value);
                stockMarket.PlayerMoney += trait.value;
            }

            if (trait.name == "Remove money")
            {
                stockMarket.PlayerMoney -= trait.value;
            }

            foreach (Traits t in traits)
            {
                if (t.name == trait.name)
                {
                    t.SetTrait(trait.value);
                }
            }
        }

        // Add the chosen event to the list of events that happened
        eventsThatHappened.Add(chosenEvent);
        events.Remove(chosenEvent);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
