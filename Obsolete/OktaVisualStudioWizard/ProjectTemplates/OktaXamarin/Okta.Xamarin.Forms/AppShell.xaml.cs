using System;
using System.Collections.Generic;
using $safeprojectname$.ViewModels;
using $safeprojectname$.Views;
using Xamarin.Forms;
using Okta.Xamarin;

namespace $safeprojectname$
{
    public partial class AppShell : global::Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnSignOutClicked(object sender, EventArgs e)
        {
            await OktaContext.Current.SignOut().ConfigureAwait(false);
        }
    }
}
