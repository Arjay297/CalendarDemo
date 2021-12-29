using CalendarDemo.Models;
using CalendarDemo.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarDemo.ViewModels
{

    [QueryProperty(nameof(EventDate), nameof(EventDate))]
    internal class EventInformationViewModel : ViewmodelBase
    {

        public string EventId { get; set; }
        public string EventDate { get; set; }


        private CalendarEvents calendarEvent;
        public CalendarEvents CalendarEvent
        {
            get { return calendarEvent; }
            set { SetProperty(ref calendarEvent, value); }
        }


        public AsyncCommand OnAppearingCommand{ get; set; }
        public EventInformationViewModel()
        {
            OnAppearingCommand = new AsyncCommand(OnAppearingAsync);
        }

        private async Task OnAppearingAsync()
        {
            CalendarEvent = await CalendarEventService.GetEventByDate(EventDate);
        }
    }
}
