using CoffeeNext.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoffeeNext.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignOutPage :  ContentPage
    {
        public SignOutPage()
        {
            //InitializeComponent();
            new SignInViewModel().SignOffFromApp().Wait();
            //SO.SignOffFromApp().GetAwaiter().GetResult();
            
        }
    }
}