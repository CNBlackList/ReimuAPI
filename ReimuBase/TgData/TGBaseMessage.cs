namespace ReimuAPI.ReimuBase.TgData
{
    public class TgBaseMessage
    {
        public int update_id { get; set; }
        
        public TgMessage message { get; set; }
        
        public EditedMessage edited_message { get; set; }
    }
}