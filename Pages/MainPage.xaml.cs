using MauiGoogleMapsInfoWindow.Models;
using MauiGoogleMapsInfoWindow.Services;
using Maui.GoogleMaps;

namespace MauiGoogleMapsInfoWindow.Pages;

public partial class MainPage : ContentPage
{
    private readonly List<PlacePin> _places;

    public MainPage()
    {
        InitializeComponent();
        _places = DemoDataService.GetPlaces();

        // 🔑 Hook into HandlerChanged to set up native custom info window adapters
        MyMap.HandlerChanged += OnMapHandlerChanged;
        MyMap.InfoWindowClicked += OnInfoWindowClicked;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPins();
    }

    private void LoadPins()
    {
        MyMap.Pins.Clear();

        foreach (var place in _places)
        {
            var pin = new Pin
            {
                Label = place.Name,
                // 🔑 Pack extra data into Address using "||" delimiter
                // Format: "address||category||rating||openingHours"
                // The custom info window adapter parses this on the native side
                Address = $"{place.Address}||{place.Category}||{place.Rating:F1}||{place.OpeningHours}",
                Position = new Position(place.Latitude, place.Longitude),
                Tag = place // Still store the full object for navigation
            };

            MyMap.Pins.Add(pin);
        }

        // Move the map to show a global view
        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Position(30.0, 10.0),
            Distance.FromKilometers(5000)));
    }

    /// <summary>
    /// After the map handler is set, access the native map and attach the custom
    /// info window adapter (Android) or delegate (iOS).
    /// </summary>
    private void OnMapHandlerChanged(object? sender, EventArgs e)
    {
        if (MyMap.Handler is null)
            return;

#if ANDROID
        SetupAndroidInfoWindow();
#elif IOS
        SetupIOSInfoWindow();
#endif
    }

#if ANDROID
    private void SetupAndroidInfoWindow()
    {
        // Access the native Android MapView from the handler
        if (MyMap.Handler?.PlatformView is Android.Views.View platformView)
        {
            // The Maui.GoogleMaps handler wraps a MapView
            // We need to find the MapView in the view hierarchy and get the GoogleMap
            var mapView = FindMapView(platformView);
            if (mapView is not null)
            {
                mapView.GetMapAsync(new MapReadyCallback(Handler!.MauiContext!));
            }
        }
    }

    /// <summary>
    /// Recursively searches the view hierarchy for a MapView.
    /// </summary>
    private static Android.Gms.Maps.MapView? FindMapView(Android.Views.View view)
    {
        if (view is Android.Gms.Maps.MapView mapView)
            return mapView;

        if (view is Android.Views.ViewGroup viewGroup)
        {
            for (int i = 0; i < viewGroup.ChildCount; i++)
            {
                var child = viewGroup.GetChildAt(i);
                if (child is not null)
                {
                    var result = FindMapView(child);
                    if (result is not null)
                        return result;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Callback that fires when the native GoogleMap is ready.
    /// Sets the custom info window adapter.
    /// </summary>
    private class MapReadyCallback : Java.Lang.Object, Android.Gms.Maps.IOnMapReadyCallback
    {
        private readonly IMauiContext _mauiContext;

        public MapReadyCallback(IMauiContext mauiContext)
        {
            _mauiContext = mauiContext;
        }

        public void OnMapReady(Android.Gms.Maps.GoogleMap googleMap)
        {
            // 🔑 Set our custom info window adapter on the native GoogleMap
            googleMap.SetInfoWindowAdapter(
                new Platforms.Android.CustomViews.CustomInfoWindowAdapter(_mauiContext));
        }
    }
#endif

#if IOS
    private void SetupIOSInfoWindow()
    {
        // Access the native iOS MapView from the handler
        if (MyMap.Handler?.PlatformView is Google.Maps.MapView mapView)
        {
            mapView.MarkerInfoWindow = (_, marker) =>
                Platforms.iOS.CustomViews.CustomInfoWindowRenderer.BuildView(marker);
        }
    }
#endif

    private async void OnInfoWindowClicked(object? sender, InfoWindowClickedEventArgs e)
    {
        // 🔑 Retrieve the PlacePin from the Tag property
        if (e.Pin.Tag is PlacePin selectedPlace)
        {
            await Navigation.PushAsync(new PinDetailPage(selectedPlace));
        }
    }
}
