#if IOS
using CoreGraphics;
using Google.Maps;
using UIKit;

namespace MauiGoogleMapsInfoWindow.Platforms.iOS.CustomViews;

// ToPlatformEmbedded cannot be used here. MAUI's embedded rendering context
// conflicts with the Google Maps SDK Metal pipeline on iOS 26, crashing in
// BubbleBehavior::Commit even for completely empty MAUI views.
// Pure UIKit is the only working approach until a Maps SDK 10.x binding
// for MAUI is available (adame.google.ios.maps currently tops at 9.2.0.8).
internal static class CustomInfoWindowRenderer
{
    internal static UIView BuildView(Marker marker)
    {
        try
        {
            var snippet = marker.Snippet ?? "";
            var parts = snippet.Split("||");
            var address = parts.Length > 0 ? parts[0] : "Unknown location";
            var category = parts.Length > 1 ? parts[1] : "";
            var rating = parts.Length > 2 ? parts[2] : "0.0";
            var hours = parts.Length > 3 ? parts[3] : "";

            const float cardWidth = 320f;
            const float cardHeight = 95f;
            const float triangleW = 28f;
            const float triangleH = 16f;

            var container = new UIView(new CGRect(0, 0, cardWidth, cardHeight + triangleH));
            container.BackgroundColor = UIColor.Clear;

            // ClipsToBounds = false keeps MasksToBounds off the layer — required
            // because MasksToBounds also triggers the BubbleBehavior crash on iOS 26.
            var card = new UIView(new CGRect(0, 0, cardWidth, cardHeight));
            card.BackgroundColor = UIColor.White;
            card.ClipsToBounds = false;
            card.Layer.CornerRadius = 16;
            card.Layer.ShadowColor = UIColor.Black.CGColor;
            card.Layer.ShadowOffset = new CGSize(0, 4);
            card.Layer.ShadowRadius = 10;
            card.Layer.ShadowOpacity = 0.15f;
            container.AddSubview(card);

            var accent = new UIView(new CGRect(0, 0, 12, cardHeight));
            accent.BackgroundColor = UIColor.FromRGB(81, 43, 212);
            accent.Layer.CornerRadius = 3;
            card.AddSubview(accent);

            var nameLabel = new UILabel(new CGRect(22, 12, cardWidth - 36, 24));
            nameLabel.Text = marker.Title ?? "Unknown";
            nameLabel.Font = UIFont.BoldSystemFontOfSize(18);
            nameLabel.TextColor = UIColor.FromRGB(26, 26, 26);
            card.AddSubview(nameLabel);

            var addrLabel = new UILabel(new CGRect(22, 40, cardWidth - 36, 26));
            addrLabel.Text = address;
            addrLabel.Font = UIFont.SystemFontOfSize(13);
            addrLabel.TextColor = UIColor.FromRGB(102, 102, 102);
            addrLabel.Lines = 2;
            addrLabel.LineBreakMode = UILineBreakMode.TailTruncation;
            card.AddSubview(addrLabel);

            // FontAwesome6Free-Solid is registered at startup via ConfigureFonts.
            //  = map-marker-alt,  = star,  = clock.
            // Using explicit \u escapes so the characters can never be dropped by editors.
            var faFont = UIFont.FromName("FontAwesome6Free-Solid", 13) ?? UIFont.SystemFontOfSize(13);
            var boldFont = UIFont.BoldSystemFontOfSize(12);
            var colWidth = (cardWidth - 36) / 3f;

            card.AddSubview(MakeIconPair(faFont, "", boldFont, category,
                new CGRect(22, 72, colWidth, 18)));
            card.AddSubview(MakeIconPair(faFont, "", boldFont, rating,
                new CGRect(22 + colWidth, 72, colWidth, 18)));
            card.AddSubview(MakeIconPair(faFont, "", boldFont, hours,
                new CGRect(22 + colWidth * 2, 72, colWidth, 18)));

            // TriangleView.Draw uses Core Graphics — not CAShapeLayer — which is
            // the other crash trigger on iOS 26 with the Maps SDK renderer.
            var triangle = new TriangleView(
                new CGRect((cardWidth - triangleW) / 2, cardHeight, triangleW, triangleH));
            container.AddSubview(triangle);

            return container;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CustomInfoWindow error: {ex.Message}");
            return new UIView(new CGRect(0, 0, 1, 1));
        }
    }

    private static UIView MakeIconPair(UIFont iconFont, string icon, UIFont textFont,
        string text, CGRect frame)
    {
        var group = new UIView(frame);

        var iconLabel = new UILabel(new CGRect(0, 0, 18, 18));
        iconLabel.Text = icon;
        iconLabel.Font = iconFont;
        iconLabel.TextColor = UIColor.FromRGB(26, 26, 26);
        group.AddSubview(iconLabel);

        var textLabel = new UILabel(new CGRect(22, 0, frame.Width - 22, 18));
        textLabel.Text = text;
        textLabel.Font = textFont;
        textLabel.TextColor = UIColor.FromRGB(26, 26, 26);
        textLabel.LineBreakMode = UILineBreakMode.TailTruncation;
        group.AddSubview(textLabel);

        return group;
    }
}

internal class TriangleView : UIView
{
    public TriangleView(CGRect frame) : base(frame)
    {
        BackgroundColor = UIColor.Clear;
    }

    public override void Draw(CGRect rect)
    {
        using var context = UIGraphics.GetCurrentContext();
        if (context == null) return;

        context.BeginPath();
        context.MoveTo(0, 0);
        context.AddLineToPoint(rect.Width, 0);
        context.AddLineToPoint(rect.Width / 2, rect.Height);
        context.ClosePath();
        UIColor.White.SetFill();
        context.FillPath();
    }
}
#endif
