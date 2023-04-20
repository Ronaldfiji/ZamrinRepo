using CoffeeNext.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeNext.Models
{
   
        public class LoginModel : BaseViewModel
        {
            private string password;
            private string email;
            private bool emailValid;
            private bool passwordValid;

            // [DataType(DataType.EmailAddress)]
            // [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
            // [Required(ErrorMessage = "You must provide email")]
            public string Email
            {
                get => email;
                set => SetProperty(ref email, value);
            }            
            public string Password
            {
                get => password;
                set => SetProperty(ref password, value);
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
        
    }
}
