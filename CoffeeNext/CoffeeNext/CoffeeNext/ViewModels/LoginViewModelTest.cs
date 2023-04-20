using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CoffeeNext.ViewModels
{
    public class LoginViewModelTest : BaseViewModel
    {
        private string email ="Bula";

        public Command SetEmailAddressCommand { get; }
       

        public LoginViewModelTest()
        {
            SetEmailAddressCommand = new Command(SetEmailAddress);
            
        }
        public string EmailAddress
        {
          /*  get { return email; }
            set {
                if (email == value)
                    return;
                email = value;
                OnPropertyChanged("EmailAddress");
                //SetProperty(ref email, value);
            }*/

            get => email;
            set => SetProperty(ref email, value);
        }

       

        private  async void SetEmailAddress()
        {
            //await Task.Run(email = "My Email is: Roqald.com");// => DoSomething());
            //email = "My Email is: Roqald.com";
            EmailAddress = "My mail box -> Ron@yahoo.com";
            await Task.Delay(1);
            Console.WriteLine("Email add is :" + EmailAddress.ToString());
            await Application.Current.MainPage.DisplayAlert("My Favourie", EmailAddress, "OK");
        }



    }
}
