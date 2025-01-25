using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData", order = 1)]
public class EventData : ScriptableObject
{
    public List<Event> events;
}
