using System;
using System.Collections.Generic;
using System.Linq;
using CodingEventsDemo.Models;

namespace CodingEventsDemo.Data
{
    public class EventData
    {
        // string key is event name
        // string value is event description
        //static private Dictionary<string, string> Events = new Dictionary<string, string>();

        // int key is event id
        // Event value is an instance of the Event class... and Event object
        static private Dictionary<int, Event> Events = new Dictionary<int, Event>();

        // GetAll
        // returns a List-esque collection of all Event objects stored in the Events
        // dictionary
        public static IEnumerable<Event> GetAll()
        {
            return Events.Values;
        }

        // Add
        // adds a KeyValuePair to the Events dictionary
        // the key is the id of the Event we are adding
        // the value is the Event object itself
        public static void Add(Event newEvent)
        {
            Events.Add(newEvent.Id, newEvent);
        }

        // Remove
        // takes in an id integer
        // removes the Event from Events that has the given id
        public static void Remove(int id)
        {
            Events.Remove(id);
        }

        // GetById
        // takes in an id integer
        // returns the Event from Events that has the given id
        public static Event GetById(int id)
        {
            return Events[id];
        }
    }
}
