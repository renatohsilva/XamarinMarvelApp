
using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;
using FFImageLoading.Forms.Droid;

namespace MarvelApp.Droid
{
    [Activity(Label = "MarvelApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init(true);
            UserDialogs.Init(this);

            LoadApplication(new MarvelApp.App());
        }
    }
}

