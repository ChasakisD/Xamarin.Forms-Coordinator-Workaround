using Xamarin.Forms;

namespace CoordinatorWorkaround.CustomViews
{
    public class CoordinatorView : ContentView
    {
        public static readonly BindableProperty NestedContentProperty =
            BindableProperty.Create(
                nameof(NestedContent), 
                typeof(ContentPage), 
                typeof(CoordinatorView));

        public ContentPage NestedContent
        {
            get => (ContentPage)GetValue(NestedContentProperty);
            set => SetValue(NestedContentProperty, value);
        }
    }
}
