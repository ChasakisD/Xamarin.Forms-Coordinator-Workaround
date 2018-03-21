# Xamarin.Forms-Coordinator-Workaround
A Xamarin.Forms (Android Only) Implementation of CoordinatorLayout with CollapsingToolbar

[![NuGet version](https://badge.fury.io/nu/XamarinForms.CoordinatorLayout.Android.svg)](https://badge.fury.io/nu/XamarinForms.CoordinatorLayout.Android)

# Documentation

![Alt Text](https://media.giphy.com/media/1gUWeJoxIm66WXaaqr/giphy.gif)

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
| Property | Type | Description |
|------------------|---------|-------------|
| `NestedContent` | `ContentPage` | The content of the NestedScrollView |
| `ImageSource` | `ImageSource` | The Source of the image that is used inside the CollapsingToolbar |
| `ToolbarBackgroundColor` | `Xamarin.Forms.Color` | The background color of the toolbar |
| `ChangeStatusBarColor` | `bool` | Type of bool, if you want to change the color of the status bar |
| `StatusBarColor` | `Xamarin.Forms.Color` | The background color of the status bar *(only if ChangeStatusBarColor is set to **true**)* |
| `CoordinatorScrimBackgroundColor` | `Xamarin.Forms.Color` | The color of the scrim effect in the collapsing toolbar |
| `CoordinatorBackgroundColor` | `Xamarin.Forms.Color` | The background color of the coordinator layout (the background of the image source) |
| `HasBackButton` | `bool` | If you want the toolbar have the back button |
| `IsFloatingButtonEnabled` | `bool` | If you want the floating action button to be enabled |
| `FloatingButtonImageSource` | `ImageSource` | The image source of the icon of the FAB *(only if IsFloatingButtonEnabled is set to **true**)* |
| `FloatingButtonBackgroundColor` | `Xamarin.Forms.Color` | The background color FAB *(only if IsFloatingButtonEnabled is set to **true**)* |
| `FloatingButtonCommand` | `ICommand` | The command that will execute when the user presses the FAB *(only if IsFloatingButtonEnabled is set to **true**)* |
