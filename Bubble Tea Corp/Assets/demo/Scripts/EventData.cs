using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public List<Event> events;
    public List<Choice> choices;

    // Weight with 0 will never happen

    public void FIllList()
    {
        choices = new List<Choice>();
        events = new List<Event>();


        ////////////////////////////////////////////
        ///WORLD EVENTS//////////////////////////////////


        Event worldEventCryptoStockIncrease = new Event
        {
            name = "Crypto Stock Increase",
            weight = 1.0f,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>
            {
                new StockChange { name = "Crypto", valueMod = StockMarket.StockMods.Ignore, value = 10.0f, changeRateMod = StockMarket.StockMods.Ignore, changeRate = 100.0f, DeltaMod = StockMarket.StockMods.Add, changeRateDelta = 100.0f},
            },
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Crypto Stock Increase", newsText = "Crypto stock increased by 1.0" }
        };

        events.Add(worldEventCryptoStockIncrease);


        Event worldEventNewVirus = new Event
        {
            name = "New Virus",
            weight = 1.0f,
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
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "World Wide Pandemic", newsText = "The whole world is on lock!!!" }
        };

        events.Add(worldEventPandemic);

        Event worldEventWorldWar = new Event
        {
            name = "World War",
            weight = 0f,
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


        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);

        Event worldEventWeaponFactory = new Event
        {
            name = "Weapon Factory",
            weight = 0f,
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
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent { newsTitle = "Emplyee Strike", newsText = "The workers are angry!" }
        };

        events.Add(woldEventEmpolyeeStrike);

        ////////////////////////////////////////////
        ///CHOICES//////////////////////////////////
        ////////////////////////////////////////////

        Event choiceEventFireEmployees50 = new Event
        {
            name = "Fire 50% of your workforce",
            weight = 1f,
            weightToGive = new List<WeightToGive>()
            {
                new WeightToGive { name = "Employee Strike", weight = 10 },
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Add money", value = 5000 },
            },
            newsEvent = new NewsEvent { newsTitle = "choiceEventFireEmployees", newsText = "choiceEventFireEmployees" }
        };

        Event choiceEventFireEmployees20 = new Event
        {
            name = "Fire 20% of your workforce",
            weight = 1f,
            weightToGive = new List<WeightToGive>()
            {
                new WeightToGive { name = "Employee Strike", weight = 5 },
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Add money", value = 2000 },
            },
            newsEvent = new NewsEvent { newsTitle = "choiceEventFireEmployees", newsText = "choiceEventFireEmployees" }
        };

        Event choiceEventFireEmployees60 = new Event
        {
            name = "Fire 60% of your workforce",
            weight = 1f,
            weightToGive = new List<WeightToGive>()
            {
                new WeightToGive { name = "Employee Strike", weight = 11 },
            },
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Add money", value = 6000 },
            },
            newsEvent = new NewsEvent { newsTitle = "choiceEventFireEmployees60", newsText = "choiceEventFireEmployees60" }
        };


        Choice newChoiceLayOffWorkers = new Choice
        {
            name = "Do you want to Lay off workers?",
            weight = 1.0f,
            events = new List<Event>
            {
                choiceEventFireEmployees50,
                choiceEventFireEmployees20,
                choiceEventFireEmployees60
            }
        };

        choices.Add(newChoiceLayOffWorkers);


        ////////////////////////////////////////////

        Event choiceEventInheritAccept = new Event
        {
            name = "Accept inheritance",
            weight = 1f,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>
            {
                new Traits { name = "Add money", value = 10000 },
            },
            newsEvent = new NewsEvent(),
        };

        Event choiceEventInheritDecline = new Event
        {
            name = "DECLINE inheritance",
            weight = 1f,
            weightToGive = new List<WeightToGive>(),
            stockToChange = new List<StockChange>(),
            traitsToChange = new List<Traits>(),
            newsEvent = new NewsEvent(),
        };


        Choice newChoiceYourInherit = new Choice
        {
            name = "Your gradma is missing, you inherit money",
            weight = 1.0f,
            events = new List<Event>
            {
                choiceEventInheritAccept,
                choiceEventInheritDecline
            }
        };

        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceYourInherit);
        choices.Add(newChoiceLayOffWorkers);
        choices.Add(newChoiceLayOffWorkers);
        choices.Add(newChoiceLayOffWorkers);
        choices.Add(newChoiceLayOffWorkers);
        choices.Add(newChoiceLayOffWorkers);


        ////////////////////////////////////////////
        ///FILLING THE REST//////////////////////////////////
        ////////////////////////////////////////////


        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
        events.Add(worldEventCryptoStockIncrease);
    }
}
