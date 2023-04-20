using CoffeeNext.Services;
using CoffeeNext.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeNext
{
    public partial class App : Application
    {
        public static string UserName { get; set; }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();           
            InitApp();
        }

        private void InitApp()
        {
            /*   if (!IsUserLoggedIn)
              {
                  MainPage = new NavigationPage(new AppShell());

              }
              else
              {
                  MainPage = new NavigationPage(new ProfilePage());
              }*/


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
