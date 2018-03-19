using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
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
        private FormsAppCompatActivity _activity;
        private Android.Views.View _currentView;

        public CoordinatorRenderer(Context context) : base(context) { }

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

            if (e.PropertyName != nameof(CoordinatorPage.Title) &&
                e.PropertyName != nameof(CoordinatorPage.ImageSource) &&
                e.PropertyName != nameof(CoordinatorPage.ToolbarBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.StatusBarColor) &&
                e.PropertyName != nameof(CoordinatorPage.CoordinatorBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.CoordinatorScrimBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.IsFloatingButtonEnabled) &&
                e.PropertyName != nameof(CoordinatorPage.FloatingButtonImageSource) &&
                e.PropertyName != nameof(CoordinatorPage.FloatingButtonBackgroundColor) &&
                e.PropertyName != nameof(CoordinatorPage.ChangeStatusBarColor) &&
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
                if (!(Context is FormsAppCompatActivity activity)) return;

                _activity = activity;
                
                _currentView = _activity.LayoutInflater.Inflate(Resource.Layout.Coordinator, this, false);
                AddView(_currentView);
                
                var toolbar = _currentView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.myToolBar);
                if (toolbar == null) return;

                if (!(e.NewElement is CoordinatorPage view)) return;

                var fragment = view.NestedContent?.CreateSupportFragment(_activity);
                if (fragment == null) return;

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
            
            /* Toolbar Title */
            collBar.Title = view.Title;

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