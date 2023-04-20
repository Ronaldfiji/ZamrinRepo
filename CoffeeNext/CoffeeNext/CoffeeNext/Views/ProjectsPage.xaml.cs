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
    public partial class ProjectsPage : ContentPage
    {
        
        public ProjectsPage()
        {
            InitializeComponent();
        }
        
        protected override async void OnAppearing()
        {
            base.OnAppearing();
           var vm = (ProjectsViewModel)BindingContext;
            if (vm.Projects.Count == 0)
                await vm.RefreshCommand.ExecuteAsync();
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
            var random = new Random();
            var random1 = (int)random.Next(0, 255);
            var random2 = (int)random.Next(0, 255);
            var random3 = (int)random.Next(0, 255);
            App.Current.Resources["WindowBackgroundColor"] = Color.FromRgb(random1, random2, random3);
            
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
        }

       /* private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");

        }*/
    }
}