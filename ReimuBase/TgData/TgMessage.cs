using ReimuAPI.ReimuBase.TgData.MediaMessage;
using ReimuAPI.ReimuBase.TgData.OtherMessage;

namespace ReimuAPI.ReimuBase.TgData
{
    public class TgMessage
    {
        public int message_id { get; set; } = -1; // 信息ID

        public UserInfo from { get; set; } // 发送消息的用户信息

        public int date { get; set; } = -1; // 日期

        public ChatInfo chat { get; set; } // 发送在哪个聊天室，如果是私聊则与 from 相同

        public UserInfo forward_from { get; set; } // 转发自（用户）

        public ChatInfo forward_from_chat { get; set; } // 转发自（聊天室/频道）

        public int forward_from_message_id { get; set; } = -1; // ???

        public string forward_signature { get; set; } // ???

        public int forward_date { get; set; } = -1; // ???

        public TgMessage reply_to_message { get; set; } // 回复的信息

        public int edit_date { get; set; } = -1; // 编辑日期

        public ChatInfo author_signature { get; set; } // ???

        public string text { get; set; } // 信息文本

        public ContentEntities[] entities { get; set; } // 蓝色可点击的信息

        public AudioFile audio { get; set; } // 音频

        public Document document { get; set; } // 文档

        public Game game { get; set; } // 游戏

        public Photo[] photo { get; set; } // 照片

        public Sticker sticker { get; set; } // 贴图

        public Video video { get; set; } // 视频

        public AudioFile voice { get; set; } // 语音

        public Video video_note { get; set; } // 语音

        public UserInfo[] new_chat_members { get; set; } // 新成员表，包含机器人

        public string caption { get; set; } // 图片的注释

        public Contact contact { get; set; } // 联系人

        public Location location { get; set; } // 位置

        public Venue venue { get; set; } // 位置

        public UserInfo new_chat_participant { get; set; } // 新成员加入

        public UserInfo new_chat_member { get; set; } // 新成员加入 包含机器人

        public UserInfo left_chat_participant { get; set; } // 成员离开

        public UserInfo left_chat_member { get; set; } // 成员离开 包含机器人

        public string new_chat_title { get; set; } // 新的群组名

        public Photo[] new_chat_photo { get; set; } // 新的群组图标

        public bool delete_chat_photo { get; set; } = false; // 群头被删除

        public bool group_chat_created { get; set; } = false; // 群组被创建

        public bool supergroup_chat_created { get; set; } = false; // 超级群被创建

        public bool channel_chat_created { get; set; } = false; // 频道被创建

        public long migrate_to_chat_id { get; set; } = -1; // 群组 ID 变更成这个值

        public long migrate_from_chat_id { get; set; } = -1; // 旧的群组的 ID

        public TgMessage pinned_message { get; set; } // 置顶消息
        
        public Invoice invoce { get; set; } // 账单

        public UserInfo GetSendUser()
        {
            return from;
        }

        public ChatInfo GetMessageChatInfo()
        {
            return chat;
        }

        public UserInfo GetForwardedFromUser()
        {
            return forward_from;
        }

        public ChatInfo GetForwardedFromChat()
        {
            return forward_from_chat;
        }

        public TgMessage GetReplyMessage()
        {
            return reply_to_message;
        }
    }
}