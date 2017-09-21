namespace ReimuAPI.ReimuBase.TgData.MediaMessage
{
    public class Sticker : Document
    {
        public int width { get; set; }
        
        public int height { get; set; }
        
        public string emoji { get; set; }
        
        public Photo thumb { get; set; }
    }
}