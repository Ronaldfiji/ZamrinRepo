using CoffeeNext.Models;
using CoffeeNext.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



namespace CoffeeNext.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command LoginCommand_Test { get; }
        public ICommand CancelCommand { get; }

        public ICommand SelectGenderCommand { get; }

        private bool emailValid;
        private bool passwordValid;
        private bool rePasswordValid;

        private string username;
        private string password;
        private string rePassword;
        private string errorMessage;

        private bool emailaddress_valid_test;
        private string emailaadressTest;


        private string firstname;
        private bool firstnameValid;
        private string lastname;
        private bool lastnameValid;

        private string phonenumber;
        private bool phonenumberValid;

        private List<Country> countryList;
        //public ObservableCollection<Country> countryList { get; }

        private string selectedGender;
       

        public LoginViewModel()
        {
            countryList = new List<Country>();
            Task.Run(async () => await populateCountryList());

            LoginCommand = new Command(OnLoginClicked, ValidateLogin);
            CancelCommand = new Command(OnCancelCliked);

            LoginCommand_Test = new Command(LoginActionClicked_Test, ValidateLogin_Test);

            this.PropertyChanged +=
               (_, __) => LoginCommand.ChangeCanExecute();

            this.PropertyChanged +=
              (_, __) => LoginCommand_Test.ChangeCanExecute();

            SelectGenderCommand = new Command(SelectGenderClicked);
            
         }

        //private async void OnAddItem(object obj)

        private async Task populateCountryList()
        {
            var countryListT = await CountryDataStore.GetCountriesList();

            foreach (var country in countryListT)
            {
                countryList.Add(country);
            }            
        }

        private async void OnCancelCliked()
        {
            this.Password = "";
            this.Username = "";
            this.FirstName = "";
            this.LastName = "";

            await Application.Current.MainPage.DisplayAlert("Selected Gender is", SelectedGender, "OK");

           // await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            //await Shell.Current.GoToAsync("..");       
        }


        private async void LoginActionClicked_Test(object obj)
        {
            await Application.Current.MainPage.DisplayAlert("Your password", emailaddress_valid_test.ToString(), "OK");
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }
        private async void OnLoginClicked(object obj)
        {
            //if (String.IsNullOrWhiteSpace(username))
            errorMessage = "Username connot be empty...............";
            var d = ErrorMessage.ToString();
            Console.WriteLine("The value is: " + d);
            await Application.Current.MainPage.DisplayAlert("Your password", $"{Password +' '+ FirstName + ' ' + LastName}","OK"); ;


            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        private  void SelectGenderClicked(Object sender)
        {

        }
        private bool ValidateLogin(object obj)
        {
            return !String.IsNullOrWhiteSpace(username)
                && !String.IsNullOrWhiteSpace(password)
                && !String.IsNullOrWhiteSpace(firstname)
                && !String.IsNullOrWhiteSpace(lastname)
                && !String.IsNullOrWhiteSpace(rePassword)
                && passwordValid && emailValid && firstnameValid 
                && lastnameValid && rePasswordValid;
        }

        public bool ValidateLogin_Test(object ob)
        {
            return !String.IsNullOrWhiteSpace(firstname) && emailaddress_valid_test;

        }



        public bool EmailAddress_Test_Valid
        {
            get => emailaddress_valid_test;
            set => SetProperty(ref emailaddress_valid_test, value);
        }

        public bool FirstNameValid
        {
            get => firstnameValid;
            set => SetProperty(ref firstnameValid, value);
        }
        public bool LastNameValid
        {
            get => lastnameValid;
            set => SetProperty(ref lastnameValid, value);
        }

        public bool PhoneNumberValid
        {
            get => phonenumberValid;
            set => SetProperty(ref phonenumberValid, value);
        }
        public bool EmailValid
        {
            get => emailValid;
            set => SetProperty(ref emailValid, value);

        }

        public bool PasswordValid
        {
            get => passwordValid;
            set => SetProperty(ref passwordValid, value);

        }
        public bool RePasswordValid
        {
            get => rePasswordValid;
            set => SetProperty(ref rePasswordValid, value);

        }


        public string FirstName
        {
            get => firstname;
            set => SetProperty(ref firstname, value);

        }
        public string LastName
        {
            get => lastname;
            set => SetProperty(ref lastname, value);

        }

        public string PhoneNumber
        {
            get => phonenumber;
            set => SetProperty(ref phonenumber, value);

        }
        public string EmailAddressTest
        {
            get => emailaadressTest;
            set => SetProperty(ref emailaadressTest, value);
        }
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);

        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);

        }
        public string ReEnterPassword
        {
            get => rePassword;
            set => SetProperty(ref rePassword, value);

        }
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
            /* set
             {
                 errorMessage = value;
                 OnPropertyChanged("ErrorMessage");
             }*/

        }

        public string SelectedGender
        {
            get => selectedGender;
            set => SetProperty(ref selectedGender, value);
            
        }

       
        public List<Country> CountryList //List<Country>
        {
            get => countryList.OrderBy(p => p.Name).ToList(); 
            // This LINQ statement returns a sorted list
            //return (from cc in countryList orderby(cc.Name) select cc).ToList();
           set => SetProperty(ref countryList, value);
        }

        

    }
}
