using CoffeeNext.Helper;
using CoffeeNext.ViewModels;
using CoffeeNext.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoffeeNext
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        
        public AppShell()
        {

            InitializeComponent();
            BindingContext = this;
            //BindingContext = new SignInViewModel();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(LoginPageTest), typeof(LoginPageTest));
            //Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute("RegoPage", typeof(RegistrationPage));     


        }

        //This is test function flyout menu item
      private async void OnSignOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                await App.Current.MainPage.DisplayAlert("Menu ", "Menu Item Clicked !", "Cancel");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to display menu display alert" + ex);
                throw ex;
                // Possible that device doesn't support secure storage on device.
            }
        }

        private bool _isUserLoggedIn;
        public bool LoginStatusDisplayFlyOut
        {
            get => _isUserLoggedIn;
            set => SetProperty(ref _isUserLoggedIn, value);
        }
        protected bool SetProperty<T>(ref T backingStore, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "", System.Action onChanged = null)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        //Function not used // Was used to testing.
        private async void LogOffButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        /* private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
         {
              OnSignOff_Clicked(sender, e);
              await Task.Delay(0);
             //await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
             //await Shell.Current.GoToAsync($"RegoPage");
             // <ShellContent Route="SignInPage" ContentTemplate="{DataTemplate local:RegistrationPage}" />
         }*/




                /*
                public bool IsLogged 
                {
                    get => (bool)GetValue(IsLoggedProperty);
                    set => SetValue(IsLoggedProperty, value);
                }
                public static readonly BindableProperty IsLoggedProperty =
            BindableProperty.Create("IsLogged", typeof(bool), typeof(AppShell), false, propertyChanged: IsLogged_PropertyChanged);

                private static void IsLogged_PropertyChanged(BindableObject bindable, object oldValue, object newValue)
                {
                    //handle log in/log out event
                  //  if ((bool)newValue)
               //user just logged in logic
            //else
              //user just logged out logic
                }
            */

    }
}
