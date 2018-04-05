using Android.App;
using Android.Content.PM;
using Android.OS;

namespace CoordinatorSample.Droid
{
    [Activity(Label = "CoordinatorSample", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            Xamarin.Forms.Forms.Init(this, bundle);

            CoordinatorWorkaround.Droid.Renderers.Forms.Init();

            LoadApplication(new CoordinatorSample.App());
        }
    }
}

