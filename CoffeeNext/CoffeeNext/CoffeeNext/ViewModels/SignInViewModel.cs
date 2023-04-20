using CoffeeNext.Helper;
using CoffeeNext.Models;
using CoffeeNext.Services;
using CoffeeNext.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoffeeNext.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        public LoginModel LoginModel_ { get; set; } = new LoginModel();
        public Command SignInCommand { get; }     

        public AsyncCommand SignUpCommand { get; }

        //public  bool isLoggedIn;// { get; set; }
        public SignInViewModel()
        {
            SignInCommand = new Command(SignInHandler);
            SignUpCommand = new AsyncCommand(DisplaySignUpPage);
           
        }

        public void passUserNameToLoginField()
        {            
            LoginModel_.Email = Settings.Username;
           
        }
       /* public bool IsLoggedIn
        {
            get => isLoggedIn;
            set => SetProperty(ref isLoggedIn, value);
        }*/

        public async void SignInHandler(object sender)
        {
            try
            {
                IsBusy = true;
                var loggedIn = await UserService.LoginUser(LoginModel_);
                if (!loggedIn.IsSuccess)
                {
                 
                    IsBusy = false;
                    if(loggedIn.ErrorCode == 401)
                        await App.Current.MainPage.DisplayAlert("Error", "Invalid username or password !", "Cancel");
                    if (loggedIn.ErrorCode == 500)
                        await App.Current.MainPage.DisplayAlert("Error", "Application api error !", "Cancel");                   
                    return;
                }

                IsBusy = false;
                Settings.Instance.IsUserLoggedInStatus = true;
                (Shell.Current as AppShell).LoginStatusDisplayFlyOut = true;              
                await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("System Error", "Application error, contact system admin !", "Cancel");
                throw ex;
            }
            finally
            {
                new LoginModel();
                IsBusy = false;
            }
        }

        async Task DisplaySignUpPage() //RegoPage
        {
            try
            {
                await Shell.Current.GoToAsync("RegoPage");
            }catch (Exception ex)
            {
                Debug.WriteLine("Failed to open destination page: " + ex);
                throw ex;
            }
            
        }
       public async Task SignOffFromApp()
        {
            try {
                
                await AuthServices.SignOff();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to signout from app: " + ex);
                throw ex;

            }
        }



    }
}
