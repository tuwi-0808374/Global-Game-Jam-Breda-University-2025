using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public List<Event> events;


    // !!! EventType 0 is a world event, EventType 1 is a choice event !!!
    // Weight with 0 will never happen

    public void FIllList()
    {
        events = new List<Event>();

        Event worldEventPepsiStockIncrease = new Event
        {
            name = "Crypto Stock Increase",
            weight = 1.0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>
            {
                new StockChange { name = "Crypto", valueMod = StockMarket.StockMods.Ignore, value = 10.0f, changeRateMod = StockMarket.StockMods.Ignore, changeRate = 100.0f, DeltaMod = StockMarket.StockMods.Add, changeRateDelta = 100.0f},
            },
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Crypto Stock Increase", newsText = "Crypto stock increased by 1.0" }
        };

        events.Add(worldEventPepsiStockIncrease);


        Event worldEventNewVirus = new Event
        {
            name = "New Virus",
            weight = 1.0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>
            {
                new WeightToGive { name = "Pandemic", weight = 10.0f } 
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "New virus!", newsText = "A new strane of virus discorverd in a Dutch Foodmarker!" }
        };

        events.Add(worldEventNewVirus);

        Event worldEventPandemic = new Event
        {
            name = "Pandemic",
            weight = 0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "World Wide Pandemic", newsText = "The whole world is on lock!!!" }
        };

        events.Add(worldEventPandemic);


        Event choiceEventSugar = new Event
        {
            name = "Add Sugar to Tea",
            weight = 1.0f,
            eventType = 1,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Health", value = -1 }
            },
            newsEvent = new NewsEvent { newsTitle = "Sugar Added", newsText = "You added sugar to your tea." }
        };

        events.Add(choiceEventSugar);

        Event choiceEventFireEmployees50 = new Event
        {
            name = "Fire 50% of your workforce",
            weight = 1f,
            eventType = 1,
            weightToGive = new List<WeightToGive>()
            {
                new WeightToGive { name = "Employee Strike", weight = 10 },
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "choiceEventFireEmployees", newsText = "choiceEventFireEmployees" }
        };

        events.Add(choiceEventFireEmployees50);

        Event choiceEventFireEmployees20 = new Event
        {
            name = "Fire 20% of your workforce",
            weight = 1f,
            eventType = 1,
            weightToGive = new List<WeightToGive>()
            {
                new WeightToGive { name = "Employee Strike", weight = 5 },
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Add money", value = 1000 },
            },
            newsEvent = new NewsEvent { newsTitle = "choiceEventFireEmployees", newsText = "choiceEventFireEmployees" }
        };

        events.Add(choiceEventFireEmployees20);

        Event worldEventWorldWar = new Event
        {
            name = "World War",
            weight = 0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>
            {
                new WeightToGive { name = "Weapon Factory", weight = 99.0f },
                new WeightToGive { name = "World Peace", weight = -10f }
            },
            stockToChange = new List<StockChange>(),
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
            weight = 0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Weapons mass", newsText = "Poeple are buying weapons at a mass!" }
        };

        events.Add(worldEventWeaponFactory);


        Event woldEventEmpolyeeStrike = new Event
        {
            name = "Employee Strike",
            weight = 0f,
            eventType = 0,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Emplyee Strike", newsText = "The workers are angry!" }
        };

        events.Add(woldEventEmpolyeeStrike);





        Event choiceEventDummy = new Event
        {
            name = "dummy",
            weight = .1f,
            eventType = 1,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "dummy", newsText = "dummy" }
        };

        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);
        events.Add(choiceEventDummy);


        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
        events.Add(worldEventPepsiStockIncrease);
    }
}
