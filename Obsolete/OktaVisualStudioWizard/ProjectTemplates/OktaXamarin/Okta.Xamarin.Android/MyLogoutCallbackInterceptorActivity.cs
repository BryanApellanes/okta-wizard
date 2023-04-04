using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace $safeprojectname$
{
    [Activity(Label = "MyLogoutCallbackInterceptor")]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
            DataSchemes = new[] { "my.app.logout" },
            DataPath = "/callback"
        )
    ]
    public class MyLogoutCallbackInterceptorActivity: Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Android.Net.Uri uri_android = Intent.Data;

            if (global::Okta.Xamarin.OidcClient.InterceptLogoutCallback(new Uri(uri_android.ToString())))
            {
                this.Finish();
            }

            return;
        }
    }
}