using CoffeeNext.Services;
using CoffeeNext.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeNext.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {
        IAuthService authService;
        public FlyoutHeader()
        {
            InitializeComponent();
            //authService = DependencyService.Get<IAuthService>();           
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {             
            //await authService.SignOff();
            await new SignInViewModel().SignOffFromApp();
            /*
            SignInViewModel SO = new SignInViewModel();
            SO.SignOffFromApp().GetAwaiter().GetResult();
            await Task.Delay(0);
            */
           
            //await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
            //await Shell.Current.GoToAsync($"RegoPage");
            // <ShellContent Route="SignInPage" ContentTemplate="{DataTemplate local:RegistrationPage}" />
        }

    }
}