using Okta.Xamarin.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace $safeprojectname$.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
            SignInCommand = new SignInCommand();
            SignOutCommand = new SignOutCommand();
        }

        public ICommand OpenWebCommand { get; }

        public SignInCommand SignInCommand { get; }
        public SignOutCommand SignOutCommand { get; set; }
    }
}