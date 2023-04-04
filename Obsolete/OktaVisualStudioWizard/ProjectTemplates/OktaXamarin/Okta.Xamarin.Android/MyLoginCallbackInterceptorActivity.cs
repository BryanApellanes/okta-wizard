using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace $safeprojectname$
{
    [Activity(Label = "MyLoginCallbackInterceptorActivity")]
    [IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
            DataSchemes = new[] { "my.app.login" },
            DataPath = "/callback"
        )
    ]
    public class MyLoginCallbackInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Android.Net.Uri uri_android = Intent.Data;

            if (global::Okta.Xamarin.OidcClient.InterceptLoginCallback(new Uri(uri_android.ToString())))
            {
                this.Finish();
            }

            return;
        }
    }
}