using System.Windows.Input;
using Xamarin.Forms;

namespace CoordinatorWorkaround.CustomViews
{
    public class CoordinatorPage : ContentPage
    {
        public static readonly BindableProperty NestedContentProperty = BindableProperty.Create(
            nameof(NestedContent), typeof(ContentPage), typeof(CoordinatorPage));
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource), typeof(ImageSource), typeof(CoordinatorPage));
        public static readonly BindableProperty ToolbarBackgroundColorProperty = BindableProperty.Create(
            nameof(ToolbarBackgroundColor), typeof(Color), typeof(CoordinatorPage), Color.DimGray);
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create(
            nameof(StatusBarColor), typeof(Color), typeof(CoordinatorPage), Color.DimGray);
        public static readonly BindableProperty CoordinatorBackgroundColorProperty = BindableProperty.Create(
            nameof(CoordinatorScrimBackgroundColor), typeof(Color), typeof(CoordinatorPage), Color.White);
        public static readonly BindableProperty CoordinatorScrimBackgroundColorProperty = BindableProperty.Create(
            nameof(CoordinatorBackgroundColor), typeof(Color), typeof(CoordinatorPage), Color.White);
        
        public static readonly BindableProperty IsFloatingButtonEnabledProperty = BindableProperty.Create(
            nameof(IsFloatingButtonEnabled), typeof(bool), typeof(CoordinatorPage), true);
        public static readonly BindableProperty FloatingButtonImageSourceProperty = BindableProperty.Create(
            nameof(FloatingButtonImageSource), typeof(ImageSource), typeof(CoordinatorPage));
        public static readonly BindableProperty FloatingButtonBackgroundColorProperty = BindableProperty.Create(
            nameof(FloatingButtonBackgroundColor), typeof(Color), typeof(CoordinatorPage), Color.Red);
        public static readonly BindableProperty FloatingButtonCommandProperty = BindableProperty.Create(
            nameof(FloatingButtonCommand), typeof(ICommand), typeof(CoordinatorPage));

        public static readonly BindableProperty ChangeStatusBarColorProperty = BindableProperty.Create(
            nameof(ChangeStatusBarColor), typeof(bool), typeof(CoordinatorPage), true);
        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(
            nameof(HasBackButton), typeof(bool), typeof(CoordinatorPage), true);

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

        public Color CoordinatorBackgroundColor
        {
            get => (Color)GetValue(CoordinatorBackgroundColorProperty);
            set => SetValue(CoordinatorBackgroundColorProperty, value);
        }

        public Color CoordinatorScrimBackgroundColor
        {
            get => (Color)GetValue(CoordinatorScrimBackgroundColorProperty);
            set => SetValue(CoordinatorScrimBackgroundColorProperty, value);
        }

        public Color ToolbarBackgroundColor
        {
            get => (Color)GetValue(ToolbarBackgroundColorProperty);
            set => SetValue(ToolbarBackgroundColorProperty, value);
        }

        public Color StatusBarColor
        {
            get => (Color)GetValue(StatusBarColorProperty);
            set => SetValue(StatusBarColorProperty, value);
        }

        public bool IsFloatingButtonEnabled
        {
            get => (bool)GetValue(IsFloatingButtonEnabledProperty);
            set => SetValue(IsFloatingButtonEnabledProperty, value);
        }

        public ImageSource FloatingButtonImageSource
        {
            get => (ImageSource)GetValue(FloatingButtonImageSourceProperty);
            set => SetValue(FloatingButtonImageSourceProperty, value);
        }

        public Color FloatingButtonBackgroundColor
        {
            get => (Color)GetValue(FloatingButtonBackgroundColorProperty);
            set => SetValue(FloatingButtonBackgroundColorProperty, value);
        }

        public ICommand FloatingButtonCommand
        {
            get => (ICommand)GetValue(FloatingButtonCommandProperty);
            set => SetValue(FloatingButtonCommandProperty, value);
        }

        public bool ChangeStatusBarColor
        {
            get => (bool)GetValue(ChangeStatusBarColorProperty);
            set => SetValue(ChangeStatusBarColorProperty, value);
        }

        public bool HasBackButton
        {
            get => (bool)GetValue(HasBackButtonProperty);
            set => SetValue(HasBackButtonProperty, value);
        }

        public CoordinatorPage()
        {
            /* Do not remove, it is going to have duplicate toolbar */
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
