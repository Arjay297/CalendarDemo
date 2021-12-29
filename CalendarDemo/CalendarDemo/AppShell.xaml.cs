using CalendarDemo.ViewModels;
using CalendarDemo.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CalendarDemo
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
            Routing.RegisterRoute(nameof(EventInformationPage), typeof(EventInformationPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
