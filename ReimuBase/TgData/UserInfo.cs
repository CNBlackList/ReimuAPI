namespace ReimuAPI.ReimuBase.TgData
{
    public class UserInfo
    {
        public int id { get; set; }
        
        public bool is_bot { get; set; }
        
        public string first_name { get; set; }
        
        public string last_name { get; set; }
        
        public string username { get; set; }
        
        public string language_code { get; set; }

        public string status { get; set; }

        public string GetUserTextInfo()
        {
            string info = "\nID: " + id;
            if (first_name != null)
            {
                info += "\nFirst name: " + first_name;
            }
            if (last_name != null)
            {
                info += "\nLast name: " + last_name;
            }
            if (username != null)
            {
                info += "\nUsername : @" + username;
            }
            if (language_code != null)
            {
                info += "\nLanguage code: " + language_code;
            }
            return info;
        }
    }
}