using Nunos2.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nunos2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new NavigationPage(new Views.MainPage()) { BarTextColor = Color.Black };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
