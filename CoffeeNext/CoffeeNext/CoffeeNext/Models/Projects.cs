using CoffeeNext.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoffeeNext.Models
{
    public class Projects : BaseViewModel
    {
        public int Id;            
        public string projectName;
        public string code;

        public string description;

        public DateTime? startDate;

        public DateTime? endDate;
        public ICollection<Person> applicationUser;
        public int ID
        {
            get => Id;
            set => SetProperty(ref Id, value);
        }

        [JsonProperty("Name")]
        public string ProjectName
        {
            get => projectName;
            set => SetProperty(ref projectName, value);

        }

        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }
        public string Description
        {
            get => description;
            //set => SetProperty(ref description, value);
           // get { return firstName; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        public DateTime? StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }
        public DateTime? EtartDate
        {
            get => endDate;
            set => SetProperty(ref endDate, value);
        }
        public virtual ICollection<Person> ApplicationUsers
        {
            get => applicationUser;
            set => SetProperty(ref applicationUser, value);
        }

       
        

    }
}
