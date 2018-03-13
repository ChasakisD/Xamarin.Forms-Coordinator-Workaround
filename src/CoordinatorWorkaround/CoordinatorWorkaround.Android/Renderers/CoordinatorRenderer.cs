using System;
using Android.Content;
using CoordinatorWorkaround.CustomViews;
using CoordinatorWorkaround.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CoordinatorView), typeof(CoordinatorRenderer))]
namespace CoordinatorWorkaround.Droid.Renderers
{
    public class CoordinatorRenderer : ViewRenderer
    {
        public CoordinatorRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                if(!(Context is FormsAppCompatActivity activity)) return;
                activity.SetContentView(Resource.Layout.Coordinator);

                var toolbar = activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolBar);
                if(toolbar == null) return;

                if (!(e.NewElement is CoordinatorView view)) return;

                var fragment = view.NestedContent?.CreateSupportFragment(activity);
                if(fragment == null)  return;

                activity.SupportFragmentManager
                    .BeginTransaction()
                    .Replace(Resource.Id.fragmentContainer, fragment)
                    .Commit();
                
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