using CoffeeNext.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CoffeeNext.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}