using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public List<Event> events;


    // !!! EventType 0 is a world event, EventType 1 is a choice event !!!

    public void FIllList()
    {
        events = new List<Event>();

        Event worldEventPepsiStockIncrease = new Event
        {
            name = "Pepsi Stock Increase",
            weight = 1.0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<Event>
            {
                new Event { name = "Pepsi", weight = 1.0f }
            },
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Pepsi Stock Increase", newsText = "Pepsi stock increased by 1.0" }
        };

        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);

        Event choiceEvent = new Event
        {
            name = "Add Sugar to Tea",
            weight = 1.0f,
            eventType = 1,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<Event>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Health", value = -1 }
            },
            newsEvent = new NewsEvent { newsTitle = "Sugar Added", newsText = "You added sugar to your tea." }
        };

        events.Add(choiceEvent);
        events.Add(choiceEvent);
        events.Add(choiceEvent);
        events.Add(choiceEvent);

        Event worldEventWorldWar = new Event
        {
            name = "World War",
            weight = 1.0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>
            {
                new WeightToGive { name = "Weapon Factory", weight = 99.0f },
                new WeightToGive { name = "World Peace", weight = -10f }
            },
            stockToChange = new List<Event>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "World War", newsText = "CNN a world war happend!" }
        };

        events.Add(worldEventWorldWar);


        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);

        Event worldEventWeaponFactory = new Event
        {
            name = "Weapon Factory",
            weight = 1.0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<Event>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Weapons mass", newsText = "Poeple are buying weapons at a mass!" }
        };

        events.Add(worldEventWeaponFactory);

        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
    }
}
