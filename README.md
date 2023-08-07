<img src="Westwind.Wpf.Statusbar/icon.png" height=64 align=right />

# WPF Statusbar Helper and Control
 
 <a href="https://www.nuget.org/packages/Westwind.Wpf.Statusbar/">![](https://img.shields.io/nuget/v/Westwind.Wpf.Statusbar.svg)</a> ![](https://img.shields.io/nuget/dt/Westwind.Wpf.Statusbar.svg)


This is a small WPF library that provides Statusbar functionality in a couple of different ways. 

* A basic statusbar control
* A Statusbar Helper you can apply against your own status bars


![](ScreenCapture.gif)

## Features

* Easy to use, single method status updates from anywhere in your app
* Status relevant icons change based on display mode
* Animated icon 'flashing' when initially rendered (optional)
* Spinning icon support typically for progress operations
* Status text and icon can expire and revert to default after timeout
* Immediate UI update handling even in synchronous code  
* Customize icons globally, per instance or per method using `ImageSource`

## Installation and base Usage
You can install this library from NuGet:

```ps
> dotnet add package Westwind.Wpf.Statusbar
```

To use the control add the following namespace to WPF Windows or controls:

```xml
<Window x:Class="SampleApp.MainWindow" ...
        xmlns:statusbar="clr-namespace:Westwind.Wpf.Statusbar;assembly=Westwind.Wpf.Statusbar"
>
```

And to use the control in a Window or Control:

```xml
<statusbar:StatusbarControl Grid.Row="1" Name="Statusbar" />
````

Alternately you can use the `StatusbarHelper` with your own existing Statusbar, provided it has an icon `Image` and a main `TextBlock`. You can attach the `StatusbarHelper` to the parent control or window and pass in the `Textblock` control and `Image` icon control which is then automated.

```csharp
 public partial class MyWindow : Window
 {
    public StatusbarHelper Status { get;  }

    public MyWindow()
    {
        InitializeComponent();
            
        Status = new StatusbarHelper(StatusText, StatusIcon);
        
        ...
    } 
 }        
```

To update status messages:

```csharp
// Using the control: shows status and resets to default  after 3 secs
Statusbar.ShowStatusSuccess("Yay. The operation was successful! ", 3000);

// Using the control: shows status and resets to default  after 2 secs
Statusbar.ShowStatusError("Ooops. Something went wrong!",2000);

// Using StatusHelper: shows status and resets to default  after 2 secs
Status.ShowStatusWarning("Careful... this might go sideways.", 2000);

// Using StatusHelper: shows spinning icon indefinitely
Status.ShowStatusProgress("This may take a minute...");
```


## Features

This library provides:

* **Simple Statusbar Control**  
A basic status bar control that has an icon plus 3 status bar panels that can be individually accessed. The control only manages the the icon and main status text. 

* **A Statusbar Helper**  
This helper allows you to attach the key features of the Statusbar control to a XAML layout of your own by explicitly passing in the icon image control and primary text control.

Both of these tools provide the following features:

* Simple ShowStatus methods to display common status modes
* Modes:  Success, Error, Warning, Progress
* Icons to display common states
* Icon animation for state change
* Animated progress icon
* Icon and text reversion to a default message and icon
* Stock icons provided, but you can override with any ImageSource


## Usage

### Statusbar Control
The status bar control can just be dropped onto a form by adding the following namespace:

```xml
<Window x:Class="SampleApp.MainWindow" ...
        xmlns:statusbar="clr-namespace:Westwind.Wpf.Statusbar;assembly=Westwind.Wpf.Statusbar"
>
```

and the actual control:

```xml
<statusbar:StatusbarControl Grid.Row="1" Name="Statusbar" />
````

In a window it looks like this:

```xml
<Window x:Class="SampleApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        
        xmlns:statusbar="clr-namespace:Westwind.Wpf.Statusbar;assembly=Westwind.Wpf.Statusbar"
        
        Title="MainWindow" 
        Height="400" Width="450">


    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height ="Auto"/>
        </Grid.RowDefinitions>

        <StackPanel Width="200" Margin="20">
        ... Content here
        </StackPanel>

        <statusbar:StatusbarControl Grid.Row="1" Name="Statusbar" />
    </Grid>
</Window>
```

#### Showing Status Messages
Once the control is on the page you can simply call the properties to manipulate the status behavior.

The following are examples that demonstrate the basic operations *(note you wouldn't run them one after the other like this but each before/after an operation has completed)*

```csharp
// shows status and resets to default  after 3 secs
Statusbar.ShowStatusSuccess("Yay. The operation was successful! ", 3000);

// shows status and resets to default  after 2 secs
Statusbar.ShowStatusError("Ooops. Something went wrong!",2000);

// shows status and resets to default  after 2 secs
Statusbar.Status.ShowStatusWarning("Careful... this might go sideways.", 2000);

// shows spinning icon indefinitely
Statusbar.Status.ShowStatusProgress("This may take a minute...");
```

Note that you can either use the control's methods directly (first two examples) or you can use the `Status` property which is the `StatusbarHelper` instance that does the actual work and can also be used independently without this control. 

#### Updating Non-Primary Panels
The various status methods can be used to update the status bar's primary text panel and icon, but you can also set the optional center and right panels and assign text or content.

```csharp
// set with plain text
Statusbar.SetStatusCenter("Center Panel Text");

// set with control content
var sp = new StackPanel() { Orientation = Orientation.Horizontal };
sp.Children.Add(new TextBlock() { Text = "Right Panel Text" });
sp.Children.Add(new Image { Source = StatusIcons.Default.SuccessIcon, Height=15, Margin = new Thickness(3, 0 ,0, 0)});

Statusbar.SetStatusRight(sp);

// Update the primary panel's text only
Statusbar.StatusText.Text = "Pull it!";
```

#### Modifying the Statusbar Layout
The status bar control is a UserControl with an embedded `Statusbar` control which is named - `Statusbar` that you can directly access and manipulate. This means you can optionally customize the layout by adding or removing panels and adding custom content beyond the base methods.

However, my recommendation is that if you are going to make heavy customizations to the status bar, skip the control and use the `StatusbarHelper` on your own custom layout in your host Window or control instead. As long as you have an icon `Image` and main panel `TextBlock` you can use the `StatusbarHelper` to get all feature benefits plus full control over your StatusBar layout.

### Statusbar Helper
The statusbar control is a quick way to drop a basic statusbar control on a page, but if you want more control over your status bar layout you can also create and **manage your own Status bar XAML layout**.

In order to use the `StatusbarHelper` class you need to **provide the primary text control and an image control**.

For example you can embed a status bar control like this into your own Xaml:

```xml
<!-- if you use default icon resources you have to add the resources -->
<Window x:Class="SampleApp.MainWindow" ...
        xmlns:statusbar="clr-namespace:Westwind.Wpf.Statusbar;assembly=Westwind.Wpf.Statusbar"
>
<Window.Resources>
    <ResourceDictionary Source="Assets/icons.xaml" />
</Window.Resources>
   ...
<StatusBar  
    Grid.Row="3" Height="30"  
    VerticalAlignment="Bottom" HorizontalAlignment="Stretch">
    <StatusBar.ItemsPanel>
        <ItemsPanelTemplate>
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="Auto"  />
                </Grid.ColumnDefinitions>
            </Grid>
        </ItemsPanelTemplate>
    </StatusBar.ItemsPanel>
 
    <!-- initial image resource from built in resources - has to be named and passed into helper -->
    <StatusBarItem Grid.Column="0" Margin="2,1,0,0">
        <Image x:Name="StatusIcon" Source="{StaticResource circle_greenDrawingImage}"  Height="15" Width="15" Margin="0"  />
    </StatusBarItem>
    
    <!-- Main panel text block - has to be named and passed into the helper -->
    <StatusBarItem Grid.Column="1">
        <TextBlock Name="StatusText" x:FieldModifier="public" HorizontalAlignment="Left">Ready</TextBlock>
    </StatusBarItem>
    
    <!-- other custom layout that you can do whatever you want with -->
    <StatusBarItem Grid.Column="2">
        <ContentControl Name="StatusCenter" 
                   Margin="10 0"
                   x:FieldModifier="public" HorizontalAlignment="Left" />
    </StatusBarItem>
    <StatusBarItem Grid.Column="3">
        <ContentControl x:Name="StatusRight" x:FieldModifier="public" HorizontalAlignment="Right" Margin="0 0 5 0" />
    </StatusBarItem>
</StatusBar>
```

> #### Status Bar Recommendations
> For best effect, there are a couple of recommendations for any custom status bars you use with `StatusHelper`:
>
>
> * Make the Statusbar a **fixed height** to avoid resizing on Flashing
> * Make the icon a fixed height and width (square ideally)

In the constructor of Window or Control that hosts this control you can then assign the `StatusbarHelper` as a property like this:


```csharp
 public partial class MyWindow : Window
 {
    public StatusbarHelper Status { get;  }

    public MyWindow()
    {
        InitializeComponent();
            
        Status = new StatusbarHelper(StatusText, StatusIcon);
        
        ...
    } 
 }        
```

To update status information you can then run commands like this:

```csharp
private void BtnSuccess_OnClick(object sender, RoutedEventArgs e)
{
    // use the controls methods
    this.Status.ShowStatusSuccess("Yay. The operation was successful! ", 3000);
}
```

## Using Custom Icons
The status bar uses default icons that are internally provided via a XAML resource. Icons are changeable via `ImageSource` instances meaning that you can override icons with many type of images such as bitmaps, XAML resources, icons, and even from font libraries like FontAwesome.

### Default Icon Configuration - how it works
By default the `StatusbarHelper` and `StatusbarControl` are using default icons that are embedded as XAML resources in `icons.xaml` and that are assigned as `DrawingImage` instances to the `ImageSource` typed properties of `StatusIcons`. The default resource icons are accessible via the following resource path and resource keys:

```cs
public StatusIcons()
{
    var dict = new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/Westwind.Wpf.Statusbar;component/Assets/icons.xaml")
    };
    if (!Application.Current.Resources.Contains("circle_greenDrawingImage"))
        Application.Current.Resources.MergedDictionaries.Add(dict);

    DefaultIcon = dict["circle_greenDrawingImage"] as DrawingImage;
    SuccessIcon = dict["circle_checkDrawingImage"] as DrawingImage;
    ErrorIcon = dict["triangle_exclamationDrawingImage"] as DrawingImage;
    WarningIcon = dict["triangle_warningDrawingImage"] as DrawingImage;
    ProgressIcon = dict["circle_notchDrawingImage"] as DrawingImage;
}
```

These icons are exposed as the default in:

```cs
StatusbarHelper.StatusIcons = StatusbarIcons.Default;
```

All icons are `ImageSource` objects, so they can be replaced with any new image source, which can come from other XAML resources, bitmap images or even from other tools like the `FontAwesome6` library - anything that can produce an `ImageSource`.

### Overriding Default Icons
There are three ways to override the default icons:

* Globally - Overide the `StatusIcon.Default`  Icon set
* Per Control - Override the `StatusbarHelper.StatusIcons` Icon set
* Per Call - Override the `imageSource` parameter on the various `ShowStatusXXX()` calls

#### Globally override StatusIcon.Default
The `StatusIcon.Default` static object contains the default icons that are used by default and are assigned by default to a new instance of the `StatusbarHelper` and by extension the `StatusbarControl` which uses the helper for rendering. 

You can assign a new set of icons to the `StatusIcon.Default` class which contains `ImageSource` properties for each of the various icons. By overriding these ImageSource properties before any controls or helpers are created you are effectively globally overriding the value all icons that are rendered.

For example, to override the `SuccessIcon` you could use the following code:

```cs
// ImageAwesome creates a DrawingImage control
image = new ImageAwesome()
{
    PrimaryColor = Brushes.ForestGreen,
    Height = 15,
    Icon = EFontAwesomeIcon.Solid_SquareCheck
};

// globally override SuccessIcon for all instance that use the default icons
StatusIcons.Default.SuccessIcon = image.Source;
```

#### Override StatusbarHelper Instance
If you want to override behavior of a single `StatusbarHelper` or `StatusbarControl` you can do so via the Helper's `StatusIcons` class. You access it both directly on a `StatusbarHelper.StatusIcons` instance or on the `StatusbarControl.Status.StatusIcons` property.


Here's an example using the [FontAwesome6 library](https://github.com/MartinTopfstedt/FontAwesome6) which among other things can create ImageSources from FontAwesome icons using the `ImageAwesome` class. Here I'm assigning to a `StatusbarControl` and it's `StatusIcons` at the control level.

```csharp
private void BtnToggleIcons_OnChecked(object sender, RoutedEventArgs e)
{
    ToggleState = !ToggleState;

    if (ToggleState)
    {
        ActiveIconSet = "Custom Images: Font Awesome 6";

        // You can override the control or StatusbarHelper icons

        // Create and assign a NEW icon set so we don't overwrite default icons
        var icons = new StatusIcons();
        Statusbar.Status.StatusIcons = icons;
        
        // if you want to retain existing icons assign them
        // icons.DefaultIcon = StatusIcons.Default.DefaultIcon

        // create new custom icons for the error icon from FontAwesome6 icons
        var image = new ImageAwesome()
        {
            PrimaryColor = Brushes.Green,
            Height = 15,
            Icon = EFontAwesomeIcon.Solid_House
        };
        icons.DefaultIcon = image.Source;

        image = new ImageAwesome()
        {
            PrimaryColor = Brushes.ForestGreen,
            Height = 15,
            Icon = EFontAwesomeIcon.Solid_SquareCheck
        };
        icons.SuccessIcon = image.Source;

        // create a custom icon for the error icon from FontAwesome6 icons
        image = new ImageAwesome()
        {
            PrimaryColor = Brushes.DarkGoldenrod,
            Height = 15,
            Icon = EFontAwesomeIcon.Solid_CircleRadiation
        };
        icons.WarningIcon = image.Source;

        image = new ImageAwesome()
        {
            PrimaryColor = Brushes.Firebrick,
            Height = 15,
            Icon = EFontAwesomeIcon.Solid_CircleExclamation
        };
        icons.ErrorIcon = image.Source;

        image = new ImageAwesome()
        {
            PrimaryColor = Brushes.SteelBlue,
            Height = 15,
            Icon = EFontAwesomeIcon.Solid_Spinner
        };
        icons.ProgressIcon = image.Source;
    }
    else
    {
        ActiveIconSet = "Default Xaml Icons";
        Statusbar.Status.StatusIcons = StatusIcons.Default;
    }

    btnToggleIcons.Content = ActiveIconSet;
    Statusbar.Status.SetStatusIcon();

    // Alternately you can also override the StatusIcon.Default icons 
    // and have them overridden anywhere the default icons are used
    // as a 'global' icon override:
    //
    // StatusbarIcons.DefaultIcon = image.Source;  // overrides anywhere the default is used

}
```

If you're using a status bar helper directly you'd instead use:

```cs
StatusHelper.StatusIcons = icons;
```

#### Per call Icon Overrides
Each of the control and helper `ShowStatusXXX()` methods have a parameter override that let you specify an `imageSource` parameter that effectively lets you use any icon you want for just a single call.

```csharp
private async void BtnRaw_OnClick(object sender, RoutedEventArgs e)
{
    var image = new ImageAwesome()
    {
        PrimaryColor = Brushes.SteelBlue,
        Height = 15,
        Icon = EFontAwesomeIcon.Solid_Spinner
    };
    
    // Use the custom ImageAwesome source 
    Statusbar.Status.ShowStatusProgress("Custom icon spinner (from FontAwesome)",
                                       imageSource: image.Source, spin: true);
}
```


## License
This library is published under **MIT license** terms.

**Copyright &copy; 2023 Rick Strahl, West Wind Technologies**

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.