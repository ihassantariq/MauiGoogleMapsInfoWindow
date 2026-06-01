#if ANDROID
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using MauiGoogleMapsInfoWindow.Controls;
using Microsoft.Maui.Controls.Embedding;
using AndroidView = Android.Views.View;

namespace MauiGoogleMapsInfoWindow.Platforms.Android.CustomViews;

public class CustomInfoWindowAdapter : Java.Lang.Object, GoogleMap.IInfoWindowAdapter
{
    private readonly IMauiContext _mauiContext;

    public CustomInfoWindowAdapter(IMauiContext mauiContext)
    {
        _mauiContext = mauiContext;
    }

    public AndroidView? GetInfoWindow(Marker marker)
    {
        try
        {
            var infoWindow = new PinInfoWindow();

            infoWindow.PlaceName = marker.Title ?? "Unknown";

            var snippet = marker.Snippet ?? "";
            var parts = snippet.Split("||");

            infoWindow.Address = parts.Length > 0 && !string.IsNullOrEmpty(parts[0])
                ? parts[0] : "Unknown location";
            infoWindow.Category = parts.Length > 1 && !string.IsNullOrEmpty(parts[1])
                ? parts[1] : "Place";
            infoWindow.Rating = parts.Length > 2 && !string.IsNullOrEmpty(parts[2])
                ? parts[2] : "0.0";
            infoWindow.OpeningHours = parts.Length > 3 && !string.IsNullOrEmpty(parts[3])
                ? parts[3] : "";

            return infoWindow.ToPlatformEmbedded(_mauiContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CustomInfoWindow error: {ex.Message}");
            return null;
        }
    }

    public AndroidView? GetInfoContents(Marker marker) => null;
}
#endif
