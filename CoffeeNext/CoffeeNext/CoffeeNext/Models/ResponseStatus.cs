using CoffeeNext.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeNext.Models
{
    public class ResponseStatus : BaseViewModel
    {
        private string message;
        private int errorCode;
        private bool status;
     

       

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);          
        }

        public int ErrorCode
        {
            get => errorCode;
            set => SetProperty(ref errorCode, value);
        }

        public bool Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

       
    }
}
