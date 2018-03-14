using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using CoordinatorWorkaround.CustomViews;
using CoordinatorWorkaround.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(CoordinatorPage), typeof(CoordinatorRenderer))]
namespace CoordinatorWorkaround.Droid.Renderers
{
    public class CoordinatorRenderer : PageRenderer
    {
        private FormsAppCompatActivity _activity;
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

            if (e.PropertyName != nameof(CoordinatorPage.NestedContent) &&
                e.PropertyName != nameof(CoordinatorPage.ImageSource) &&
                e.PropertyName != nameof(CoordinatorPage.ToolbarBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.CoordinatorBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.IsFloatingButtonEnabled) &&
                e.PropertyName != nameof(CoordinatorPage.FloatingButtonImageSource) &&
                e.PropertyName != nameof(CoordinatorPage.FloatingButtonBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.FloatingButtonCommand) &&
                e.PropertyName != nameof(CoordinatorPage.HasBackButton))
                return;


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

                _activity = activity;

                _currentView = _activity.LayoutInflater.Inflate(Resource.Layout.Coordinator, this, false);
                AddView(_currentView);

                _previousToolbar = _activity.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                var toolbar = _currentView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolBar);
                if(toolbar == null) return;

                if (!(e.NewElement is CoordinatorPage view)) return;

                var fragment = view.NestedContent?.CreateSupportFragment(_activity);
                if(fragment == null)  return;

                var fragmentContainer = _currentView.FindViewById<FrameLayout>(Resource.Id.fragmentContainer);
                _activity.SupportFragmentManager
                    .BeginTransaction()
                    .Replace(fragmentContainer.Id, fragment)
                    .Commit();

                /* Set Floating Button On Click */
                var floatingButton =
                    _currentView.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id
                        .floatingActionButton);
                floatingButton.Click += FloatingButtonClick;

                _activity.SetSupportActionBar(toolbar);
                
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

            /* Toolbar Background Color */
            var toolbar = 
                _currentView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id
                    .myToolBar);
            toolbar.SetBackgroundColor(view.ToolbarBackgroundColor.ToAndroid());

            /* Collapsing Toolbar Scrim Color */
            var collBar =
                _currentView.FindViewById<Android.Support.Design.Widget.CollapsingToolbarLayout>(Resource.Id
                    .collapsingToolBar);
            collBar.SetContentScrimColor(view.CoordinatorScrimBackgroundColor.ToAndroid());

            /* Status Bar Background Color */
            if (view.ChangeStatusBarColor)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    _activity.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                    _activity.Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                    _activity.Window.SetStatusBarColor(view.StatusBarColor.ToAndroid());
                }
            }
            
            /* Toolbar Title */
            toolbar.Title = view.Title;

            /* Coordinator Layout Background Color */
            var coordinator = 
                _currentView.FindViewById<Android.Support.Design.Widget.CollapsingToolbarLayout>(Resource.Id
                    .collapsingToolBar);
            coordinator.SetBackgroundColor(view.CoordinatorBackgroundColor.ToAndroid());

            /* Floating Action Button Visibility */
            var floatingButton =
                _currentView.FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id
                    .floatingActionButton);
            floatingButton.Visibility = view.IsFloatingButtonEnabled ? ViewStates.Visible : ViewStates.Gone;
            
            /* Floating Action Button Icon */
            var floatingBitmap = await GetBitmapFromImageSourceAsync(view.FloatingButtonImageSource, _currentView.Context);
            floatingButton.SetImageBitmap(floatingBitmap);

            /* Floating Action Button Background Color */
            floatingButton.BackgroundTintList = ColorStateList.ValueOf(view.FloatingButtonBackgroundColor.ToAndroid());

            /* Has Back Button */
            _activity.SupportActionBar?.SetDisplayHomeAsUpEnabled(view.HasBackButton);

            /* Coordinator Image Source */
            var imageView = _currentView.FindViewById<ImageView>(Resource.Id.coordinatorImageView);
            var imageBitmap = await GetBitmapFromImageSourceAsync(view.ImageSource, _currentView.Context);
            imageView.SetImageBitmap(imageBitmap);
        }

        private void FloatingButtonClick(object sender, EventArgs e)
        {
            if (!(Element is CoordinatorPage view)) return;

            /* Call Floating Button Command */
            if (view.FloatingButtonCommand == null) return;
            if (view.FloatingButtonCommand.CanExecute(null))
            {
                view.FloatingButtonCommand.Execute(null);
            }
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