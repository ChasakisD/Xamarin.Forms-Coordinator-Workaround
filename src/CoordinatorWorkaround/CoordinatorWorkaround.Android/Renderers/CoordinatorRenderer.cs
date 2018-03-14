using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using CoordinatorWorkaround.CustomViews;
using CoordinatorWorkaround.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CoordinatorPage), typeof(CoordinatorRenderer))]
namespace CoordinatorWorkaround.Droid.Renderers
{
    public class CoordinatorRenderer : PageRenderer
    {
        private Android.Views.View _currentView;
        private Android.Support.V7.Widget.Toolbar _previousToolbar;

        public CoordinatorRenderer(Context context) : base(context) { }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_previousToolbar == null) return;
            if (!(Context is FormsAppCompatActivity activity)) return;

            activity.SetSupportActionBar(_previousToolbar);
        }
       
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (_currentView != null)
            {
                var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
                var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

                _currentView.Measure(msw, msh);
                _currentView.Layout(0, 0, r - l, b - t);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null)
                return;
            
            if (e.PropertyName != nameof(CoordinatorPage.ImageSource)) return;
            UpdateCoordinatorLayout();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                if(!(Context is FormsAppCompatActivity activity)) return;

                _currentView = activity.LayoutInflater.Inflate(Resource.Layout.Coordinator, this, false);
                AddView(_currentView);

                _previousToolbar = activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                var toolbar = _currentView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolBar);
                if(toolbar == null) return;

                if (!(e.NewElement is CoordinatorPage view)) return;

                var fragment = view.NestedContent?.CreateSupportFragment(activity);
                if(fragment == null)  return;

                var fragmentContainer = _currentView.FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
                activity.SupportFragmentManager
                    .BeginTransaction()
                    .Replace(fragmentContainer.Id, fragment)
                    .Commit();
                
                toolbar.Title = "Finally It Worked";
                activity.SetSupportActionBar(toolbar);
                activity.SupportActionBar?.SetDisplayHomeAsUpEnabled(true);

                UpdateCoordinatorLayout();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void UpdateCoordinatorLayout()
        {
            if (!(Element is CoordinatorPage view)) return;
            if (view.ImageSource == null) return;

            var imageView = _currentView.FindViewById<ImageView>(Resource.Id.coordinatorImageView);
            var bm = await GetBitmapFromImageSourceAsync(view.ImageSource, _currentView.Context);
            imageView.SetImageBitmap(bm);
        }

        #region [Helper Methods]

        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        public static async Task<Bitmap> GetBitmapFromImageSourceAsync(ImageSource source, Context context)
        {
            var handler = GetHandler(source);

            return await handler.LoadImageAsync(source, context);
        }

        #endregion
        
    }
}