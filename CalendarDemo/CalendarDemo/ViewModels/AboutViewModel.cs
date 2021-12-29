using CalendarDemo.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CalendarDemo.Models;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamForms.Controls;
using CalendarDemo.Services;

namespace CalendarDemo.ViewModels
{
    public class AboutViewModel : ViewmodelBase
    {

       
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        private ObservableCollection<SpecialDate> events;
        public ObservableCollection<SpecialDate> Events
        {
            get { return events; }
            set { events = value; OnPropertyChanged(nameof(Events)); }
        }

        private ObservableCollection<CalendarEvents> calendarEvents;
        public ObservableCollection<CalendarEvents> CalendarEvents
        {
            get { return calendarEvents; }
            set { SetProperty(ref calendarEvents, value); }
        }

        private CalendarEvents selectedEvents;
        public CalendarEvents SelectedEvents
        {
            get { return selectedEvents; }
            set { SetProperty(ref selectedEvents, value); }
        }


        public ICommand ChosenDateCommand { get; set; }

        public AsyncCommand GotoAddEventPage { get; set; }

        public AsyncCommand OnAppearingCommand { get; set; }

        public AsyncCommand SelectionChangedCommand { get; set; }

        public AboutViewModel()
        {
            Title = "Events";
            ChosenDateCommand = new MvvmHelpers.Commands.Command(ChosenDate);
            GotoAddEventPage = new AsyncCommand(GotoAddEventPageAsync);
            OnAppearingCommand = new AsyncCommand(OnAppearingAsync);
            SelectionChangedCommand = new AsyncCommand(SelectionChangeAsync);
        }

        private async Task SelectionChangeAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(EventInformationPage)}?EventDate={SelectedEvents.EventDate}");
        }

        private async Task OnAppearingAsync()
        {
            CalendarEvents = new ObservableCollection<CalendarEvents>(await CalendarEventService.GetAllEvents());
            Events = new ObservableCollection<SpecialDate>();
            foreach (CalendarEvents item in CalendarEvents)
            {
                Events.Add(new SpecialDate(item.EventDate){ BackgroundColor = Color.Red, Selectable = true});
            }
        }

        private async Task GotoAddEventPageAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddEventPage)}?EventDateAsString={Date}");
        }

        private async void ChosenDate(object obj)
        {
            DateTime? dateTime = obj as DateTime?;
            CalendarEvents ev = await CalendarEventService.GetEventByDate(dateTime.ToString());
            if (ev != null)
            {
                await Shell.Current.GoToAsync($"{nameof(EventInformationPage)}?EventDate={dateTime}");
            }
        }

       
    }
}