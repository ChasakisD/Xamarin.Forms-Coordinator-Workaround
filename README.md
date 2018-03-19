# Xamarin.Forms-Coordinator-Workaround
A Xamarin.Forms (Android Only) Implementation of CoordinatorLayout with CollapsingToolbar

[![NuGet version](https://badge.fury.io/nu/XamarinForms.CoordinatorLayout.Android.svg)](https://badge.fury.io/nu/XamarinForms.CoordinatorLayout.Android)

# Documentation
## Installation

#### Xamarin.Android
Initialize the renderer below the Xamarin.Forms.Init
```cs
global::Xamarin.Forms.Forms.Init(this, bundle);
CoordinatorWorkaround.Droid.Renderers.Forms.Init();

```

#### Xamarin.Forms (.NetStandard)
Create the page as normal page and navigate to it
```cs
var page = new CoordinatorPage
{
    Title = "Finally Worked Monkeys!",
    NestedContent = //the instance of my ContentPage here,
    ImageSource = "my image source here",
    StatusBarColor = Color.DarkMagenta,
    ToolbarBackgroundColor = Color.DimGray,
    CoordinatorBackgroundColor = Color.Green,
    CoordinatorScrimBackgroundColor = Color.Red,
    FloatingButtonImageSource = "my image source here",
    FloatingButtonBackgroundColor = Color.DarkRed,
    FloatingButtonCommand = new Command(async () => await DisplayAlert("hehe", "hehe", "ok"), () => true),
    ChangeStatusBarColor = true,
    HasBackButton = true
};
```

## Bindable Properties
#### NestedContent
Type of ContentPage, the content of the NestedScrollView

#### ImageSource
The Source of the image that is used inside the CollapsingToolbar

#### ToolbarBackgroundColor
Type of Xamarin.Forms.Color, the background color of the toolbar

#### ChangeStatusBarColor
Type of bool, if you want to change the color of the status bar

#### StatusBarColor
Type of Xamarin.Forms.Color, the background color of the status bar (Only if ChangeStatusBarColor is set to true)

#### CoordinatorScrimBackgroundColor
Type of Xamarin.Forms.Color, the color of the scrim effect in the collapsing toolbar

#### CoordinatorBackgroundColor
Type of Xamarin.Forms.Color, the background color of the coordinator layout (the background of the image source)

#### HasBackButton
Type of bool, if you want the toolbar have the back button

#### IsFloatingButtonEnabled
Type of bool, if you want the floating action button to be enabled

#### FloatingButtonImageSource
The image source of the icon of the FAB *(only if IsFloatingButtonEnabled is set to **true**)*

#### FloatingButtonBackgroundColor
Type of Xamarin.Forms.Color, the background color FAB *(only if IsFloatingButtonEnabled is set to **true**)*

#### FloatingButtonCommand
Type of ICommand, the command that will execute when the user presses the FAB *(only if IsFloatingButtonEnabled is set to **true**)*