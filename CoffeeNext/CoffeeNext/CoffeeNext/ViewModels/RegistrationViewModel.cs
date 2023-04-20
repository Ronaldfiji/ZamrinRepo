using CoffeeNext.Models;
using CoffeeNext.Services;
using CoffeeNext.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using System.Diagnostics;

namespace CoffeeNext.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public Person Person { get; } = new Person();
        public ResponseStatus ResponseStatus { get; } = new ResponseStatus();

       // private List<Country> countryList = new List<Country>();
        public Command SignUpCommand { get; }
        public ICommand CancelCommand { get; }
        private bool displayToast;

       private Country _selectedCountry = null;

        public RegistrationViewModel()
        {

            
            //Task.Run(async () => await populateCountryList());
            SignUpCommand = new Command(OnSignUpClicked, ValidateLogin);
            CancelCommand = new Command(OnCancelCliked);

            //this.PropertyChanged += (_, __) => LoginCommand.ChangeCanExecute();

            Person.PropertyChanged += (_, __) => SignUpCommand.ChangeCanExecute();

            
        }

     /*   private async Task populateCountryList()
        {
            var countryListT = await CountryDataStore.GetCountriesList();

            foreach (var country in countryListT)
            {
                countryList.Add(country);
            }
        }
        public List<Country> CountryList //List<Country>
        {
            get => countryList.OrderBy(p => p.Name).ToList();
            // This LINQ statement returns a sorted list
            //return (from cc in countryList orderby(cc.Name) select cc).ToList();
            set => SetProperty(ref countryList, value);
        }*/

        private async void OnCancelCliked()
        {
            //await Application.Current.MainPage.DisplayAlert("Selected Gender is", Person.FirstName, "OK");
            //await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");           
            await Shell.Current.GoToAsync("..");       
        }
        private async void OnSignUpClicked(object obj)
        {
            

            try {
                
               
                IsBusy = true;
                if(SelectedCountryVM == null)
                {
                    Person.SelectedCountry = "";
                   
                }
                else { 
                    Person.SelectedCountry = SelectedCountryVM.Name; 
                }
                  
                var res =  await UserService.CreateUser(Person);
                if (!res.Status)
                {
                    var options = new ToastOptions
                    {
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Message = "Failed to create profile, contact systems admin !"

                        },
                        BackgroundColor = Color.FromHex("#2196F3"),
                        Duration = TimeSpan.FromSeconds(10),

                    };

                    //await App.Current.MainPage.DisplayToastAsync(options);
                    IsBusy = false;
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to create profile, contact systems admin !", "Cancel");
                    return;
                }
                var successMessage = new ToastOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.Red,
                        Message = "Profile created successfully. Redirecting to SignIn page !",
                        Font = Font.SystemFontOfSize(14).WithSize(20),
                        Padding= new Thickness(0, 0, 10,100),
                        
                        
                    },
                    BackgroundColor = Color.FromHex("#03fcc6"),
                    Duration = TimeSpan.FromSeconds(5), 
                    CornerRadius=1,
                    IsRtl=false,  
                    
                };
                IsBusy = false;
                await  App.Current.MainPage.DisplayToastAsync(successMessage);             
                await Shell.Current.GoToAsync($"//{nameof(SignInPage)}");                 
                
            }
            catch(Exception ex)
            { 
                throw ex;
            } 
            finally
            {
                Person pp = new Person();
                IsBusy = false;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            //await Shell.Current.GoToAsync($"//{nameof(ProfilePage)}");
        
            
        }

       
        private bool ValidateLogin(object obj)
        {
           
            
            return !String.IsNullOrWhiteSpace(Person.FirstName)
             && !String.IsNullOrWhiteSpace(Person.LastName)
             && !String.IsNullOrWhiteSpace(Person.Email)
             && !String.IsNullOrWhiteSpace(Person.Password)
             && !String.IsNullOrWhiteSpace(Person.RePassword)
             && Person.FirstNameValid && Person.LastNameValid && Person.EmailValid &&
             Person.PasswordValid && Person.RePasswordValid;

        }

        public bool DisplayToast
        {
            get => displayToast;
            set => SetProperty(ref displayToast, value);
        }

        public Country SelectedCountryVM
        {
            get => _selectedCountry;
            set
            {
                SetProperty(ref _selectedCountry, value);
                //OnItemSelected(value);
            }
        }

    }
}
