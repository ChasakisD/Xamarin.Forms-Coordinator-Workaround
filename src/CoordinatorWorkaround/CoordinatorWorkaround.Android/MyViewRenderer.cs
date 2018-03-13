using System;
using Android.Content;
using CoordinatorWorkaround;
using CoordinatorWorkaround.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyView), typeof(MyViewRenderer))]
namespace CoordinatorWorkaround.Droid
{
    public class MyViewRenderer : PageRenderer
    {
        public MyViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                if(!(Context is FormsAppCompatActivity activity)) return;
                activity.SetContentView(Resource.Layout.coordinator);

                var toolbar = activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolBar);
                if(toolbar == null) return;

                toolbar.Title = "Finally It Worked";
                activity.SetSupportActionBar(toolbar);
                activity.SupportActionBar?.SetDisplayHomeAsUpEnabled(true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}