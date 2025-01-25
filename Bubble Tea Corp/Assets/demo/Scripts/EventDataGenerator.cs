using System.Collections.Generic;
using UnityEngine;

public class EventDataGenerator
{
    public EventData eventData;
    public void GenerateDummyData()
    {
        eventData.events = new List<Event>
        {
            new Event
            {
                name = "Event1",
                weight = 1.0f,
                eventType = 0,
                weightToGive = new List<WeightToGive>
                {
                    new WeightToGive { name = "Event2", weight = 0.5f }
                },
                stockToChange = new List<Event>(),
                traitsToChange = new List<Traits>
                {
                    new Traits { name = "Trait1", value = 1.0f }
                },
                newsEvent = new NewsEvent { newsTitle = "News1", newsText = "News Text 1" }
            },
            new Event
            {
                name = "Event2",
                weight = 2.0f,
                eventType = 1,
                weightToGive = new List<WeightToGive>
                {
                    new WeightToGive { name = "Event1", weight = 0.3f }
                },
                stockToChange = new List<Event>(),
                traitsToChange = new List<Traits>
                {
                    new Traits { name = "Trait2", value = 2.0f }
                },
                newsEvent = new NewsEvent { newsTitle = "News2", newsText = "News Text 2" }
            }
        };

        Debug.Log("Dummy data generated.");
    }
}
