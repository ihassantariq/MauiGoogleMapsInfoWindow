# Custom Google Maps Info Window in .NET MAUI

A .NET MAUI app showing how to build a fully custom info window when tapping a Google Maps pin — including platform-specific native hooks for Android and iOS.

## 📺 Video Tutorial

[![Custom Google Maps Info Window in .NET MAUI](https://img.youtube.com/vi/Dza9-zZukQM/maxresdefault.jpg)](https://youtu.be/Dza9-zZukQM?si=jzsXwzGNsO30TRw-)

## The Problem

[Onion.Maui.GoogleMaps](https://github.com/themronion/Maui.GoogleMaps) is a great MAUI wrapper over the native Google Maps SDKs, but it doesn't expose a public API to customize the info window that appears when you tap a pin. You can set a `Label` and `Address` on a `Pin`, but that's it — the callout UI is entirely controlled by the native SDK.

To show a fully custom card (with your own layout, fonts, and icons), you have to hook into the native map instance directly on each platform.

## Step 1 — Packing Data into the Pin

The native info window adapters only receive a `Marker` object — they can't directly access your MAUI model. The trick is to encode all the extra data you need into the `Pin.Address` field as a `||`-delimited string before adding the pin to the map:

```csharp
var pin = new Pin
{
    Label = place.Name,
    Address = $"{place.Address}||{place.Category}||{place.Rating:F1}||{place.OpeningHours}",
    Position = new Position(place.Latitude, place.Longitude),
    Tag = place  // keep the full object for navigation
};
```

On the native side, parse it back out:

```csharp
var parts = marker.Snippet.Split("||");
var address  = parts[0];
var category = parts[1];
var rating   = parts[2];
var hours    = parts[3];
```

## Step 2 — Hooking into the Native Map

MAUI's `HandlerChanged` event fires once the native map view is ready. This is where you attach the platform-specific adapter:

```csharp
MyMap.HandlerChanged += OnMapHandlerChanged;

private void OnMapHandlerChanged(object? sender, EventArgs e)
{
#if ANDROID
    SetupAndroidInfoWindow();
#elif IOS
    SetupIOSInfoWindow();
#endif
}
```

On **Android**, walk the native view hierarchy to find the `MapView`, call `GetMapAsync`, then set the adapter inside `OnMapReady`:

```csharp
googleMap.SetInfoWindowAdapter(new CustomInfoWindowAdapter(_mauiContext));
```

On **iOS**, access the native `Google.Maps.MapView` directly from the handler:

```csharp
if (MyMap.Handler?.PlatformView is Google.Maps.MapView mapView)
    mapView.MarkerInfoWindow = (_, marker) => CustomInfoWindowRenderer.BuildView(marker);
```

## Step 3 — Android: IInfoWindowAdapter

`CustomInfoWindowAdapter` implements `GoogleMap.IInfoWindowAdapter`. In `GetInfoWindow()`, instantiate your MAUI `ContentView`, set its properties, and convert it to a native Android view using `ToPlatformEmbedded`:

```csharp
public AndroidView? GetInfoWindow(Marker marker)
{
    var infoWindow = new PinInfoWindow();
    infoWindow.PlaceName = marker.Title;
    // parse snippet into address, category, rating, hours...
    return infoWindow.ToPlatformEmbedded(_mauiContext);
}
```

`GetInfoContents()` returns `null` — this tells the SDK to use `GetInfoWindow()` for the full window replacement instead of just the content area.

## Step 4 — iOS: Pure UIKit

On iOS, `ToPlatformEmbedded` **crashes** at runtime. MAUI's embedded rendering context conflicts with the Google Maps SDK's Metal pipeline (`BubbleBehavior::Commit`), even for a completely empty MAUI view. Setting `ClipsToBounds = true` on any subview triggers the same crash.

The only working approach is building the info window entirely in **UIKit**:

- `UIView` with `CALayer` shadow for the card
- `UILabel` instances for name, address, and the icon+text pairs
- `UIFont.FromName("FontAwesome6Free-Solid", 13)` to load FontAwesome icons
- A custom `TriangleView` that overrides `Draw()` with Core Graphics for the callout pointer

```csharp
mapView.MarkerInfoWindow = (_, marker) => CustomInfoWindowRenderer.BuildView(marker);
```

## Step 5 — The Info Window Design

The shared design (used directly on Android via XAML, mirrored manually in UIKit on iOS) is a card with:

- A rounded `Border` with a **purple accent bar** on the left edge
- **Place name** (bold, 18pt) and **address** (13pt, truncated to 2 lines)
- A bottom row with three icon+label pairs: category, rating, and opening hours
- FontAwesome Solid icons: `&#xf3c5;` (location), `&#xf005;` (star), `&#xf017;` (clock)
- A `Polygon` below the card forming the callout triangle pointer

## Step 6 — FontAwesome Icons

`fa_solid_900.ttf` is included in `Resources/Fonts/` and registered in `MauiProgram.cs`:

```csharp
fonts.AddFont("fa_solid_900.ttf", "FASolid");
```

In XAML (Android):

```xml
<Label Text="&#xf3c5;" FontFamily="FASolid" FontSize="14" TextColor="#512BD4"/>
```

In UIKit (iOS):

```csharp
var faFont = UIFont.FromName("FontAwesome6Free-Solid", 13);
iconLabel.Font = faFont;
iconLabel.Text = "";
```

Using FontAwesome instead of emoji ensures consistent rendering and full `TextColor` control across platforms — emoji ignore `TextColor` entirely.

## Step 7 — Navigating from the Info Window

Wire up `InfoWindowClicked` to navigate to a detail page. The full model object is stored in `Pin.Tag` so you don't need to re-parse the snippet:

```csharp
MyMap.InfoWindowClicked += async (_, e) =>
{
    if (e.Pin.Tag is PlacePin place)
        await Navigation.PushAsync(new PinDetailPage(place));
};
```

## Features

- Display multiple custom pins on a Google Map
- Tap a pin to view a fully custom-styled info window card
- Navigate to a detail page for each pin
- FontAwesome Solid icons with platform-consistent rendering
- Runs on Android, iOS, and Mac Catalyst

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or VS Code with the .NET MAUI workload
- A [Google Maps API key](https://developers.google.com/maps/documentation/android-sdk/get-api-key)

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/ihassantariq/MauiGoogleMapsInfoWindow.git
   cd MauiGoogleMapsInfoWindow
   ```

2. Open `Constants.cs` and replace `YOUR_GOOGLE_MAPS_API_KEY` with your actual Google Maps API key:
   ```csharp
   public const string GoogleMapsApiKey = "YOUR_GOOGLE_MAPS_API_KEY";
   ```

3. Build and run on your target platform:
   ```bash
   dotnet build
   ```

## Dependencies

- [Onion.Maui.GoogleMaps](https://github.com/themronion/Maui.GoogleMaps) — Google Maps control for .NET MAUI

## License

MIT
