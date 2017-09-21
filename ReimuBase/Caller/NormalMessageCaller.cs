using ReimuAPI.ReimuBase;
using ReimuAPI.ReimuBase.TgData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReimuAPI.ReimuBase.Caller
{
    public class NormalMessageCaller
    {
        private static string myUsername = TgApi.getDefaultApiConnection().getMe().username.ToLower();
        private static int myUsernameLength = TgApi.getDefaultApiConnection().getMe().username.Length;

        public void call(TgMessage message, string JsonMessage)
        {
            if (TempData.pluginsList == null)
            {
                RAPI.loadPlugins();
            }
            List<PluginObject> plugins = TempData.pluginsList;
            string messageType = message.chat.type.Substring(0, 1).ToUpper() + message.chat.type.Substring(1).ToLower();
            if (message.text != null)
            {
                if (message.entities != null) // 收到蓝字
                {
                    if (message.entities[0].type == "bot_command") 
                    {
                        if (message.chat.type == "private" && message.text.Length >= 6)
                        {
                            if (message.text.Substring(0, 6) == "/start")
                            {
                                if (message.text.Length > 7)
                                {
                                    PluginsCaller.callStartReceiver(
                                        plugins,
                                        "OnStartReceive",
                                        JsonMessage,
                                        new object[] { message, JsonMessage, message.text.Substring(7) }
                                        );
                                }
                                else
                                {
                                    PluginsCaller.callStartReceiver(
                                        plugins,
                                        "OnStartReceive",
                                        JsonMessage,
                                        new object[] { message, JsonMessage }
                                        );
                                }
                                return;
                            }
                        }
                        if (message.entities[0].offset == 0)
                        {
                            string command = message.text.Substring(0, message.entities[0].length).ToLower();
                            if (command.IndexOf("@") != -1)
                            {
                                if (command.IndexOf("@" + TgApi.getDefaultApiConnection().getMe().username.ToLower()) != -1)
                                {
                                    if (command.Substring(command.Length - myUsernameLength) == myUsername)
                                    {
                                        PluginsCaller.callCommandReceiver(
                                            plugins,
                                            "On" + messageType + "CommandReceive",
                                            JsonMessage,
                                            new object[] { message, JsonMessage, command.Substring(0, command.Length - myUsernameLength - 1) });
                                        return; // 收到命令
                                    }
                                }
                            } else
                            {
                                PluginsCaller.callCommandReceiver(
                                    plugins,
                                    "On" + messageType + "CommandReceive",
                                    JsonMessage,
                                    new object[] { message, JsonMessage, command });
                                return; // 收到命令
                            }
                        }
                    }
                }
                if (message.forward_from != null)
                {
                    PluginsCaller.callTextReceiver(plugins, "On" + messageType + "ForwardedUserMessageReceive", JsonMessage, new object[] { message, JsonMessage, message.forward_from });
                    return; // 收到转发自某个用户的消息
                }
                if (message.forward_from_chat != null)
                {
                    PluginsCaller.callTextReceiver(plugins, "On" + messageType + "ForwardedChatMessageReceive", JsonMessage, new object[] { message, JsonMessage, message.forward_from_chat });
                    return; // 收到转发自某个频道的消息
                }
                PluginsCaller.callTextReceiver(plugins, "On" + messageType + "MessageReceive", JsonMessage, new object[] { message, JsonMessage, message.text });
                return; // 收到普通信息
            }
            if (message.new_chat_member != null)
            {
                PluginsCaller.callMemberJoinReceiver(plugins, "On" + messageType + "MemberJoinReceive", JsonMessage, new object[] { message, JsonMessage, message.new_chat_member });
                return; // 收到新成员加入或被拉入的消息
            }
            if (message.left_chat_member != null)
            {
                PluginsCaller.callMemberJoinReceiver(plugins, "On" + messageType + "MemberLeftReceive", JsonMessage, new object[] { message, JsonMessage, message.left_chat_member });
                return; // 收到成员退出群组或被踢出的消息
            }
            if (message.audio != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "AudioReceive", JsonMessage, new object[] { message, JsonMessage, message.audio });
                return; // 收到音频文件
            }
            if (message.document != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "DocumentReceive", JsonMessage, new object[] { message, JsonMessage, message.document });
                return; // 收到文档
            }
            if (message.game != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "GameReceive", JsonMessage, new object[] { message, JsonMessage, message.game });
                return; // 收到
            }
            if (message.photo != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "PhotoReceive", JsonMessage, new object[] { message, JsonMessage, message.photo });
                return; // 收到照片
            }
            if (message.sticker != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "StickerReceive", JsonMessage, new object[] { message, JsonMessage, message.sticker });
                return; // 收到贴图（表情）
            }
            if (message.video != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "VideoReceive", JsonMessage, new object[] { message, JsonMessage, message.video });
                return; // 收到视频
            }
            if (message.voice != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "VoiceReceive", JsonMessage, new object[] { message, JsonMessage, message.voice });
                return; // 收到语音
            }
            if (message.video_note != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "VideoNoteReceive", JsonMessage, new object[] { message, JsonMessage, message.video_note });
                return; // 收到视频 Note
            }
            if (message.contact != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "ContactReceive", JsonMessage, new object[] { message, JsonMessage, message.contact });
                return; // 收到联系人
            }
            if (message.location != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "LocationReceive", JsonMessage, new object[] { message, JsonMessage, message.location });
                return; // 收到位置信息
            }
            if (message.venue != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "VenueReceive", JsonMessage, new object[] { message, JsonMessage, message.venue });
                return; // 收到实体地点信息
            }
            if (message.new_chat_title != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "NewChatTitleReceive", JsonMessage, new object[] { message, JsonMessage, message.new_chat_title });
                return; // 收到新的群标题
            }
            if (message.new_chat_photo != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "NewChatPhotoReceive", JsonMessage, new object[] { message, JsonMessage, message.new_chat_photo });
                return; // 收到新的群组头像
            }
            if (message.delete_chat_photo)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "ChatPhotoDeletedReceive", JsonMessage, new object[] { message });
                return; // 群头被删了
            }
            if (message.group_chat_created)
            {
                PluginsCaller.callPlugins(plugins, "OnGroupCreatedReceive", JsonMessage, new object[] { message });
                return; // 收到群组被创建
            }
            if (message.supergroup_chat_created)
            {
                PluginsCaller.callPlugins(plugins, "OnSupergroupCreatedReceive", JsonMessage, new object[] { message });
                return; // 收到超级群被创建
            }
            if (message.channel_chat_created)
            {
                PluginsCaller.callPlugins(plugins, "OnChannelCreatedReceive", JsonMessage, new object[] { message });
                return; // 收到频道被创建
            }
            if (message.migrate_to_chat_id != -1)
            {
                PluginsCaller.callPlugins(plugins, "OnMigrateToChatReceive", JsonMessage, new object[] { message, JsonMessage, message.migrate_to_chat_id });
                return; // 收到群组 ID 变更去那里
            }
            if (message.migrate_from_chat_id != -1)
            {
                PluginsCaller.callPlugins(plugins, "OnMigrateFromChatReceive", JsonMessage, new object[] { message, JsonMessage, message.migrate_from_chat_id });
                return; // 收到群组 ID 从那里变更
            }
            if (message.invoce != null)
            {
                PluginsCaller.callPlugins(plugins, "On" + messageType + "InvoiceReceive", JsonMessage, new object[] { message, JsonMessage, message.invoce });
                return; // 收到账单
            }
            PluginsCaller.callOtherMessageReceiver(plugins, "ReceiveOtherMessage", JsonMessage, new object[] { message, JsonMessage }); // 未知的消息类型，统一 Call 其他
        }
    }
}
