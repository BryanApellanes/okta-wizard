using System.ComponentModel;
using Xamarin.Forms;
using $safeprojectname$.ViewModels;

namespace $safeprojectname$.Views
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