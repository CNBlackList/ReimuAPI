using ReimuAPI.ReimuBase.TgData.MediaMessage;

namespace ReimuAPI.ReimuBase.TgData.OtherMessage
{
    public class Game
    {
        public string title { get; set; }

        public string description { get; set; }

        public Photo photo { get; set; }

        public string text { get; set; }

        public ContentEntities text_entities { get; set; }

        public Animation animation { get; set; }
    }
}
