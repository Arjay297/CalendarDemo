using CalendarDemo.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalendarDemo.ViewModels
{

    [QueryProperty(nameof(EventDateAsString), nameof(EventDateAsString))]
    public class AddEventViewModel : ViewmodelBase
    {

        public DateTime EventDate { get; set; }

        public string EventDateAsString { get; set; }

        private string eventTitle;
        public string EventTitle
        {
            get { return eventTitle; }
            set { SetProperty(ref eventTitle, value); }
        }

        private string eventInformation;
        public string EventInformation
        {
            get { return eventInformation; }
            set { SetProperty(ref eventInformation, value); }
        }

        public AsyncCommand SaveCommand { get; set; }
        public AsyncCommand CancelCommand { get; set; }

        public AsyncCommand OnAppearingCommand { get; set; }
        public AddEventViewModel()
        {
            SaveCommand = new AsyncCommand(SaveAsync);
            CancelCommand = new AsyncCommand(CancelAsync);
            OnAppearingCommand = new AsyncCommand(OnAppearingAsync);
        }

        private async Task OnAppearingAsync()
        {
           EventDate = Convert.ToDateTime(EventDateAsString);
        }

        private async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async Task SaveAsync()
        {
            await CalendarEventService.AddEvents(EventTitle, EventInformation, EventDate);
            await Shell.Current.GoToAsync("..");
        }
    }
}
