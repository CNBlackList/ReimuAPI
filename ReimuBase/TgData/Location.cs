namespace ReimuAPI.ReimuBase.TgData
{
    public class Location
    {
        public int latitude { get; set; }

        public int longitude { get; set; }
    }

    public class Venue
    {
        public Location location { get; set; }

        public string title { get; set; }

        public string address { get; set; }

        public string foursquare_id { get; set; }
    }
}
