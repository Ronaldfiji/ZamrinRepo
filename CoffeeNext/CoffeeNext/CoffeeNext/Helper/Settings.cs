using CoffeeNext.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace CoffeeNext.Helper
{
    public class Settings 
    {
        
        public static Settings Instance { get; } = new Settings(); // To access non static method as static member
        //public static string Username => Preferences.Get("Username", string.Empty);
        public static string Username
        {
            get => Preferences.Get("Username", string.Empty);
            set => Preferences.Set("Username", value);    

        }
        public bool IsUserLoggedInStatus { get;set;}
        /*
        private bool _isUserLoggedIn ;
        
        public bool IsUserLoggedInStatus
        { 
            get { return false; }
            set => SetProperty(ref _isUserLoggedIn, value);
        }       
        protected bool SetProperty<T>(ref T backingStore, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "", System.Action onChanged = null)
        {
            if (System.Collections.Generic.EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        */
    }
}
