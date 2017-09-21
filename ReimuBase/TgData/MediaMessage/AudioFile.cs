namespace ReimuAPI.ReimuBase.TgData.MediaMessage
{
    public class AudioFile : Document
    {
        public int duration { get; set; }
        
        public string title { get; set; }
        
        public string performer { get; set; }
    }
}