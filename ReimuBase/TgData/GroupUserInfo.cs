namespace ReimuAPI.ReimuBase.TgData
{
    public class GroupUserInfo
    {
        public string status { get; set; }
        
        public UserInfo user { get; set; }

        public bool can_be_edited { get; set; }

        public bool can_change_info { get; set; }

        public bool can_delete_messages { get; set; }

        public bool can_invite_users { get; set; }

        public bool can_restrict_members { get; set; }

        public bool can_pin_messages { get; set; }

        public bool can_promote_members { get; set; }
    }

    public class MemberList
    {
        public bool ok { get; set; }

        public GroupUserInfo[] result { get; set; }
    }
}