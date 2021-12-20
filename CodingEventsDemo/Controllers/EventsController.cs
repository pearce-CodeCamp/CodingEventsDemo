using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coding_events_practice.Controllers
{
    public class EventsController : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            // ViewBag.events = EventData.GetAll();
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }

        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        [HttpPost]
        // don't need route because we have renamed this method to be called "Add", so it is automatically mapped to the
        // Events/Add route
/*        [Route("Events/Add")]*/
        // used to have 2 parameters: name, description
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            // addEventViewModel.Name is whatever name was inputted in form
            // addEventViewModel.Description is the inputted description
            // Therefore, we can use these values to create a new instance of Event

            // we should only create a new event if we have valid data according to the addEventViewModel
            // ModelState.IsValid returns false if the data inputted into the name and description inputs
            // in the form on the Add view are invalid
            // returns true is they are valid
            if (ModelState.IsValid)
            {
                Event newEvent = new Event(addEventViewModel.Name, addEventViewModel.Description);

                // Syntax from the book commented here:
                /*Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description
                };*/

                EventData.Add(newEvent);

                return Redirect("/Events");
            }

            // pass the addEventViewModel back into the view and rerender it
            return View(addEventViewModel);
            
        }

        [HttpGet]
        [Route("Events/Edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            // use eventId to select an Event via EventData and store that event
            Event eventToEdit = EventData.GetById(eventId);
            ViewBag.eventToEdit = eventToEdit;
            ViewBag.title = $"Editing {eventToEdit.Name} (ID={eventToEdit.Id})";
            return View();
        }

        // path: /Events/Edit
        [HttpPost]
        [Route("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event edittedEvent = EventData.GetById(eventId);
            edittedEvent.Name = name;
            edittedEvent.Description = description;
            return Redirect("/Events");
        }

        public IActionResult Delete()
        {
            //ViewBag.title = "Delete Events";
            ViewBag.events = EventData.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }
    }
}
