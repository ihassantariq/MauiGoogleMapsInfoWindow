using MauiGoogleMapsInfoWindow.Models;

namespace MauiGoogleMapsInfoWindow.Pages;

public partial class PinDetailPage : ContentPage
{
    public PinDetailPage(PlacePin place)
    {
        InitializeComponent();
        PopulateUI(place);
    }

    private void PopulateUI(PlacePin place)
    {
        Title = place.Name;
        PlaceName.Text = place.Name;
        PlaceCategory.Text = place.Category;
        PlaceRating.Text = place.Rating.ToString("0.0");
        PlaceHours.Text = place.OpeningHours;
        PlaceDescription.Text = place.Description;
        PlaceAddress.Text = place.Address;
        PlaceCoordinates.Text = $"Lat: {place.Latitude:F4}  |  Lng: {place.Longitude:F4}";

        // Set hero image from URL
        HeroImage.Source = ImageSource.FromUri(new Uri(place.ImageUrl));
    }
}
