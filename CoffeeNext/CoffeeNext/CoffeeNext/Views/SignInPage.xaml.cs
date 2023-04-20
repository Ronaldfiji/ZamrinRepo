using CoffeeNext.Helper;
using CoffeeNext.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeNext.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            UsernameEntry.Completed += (sender, args) => { UserPassword.Focus(); };
           // UserPassword.Completed += (sender, args) => { SignInCommand.Execute(null); };
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();           
            var vm = BindingContext as SignInViewModel;
            if (vm != null)
            {
                vm.passUserNameToLoginField();
            }
                
               /* if (Settings.Instance.IsUserLoggedInStatus)
                await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");*/
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
            await Shell.Current.GoToAsync($"RegoPage");
            // <ShellContent Route="SignInPage" ContentTemplate="{DataTemplate local:RegistrationPage}" />
        }


    }
}