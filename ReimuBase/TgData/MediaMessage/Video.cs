namespace ReimuAPI.ReimuBase.TgData.MediaMessage
{
    public class Video : Document
    {
        public int width { get; set; }

        public int height { get; set; }

        public int duration { get; set; }

        public Photo thumb { get; set; }

        public int length { get; set; }
    }
}
