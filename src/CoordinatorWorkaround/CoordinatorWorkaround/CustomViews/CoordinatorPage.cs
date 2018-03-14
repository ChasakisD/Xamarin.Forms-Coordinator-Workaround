using Xamarin.Forms;

namespace CoordinatorWorkaround.CustomViews
{
    public class CoordinatorPage : ContentPage
    {
        public static readonly BindableProperty NestedContentProperty = BindableProperty.Create(
            nameof(NestedContent), typeof(ContentPage), typeof(CoordinatorPage));
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource), typeof(ImageSource), typeof(CoordinatorPage));

        public ContentPage NestedContent
        {
            get => (ContentPage)GetValue(NestedContentProperty);
            set => SetValue(NestedContentProperty, value);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public CoordinatorPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
