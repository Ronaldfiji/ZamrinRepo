using CoffeeNext.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeNext.Models
{
    public class Person : BaseViewModel
    {
        private string firstname;
        private string lastname;
        private string phoneNumber;
        private string email;
        private string gender = String.Empty;
        private string city = String.Empty;
        private string state = String.Empty;
        private string zipcode = String.Empty;
        private string residentialAddress = String.Empty;
        private string dob  = String.Empty;
        private string selectedCountry = String.Empty;
        private List<Country> countriesList = new List<Country>();

        private string password;
        private string rePassword;

        private bool firstNameValid;
        private bool lastNameValid;
        private bool emailValid;
        private bool phoneNumberValid;
        private bool passwordValid;
        private bool rePasswordValid;
        private bool zipCodeValid;
        private bool cityValid;
        private bool stateValid;
        private bool residentialAddressValid;
        public Person()
        {
            CountriesList = (List<Country>) CountryDataStore.GetCountriesList().Result;
        }
        public string FirstName
        {
            get => firstname;
            set => SetProperty(ref firstname, value);
            //set { firstname = value; OnPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }        
        public string DOB 
        {
            get => dob;
            set => SetProperty(ref dob, value);
        }
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }       
        public string ZipCode
        {
            get => zipcode;
            set => SetProperty(ref zipcode, value);
        } 
        public string ResidentialAddress
        {
            get => residentialAddress;
            set => SetProperty(ref residentialAddress, value);
        }
        [JsonProperty("Country")]
        public string SelectedCountry
        {
            get => selectedCountry;
            set => SetProperty(ref selectedCountry, value);
            // set { firstname = country; OnPropertyChanged(nameof(SelectedCountry)); }           
        }
        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string RePassword
        {
            get => rePassword;
            set => SetProperty(ref rePassword, value);
        }
       

        public bool FirstNameValid
        {
            get => firstNameValid;
            set => SetProperty(ref firstNameValid, value);
        }
        public bool LastNameValid
        {
            get => lastNameValid;
            set => SetProperty(ref lastNameValid, value);
        }
        public bool PhoneNumberValid
        {
            get => phoneNumberValid;
            set => SetProperty(ref phoneNumberValid, value);
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
        public bool ZipCodeValid
        {
            get => zipCodeValid;
            set => SetProperty(ref zipCodeValid, value);
        }
        public bool CityValid
        {
            get => cityValid;
            set => SetProperty(ref cityValid, value);
        }
        public bool StateValid
        {
            get => stateValid;
            set => SetProperty(ref stateValid, value);
        }
        public bool ResidentialAddressValid
        {
            get => residentialAddressValid;
            set => SetProperty(ref residentialAddressValid, value);
        }
        public List<Country> CountriesList
        {
            get => countriesList.OrderBy(p => p.Name).ToList();

            set => SetProperty(ref countriesList, value);
        }
        public void ClearFields()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Gender = string.Empty;           
            SelectedCountry = string.Empty;
            Password = string.Empty;
            RePassword = string.Empty;
            City = string.Empty;
            ResidentialAddress = string.Empty;
            ZipCode = string.Empty;
            State = string.Empty;           

        }    
    }  

}
