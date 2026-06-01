namespace MauiGoogleMapsInfoWindow.Services;

using MauiGoogleMapsInfoWindow.Models;

/// <summary>
/// Provides demo/seed data for map pins.
/// In a real app, this would come from an API or database.
/// </summary>
public static class DemoDataService
{
    public static List<PlacePin> GetPlaces()
    {
        return
        [
            // --- Famous landmarks ---
            new PlacePin
            {
                Id = 1,
                Name = "Eiffel Tower",
                Description = "The Eiffel Tower is a wrought-iron lattice tower on the Champ de Mars in Paris, France. " +
                              "It is named after the engineer Gustave Eiffel, whose company designed and built the tower from 1887 to 1889. " +
                              "Standing at 330 metres tall, it was the world's tallest man-made structure for 41 years until the Chrysler Building was erected in New York City.",
                Latitude = 48.8584,
                Longitude = 2.2945,
                Category = "Landmark",
                Address = "Champ de Mars, 5 Av. Anatole France, 75007 Paris, France",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a8/Tour_Eiffel_Wikimedia_Commons.jpg",
                Rating = 4.7,
                OpeningHours = "9:30 AM – 11:45 PM"
            },

            new PlacePin
            {
                Id = 2,
                Name = "Colosseum",
                Description = "The Colosseum is an oval amphitheatre in the centre of the city of Rome, Italy. " +
                              "Built of travertine limestone, tuff, and brick-faced concrete, it was the largest amphitheatre ever built at the time. " +
                              "The Colosseum could hold an estimated 50,000 to 80,000 spectators at various points in its history.",
                Latitude = 41.8902,
                Longitude = 12.4922,
                Category = "Historical",
                Address = "Piazza del Colosseo, 1, 00184 Roma RM, Italy",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/de/Colosseo_2020.jpg/960px-Colosseo_2020.jpg",
                Rating = 4.8,
                OpeningHours = "9:00 AM – 7:15 PM"
            },

            new PlacePin
            {
                Id = 3,
                Name = "Statue of Liberty",
                Description = "The Statue of Liberty is a colossal neoclassical sculpture on Liberty Island in New York Harbor. " +
                              "The copper statue, a gift from the people of France, was designed by French sculptor Frédéric Auguste Bartholdi " +
                              "and its metal framework was built by Gustave Eiffel. The statue was dedicated on October 28, 1886.",
                Latitude = 40.6892,
                Longitude = -74.0445,
                Category = "Landmark",
                Address = "Liberty Island, New York, NY 10004, USA",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a1/Statue_of_Liberty_7.jpg",
                Rating = 4.7,
                OpeningHours = "9:00 AM – 5:00 PM"
            },

            new PlacePin
            {
                Id = 4,
                Name = "Taj Mahal",
                Description = "The Taj Mahal is an ivory-white marble mausoleum on the south bank of the river Yamuna in Agra, India. " +
                              "It was commissioned in 1631 by the fifth Mughal emperor, Shah Jahan, to house the tomb of his beloved wife, Mumtaz Mahal. " +
                              "The Taj Mahal is widely recognized as one of the most beautiful buildings ever created.",
                Latitude = 27.1751,
                Longitude = 78.0421,
                Category = "Historical",
                Address = "Dharmapuri, Forest Colony, Tajganj, Agra, Uttar Pradesh 282001, India",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Taj_Mahal_%28Edited%29.jpeg",
                Rating = 4.6,
                OpeningHours = "6:00 AM – 6:30 PM (Closed on Fridays)"
            },

            new PlacePin
            {
                Id = 5,
                Name = "Sydney Opera House",
                Description = "The Sydney Opera House is a multi-venue performing arts centre in Sydney, Australia. " +
                              "Designed by Danish architect Jørn Utzon, it was formally opened on 20 October 1973. " +
                              "It is one of the most famous and distinctive buildings of the 20th century, with its series of sail-shaped shells forming the roof.",
                Latitude = -33.8568,
                Longitude = 151.2153,
                Category = "Landmark",
                Address = "Bennelong Point, Sydney NSW 2000, Australia",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a0/Sydney_Australia._%2821339175489%29.jpg",
                Rating = 4.6,
                OpeningHours = "9:00 AM – 8:30 PM"
            },

            new PlacePin
            {
                Id = 6,
                Name = "Machu Picchu",
                Description = "Machu Picchu is a 15th-century Inca citadel situated on a mountain ridge 2,430 metres above sea level " +
                              "in the Cusco Region of Peru. Most archaeologists believe that Machu Picchu was constructed as an estate " +
                              "for the Inca emperor Pachacuti (1438–1472). Often referred to as the 'Lost City of the Incas'.",
                Latitude = -13.1631,
                Longitude = -72.5450,
                Category = "Historical",
                Address = "Aguas Calientes, Cusco Region 08681, Peru",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/e/eb/Machu_Picchu%2C_Peru.jpg",
                Rating = 4.9,
                OpeningHours = "6:00 AM – 5:30 PM"
            },

            new PlacePin
            {
                Id = 7,
                Name = "Great Wall of China",
                Description = "The Great Wall of China is a series of fortifications that were built across the historical northern borders of ancient Chinese states. " +
                              "Several walls were built from as early as the 7th century BC, with selective stretches later joined together by Qin Shi Huang. " +
                              "The total length of the wall is approximately 21,196 kilometres.",
                Latitude = 40.4319,
                Longitude = 116.5704,
                Category = "Historical",
                Address = "Huairou District, Beijing, China",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/23/The_Great_Wall_of_China_at_Jinshanling-edit.jpg",
                Rating = 4.8,
                OpeningHours = "7:30 AM – 5:30 PM"
            },

            new PlacePin
            {
                Id = 8,
                Name = "Christ the Redeemer",
                Description = "Christ the Redeemer is an Art Deco statue of Jesus Christ in Rio de Janeiro, Brazil. " +
                              "Created by French sculptor Paul Landowski, the statue is 30 metres tall, not including its 8-metre pedestal. " +
                              "The arms stretch 28 metres wide. It is the largest Art Deco-style statue in the world.",
                Latitude = -22.9519,
                Longitude = -43.2105,
                Category = "Landmark",
                Address = "Parque Nacional da Tijuca - Alto da Boa Vista, Rio de Janeiro, Brazil",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4f/Christ_the_Redeemer_-_Cristo_Redentor.jpg",
                Rating = 4.7,
                OpeningHours = "8:00 AM – 7:00 PM"
            },

            new PlacePin
            {
                Id = 9,
                Name = "Big Ben",
                Description = "Big Ben is the nickname for the Great Bell of the striking clock at the north end of the Palace of Westminster in London, England. " +
                              "The tower was designed by Augustus Pugin in a neo-Gothic style and stands 96 metres tall. " +
                              "The Elizabeth Tower is one of the most prominent symbols of the United Kingdom and parliamentary democracy.",
                Latitude = 51.5007,
                Longitude = -0.1246,
                Category = "Landmark",
                Address = "Westminster, London SW1A 0AA, United Kingdom",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/9/93/Clock_Tower_-_Palace_of_Westminster%2C_London_-_May_2007.jpg",
                Rating = 4.6,
                OpeningHours = "External viewing only"
            },

            new PlacePin
            {
                Id = 10,
                Name = "Petra",
                Description = "Petra is a famous archaeological site in Jordan's southwestern desert. " +
                              "Dating to around 300 B.C., it was the capital of the Nabatean Kingdom. " +
                              "Accessed via a narrow canyon called Al Siq, it contains tombs and temples carved into pink sandstone cliffs, " +
                              "earning it the nickname 'Rose City'.",
                Latitude = 30.3285,
                Longitude = 35.4444,
                Category = "Historical",
                Address = "Wadi Musa, Jordan",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Treasury_petra_crop.jpeg/960px-Treasury_petra_crop.jpeg",
                Rating = 4.8,
                OpeningHours = "6:00 AM – 6:00 PM (Summer)"
            }
        ];
    }
}
