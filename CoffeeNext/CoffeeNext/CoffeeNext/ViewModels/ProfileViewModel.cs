using CoffeeNext.Helper;
using CoffeeNext.Models;
using CoffeeNext.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CoffeeNext.ViewModels
{
   /* public interface IProfileViewModel 
    {
        AsyncCommand SignOffCommand { get; }
    }*/
    public class ProfileViewModel : BaseViewModel//, IProfileViewModel
    {
        public AsyncCommand SignOffCommand { get; set; }
        public Person Person { get; } = new Person();
        public ProfileViewModel()
        {
            SignOffCommand = new AsyncCommand(SignOffUser);
            
        }

        async Task SignOffUser()
        {            
            try
            {              
                await AuthServices.SignOff();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(ProfileViewModel)}  , Failed to signout from app: " + ex);
                throw ex;
            }  
        }


        // Need to implement this fucntion.
         public override Task InitializeAsync(IDictionary<string, string> query)
         {
             IsBusy = true;
             IsBusy = true;
             Debug.WriteLine("Logging from profile page.....InitAsync function................................");
            // await Task.Delay(1);  
             IsBusy = false;
            Debug.WriteLine("Logging from profile page.....InitAsync function......async task completed................");
            return Task.CompletedTask;
        }


        public async Task DisplayUserProfile()
        {
            try
            {              
                if (!await AuthServices.IsUserSessionValid()) {
                    await App.Current.MainPage.DisplayToastAsync(UIMessage.DisplayTimeOutMessage());
                    SignOffUser().Wait();
                    Application.Current.MainPage = new AppShell();
                    //await Application.Current.MainPage.DisplayAlert("error", "Invalid Credentials", "CANCEL");
                }
                else
                {
                    var claims = await AuthServices.GetUserClaims();
                    string id = claims.FindFirst("ID")?.Value;
                    string firstName = claims.FindFirst("Given_Name")?.Value;
                    string lastName = claims.FindFirst("Family_name")?.Value;
                    string email = claims.FindFirst("Email")?.Value;
                    string dob = claims.FindFirst("Birthdate")?.Value;
                    Person.FirstName = firstName;
                    Person.LastName = lastName;
                    Person.Email = email;
                    Person.DOB = dob;                   
                }                         
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Failed to extract claims data from token..............." + ex);
                throw ex;
            }

        }

     
    }
}
