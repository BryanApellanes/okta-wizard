﻿using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace $safeprojectname$.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.ProfileViewModel(this);
			if(Instance == null)
			{
				Instance = this;
			}
        }

		public static ProfilePage Instance { get; set; }

		public void SetClaims(Dictionary<string, object> claims)
		{
			StackLayout claimsLayout = (StackLayout)this.FindByName("Claims");
			claimsLayout.Children.Clear();
			foreach (string key in claims.Keys)
			{
				Label label = new Label { Text = key };
				label.FontSize = Device.GetNamedSize(NamedSize.Medium, label);
				Label value = new Label { Text = claims[key]?.ToString() };
				value.FontSize = Device.GetNamedSize(NamedSize.Small, value);
				
				claimsLayout.Children.Add(label);
				claimsLayout.Children.Add(value);
			}
		}
    }
}
