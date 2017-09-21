using ReimuAPI.ReimuBase.TgData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ReimuAPI.ReimuBase
{
    public class TgApi
    {
        // ParseMode
        public static readonly int PARSEMODE_DISABLED = 0;
        public static readonly int PARSEMODE_MARKDOWN = 1;
        public static readonly int PARSEMODE_HTML = 2;

        // ChatAction
        public static readonly int CHATACTION_TYPING = 0;
        public static readonly int CHATACTION_UPLOADING_PHOTO = 1;
        public static readonly int CHATACTION_RECORDING_VIDEO = 2;
        public static readonly int CHATACTION_UPLOADING_VIDEO = 3;
        public static readonly int CHATACTION_RECORDING_AUDIO = 4;
        public static readonly int CHATACTION_UPLOADING_AUDIO = 5;
        public static readonly int CHATACTION_UPLOADING_DOCUMENT = 6;
        public static readonly int CHATACTION_FINDING_LOCATION = 7;
        public static readonly int CHATACTION_RECORDING_VIDEONOTE = 8;
        public static readonly int CHATACTION_UPLOADING_VIDEONOTE = 9;

        private readonly string apiUrl;

        public TgApi()
        {
            ReimuConfig config = new ConfigManager().getConfig();
            apiUrl = "https://" + config.api_host + "/bot" + config.api_key + "/";
        }

        public TgApi(string ApiKey, string ApiHost = "api.telegram.org")
        {
            apiUrl = "https://" + ApiHost + "/bot" + ApiKey + "/";
        }

        public static TgApi getDefaultApiConnection()
        {
            if (TempData.tgApi == null)
            {
                TgApi api = new TgApi();
                TempData.tgApi = api;
                return api;
            }
            return TempData.tgApi;
        }

        public UserInfo getMe()
        {
            if (TempData.SelfInfo == null)
            {
                UserInfoRequest data = (UserInfoRequest)new DataContractJsonSerializer(
                    typeof(UserInfoRequest)
                ).ReadObject(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes(getWeb(apiUrl + "getMe").Content)
                    )
                );
                if (data.ok)
                {
                    TempData.SelfInfo = data.result;
                    return data.result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return TempData.SelfInfo;
            }
        }

        public SendMessageResult sendMessage(
            long ChatID,
            string Message,
            int ReplyID = -1,
            int ParseMode = 0,
            bool DisableWebPreview = true,
            bool DisableNotification = true,
            string ReplyMarkup = null
            )
        {
            string jsonData = "{\"chat_id\":" + ChatID;
            jsonData += ",\"text\":" + jsonEncode(Message);
            if (ReplyID != -1)
            {
                jsonData += ",\"reply_to_message_id\":" + ReplyID;
            }
            if (ParseMode == PARSEMODE_MARKDOWN)
            {
                jsonData += ",\"parse_mode\":\"Markdown\"";
            }
            if (ParseMode == PARSEMODE_HTML)
            {
                jsonData += ",\"parse_mode\":\"HTML\"";
            }
            jsonData += ",\"disable_web_page_preview\":" + booleanToString(DisableWebPreview);
            jsonData += ",\"disable_notification\":" + booleanToString(DisableNotification);
            if (ReplyMarkup != null)
            {
                jsonData += ",\"reply_markup\":" + ReplyMarkup;
            }
            jsonData += "}";
            ApiResult recData = postJson(apiUrl + "sendMessage", jsonData);
            return getSendMessageResult(recData);
        }

        public SendMessageResult forwardMessage(long ChatID, long FromChatID, int MessageID, bool DisableNotification = true)
        {
            ApiResult recData = postWeb(apiUrl + "forwardMessage", "chat_id=" + ChatID +
                "&from_chat_id=" + FromChatID +
                "&message_id=" + MessageID +
                "&disable_notification" + booleanToString(DisableNotification)
                );
            return getSendMessageResult(recData);
        }

        public bool sendPhoto()
        {
            return false;
        }

        public bool sendAudio()
        {
            return false;
        }

        public bool sendDocument()
        {
            return false;
        }

        public bool sendVideo()
        {
            return false;
        }

        public bool sendVoice()
        {
            return false;
        }

        public bool sendVideoNote()
        {
            return false;
        }

        public bool sendLocation()
        {
            return false;
        }

        public bool sendVenue()
        {
            return false;
        }

        public SendMessageResult sendContact(
            long ChatID,
            string PhoneNumber,
            string FirstName,
            string LastName,
            int ReplyID,
            bool DisableNotification = true,
            string ReplyMarkup = null
            )
        {
            string jsonData = "{\"chat_id\":" + ChatID;
            jsonData += ",\"phone_number\":" + jsonEncode(PhoneNumber);
            if (ReplyID != -1)
            {
                jsonData += ",\"reply_to_message_id\":" + ReplyID;
            }
            jsonData += ",\"first_name\":" + jsonEncode(FirstName);
            jsonData += ",\"last_name\":" + jsonEncode(LastName);
            jsonData += ",\"disable_notification\":" + booleanToString(DisableNotification);
            if (ReplyMarkup != null)
            {
                jsonData += ",\"reply_markup\":" + ReplyMarkup;
            }
            jsonData += "}";
            ApiResult recData = postJson(apiUrl + "sendContact", jsonData);
            return getSendMessageResult(recData);
        }

        public SetActionResult sendChatAction(long ChatID, int Action)
        {
            string realAction = "";
            switch (Action)
            {
                case 0:
                    realAction = "typing";
                    break;
                case 1:
                    realAction = "upload_photo";
                    break;
                case 2:
                    realAction = "record_video";
                    break;
                case 3:
                    realAction = "upload_video";
                    break;
                case 4:
                    realAction = "record_audio";
                    break;
                case 5:
                    realAction = "upload_audio";
                    break;
                case 6:
                    realAction = "upload_document";
                    break;
                case 7:
                    realAction = "find_location";
                    break;
                case 8:
                    realAction = "record_video_note";
                    break;
                case 9:
                    realAction = "upload_video_note";
                    break;
                default:
                    realAction = "typing";
                    break;
            }
            ApiResult recData = postWeb(apiUrl + "sendChatAction", "chat_id=" + ChatID +
                "&action=" + realAction
                );
            return getSetActionResult(recData);
        }

        public bool getUserProfilePhotos()
        {
            return false;
        }

        public bool getFile()
        {
            return false;
        }

        public SetActionResult kickChatMember(long ChatID, long UserID, long UtilDate = 0)
        {
            string realUtilDate = "";
            if (UtilDate != 0)
            {
                realUtilDate = "&until_date=" + UtilDate;
            }
            ApiResult recData = postWeb(apiUrl + "kickChatMember", "chat_id=" + ChatID +
                "&user_id=" + UserID + realUtilDate
                );
            return getSetActionResult(recData);
        }

        public SetActionResult unbanChatMember(long ChatID, long UserID)
        {
            ApiResult recData = postWeb(apiUrl + "unbanChatMember", "chat_id=" + ChatID + "&user_id=" + UserID);
            return getSetActionResult(recData);
        }

        public SetActionResult restrictChatMember(
            long ChatID,
            long UserID,
            long UtilDate = 0,
            bool SendMessage = false,
            bool SendMedia = false,
            bool SendOthers = false,
            bool SendWebpage = false
            )
        {
            string realUtilDate = "";
            if (UtilDate != 0)
            {
                realUtilDate = "&until_date=" + UtilDate;
            }
            string permissions = "";
            permissions += "&can_send_messages=" + booleanToString(SendMessage);
            permissions += "&can_send_media_messages=" + booleanToString(SendMedia);
            permissions += "&can_send_other_messages=" + booleanToString(SendOthers);
            permissions += "&can_add_web_page_previews=" + booleanToString(SendWebpage);
            ApiResult recData = postWeb(apiUrl + "restrictChatMember", "chat_id=" + ChatID +
                "&user_id=" + UserID + realUtilDate + permissions
                );
            return getSetActionResult(recData);
        }

        public SetActionResult promoteChatMember(
            long ChatID,
            long UserID,
            bool ChangeInfo = false,
            bool PostMessages = false,
            bool EditMessages = false,
            bool DeleteMessages = false,
            bool InviteUsers = false,
            bool RestrictMembers = false,
            bool PinMessages = false,
            bool PromoteMembers = false
            )
        {
            string permissions = "";
            permissions += "&can_change_info=" + booleanToString(ChangeInfo);
            permissions += "&can_post_messages=" + booleanToString(PostMessages);
            permissions += "&can_edit_messages=" + booleanToString(EditMessages);
            permissions += "&can_delete_messages=" + booleanToString(DeleteMessages);
            permissions += "&can_invite_users=" + booleanToString(InviteUsers);
            permissions += "&can_restrict_members=" + booleanToString(RestrictMembers);
            permissions += "&can_pin_messages=" + booleanToString(PinMessages);
            permissions += "&can_promote_members=" + booleanToString(PromoteMembers);
            ApiResult recData = postWeb(apiUrl + "promoteChatMember", "chat_id=" + ChatID +
                "&user_id=" + UserID + permissions
                );
            return getSetActionResult(recData);
        }

        public SetActionResult exportChatInviteLink(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "exportChatInviteLink", "chat_id=" + ChatID);
            return getSetActionResult(recData);
        }

        public SendMessageResult setChatPhoto(long ChatID, byte[] photo)
        {
            ApiResult recData = postWeb(apiUrl + "setChatPhoto", "chat_id=" + ChatID);
            return getSendMessageResult(recData);
        }

        public SendMessageResult deleteChatPhoto(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "deleteChatPhoto", "chat_id=" + ChatID);
            return getSendMessageResult(recData);
        }

        public SendMessageResult setChatTitle(long ChatID, string Title)
        {
            ApiResult recData = postWeb(apiUrl + "setChatTitle", "chat_id=" + ChatID + "&title=" + Title);
            return getSendMessageResult(recData);
        }

        public SetActionResult setChatDescription(long ChatID, string Description)
        {
            ApiResult recData = postWeb(apiUrl + "setChatDescription", "chat_id=" + ChatID + "&title=" + Description);
            return getSetActionResult(recData);
        }

        public SendMessageResult pinChatMessage(long ChatID, int MessageID, bool DisableNotification)
        {
            ApiResult recData = postWeb(
                apiUrl + "setChatDescription",
                "chat_id=" + ChatID +
                "&message_id=" + MessageID +
                "&disable_notification=" + DisableNotification
                );
            return getSendMessageResult(recData);
        }

        public SetActionResult unpinChatMessage(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "unpinChatMessage", "chat_id=" + ChatID);
            return getSetActionResult(recData);
        }

        public SetActionResult leaveChat(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "leaveChat", "chat_id=" + ChatID);
            return getSetActionResult(recData);
        }

        public UserInfoRequest getChat(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "getChat", "chat_id=" + ChatID);
            return getMemberInfo(recData);
        }

        public UserInfoRequest getChat(string ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "getChat", "chat_id=" + ChatID);
            return getMemberInfo(recData);
        }

        public GroupUserInfo[] getChatAdministrators(long gid)
        {
            if (TempData.tempAdminList == null)
            {
                TempData.tempAdminList = new Dictionary<long, GroupUserInfo[]> { };
                TempData.adminListUptime = DateTime.Now.AddMinutes(60);
            }
            GroupUserInfo[] list;
            if (DateTime.Now <= TempData.adminListUptime)
            {
                TempData.tempAdminList.TryGetValue(gid, out list);
                if (list != null)
                {
                    return list;
                }
            }
            else
            {
                TempData.tempAdminList.Clear();
                TempData.adminListUptime = DateTime.Now.AddMinutes(60);
            }
            MemberList data = (MemberList)new DataContractJsonSerializer(
                typeof(MemberList)
            ).ReadObject(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(postWeb(apiUrl + "getChatAdministrators", "chat_id=" + gid).Content)
                )
            );
            TempData.tempAdminList[gid] = data.result;
            return data.result;
        }

        public MembersCountResult getChatMembersCount(long ChatID)
        {
            ApiResult recData = postWeb(apiUrl + "getChatMembersCount", "chat_id=" + ChatID);
            MembersCountResult data = (MembersCountResult)new DataContractJsonSerializer(
                typeof(MembersCountResult)
            ).ReadObject(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(recData.Content)
                )
            );
            data.httpContent = recData;
            return data;
        }

        public UserInfoRequest getChatMember(long ChatID, int UserID)
        {
            ApiResult recData = postWeb(apiUrl + "getChatMember", "chat_id=" + ChatID + "&user_id=" + UserID);
            return getMemberInfo(recData);
        }

        public ApiResult answerCallbackQuery
            (string CallbackQueryID,
            string Text = null,
            bool ShowAlert = false,
            string URL = null,
            int CacheTime = 0)
        {
            string otherMsg = "";
            if (Text != null)
            {
                otherMsg += "&text=" + Text;
            }
            otherMsg += "&show_alert=" + ShowAlert;
            if (URL != null)
            {
                otherMsg += "&url=" + URL;
            }
            if (CacheTime != 0)
            {
                otherMsg += "&cache_time=" + CacheTime;
            }

            ApiResult recData = postWeb(apiUrl + "answerCallbackQuery", "callback_query_id=" + CallbackQueryID + otherMsg);
            return recData;
        }


        public ApiResult editMessageText(
            string Message,
            long ChatID = -1,
            string InlineMessageID = null,
            int MessageID = -1,
            int ParseMode = 0,
            bool DisableWebPreview = true,
            string ReplyMarkup = null
            )
        {
            string jsonData = "{\"text\":" + jsonEncode(Message);
            if (ChatID != -1)
            {
                jsonData += ",\"chat_id\":" + ChatID;
            }
            if (InlineMessageID != null)
            {
                jsonData += ",\"message_id\":" + MessageID;
                jsonData += ",\"inline_message_id\":" + InlineMessageID;
            }
            if (ParseMode == PARSEMODE_MARKDOWN)
            {
                jsonData += ",\"parse_mode\":\"Markdown\"";
            }
            if (ParseMode == PARSEMODE_HTML)
            {
                jsonData += ",\"parse_mode\":\"HTML\"";
            }
            jsonData += ",\"disable_web_page_preview\":" + booleanToString(DisableWebPreview);
            if (ReplyMarkup != null)
            {
                jsonData += ",\"reply_markup\":" + ReplyMarkup;
            }
            jsonData += "}";
            ApiResult recData = postJson(apiUrl + "editMessageText", jsonData);
            return recData;
        }

        public ApiResult editMessageCaption(
            string Message = null,
            long ChatID = -1,
            string InlineMessageID = null,
            int MessageID = -1,
            string ReplyMarkup = null
            )
        {
            string jsonData = "{";
            if (ChatID != -1)
            {
                jsonData += "\"chat_id\":" + ChatID;
            }
            if (InlineMessageID != null)
            {
                jsonData += "\"inline_message_id\":" + InlineMessageID;
                jsonData += ",\"message_id\":" + MessageID;
            }
            if (Message != null)
            {
                jsonData += "\"caption\":" + jsonEncode(Message);
            }
            if (ReplyMarkup != null)
            {
                jsonData += ",\"reply_markup\":" + ReplyMarkup;
            }
            jsonData += "}";
            ApiResult recData = postJson(apiUrl + "editMessageCaption", jsonData);
            return recData;
        }

        public ApiResult editMessageReplyMarkup(
            long ChatID = -1,
            string InlineMessageID = null,
            int MessageID = -1,
            string ReplyMarkup = null
            )
        {
            string jsonData = "{";
            if (ChatID != -1)
            {
                jsonData += "\"chat_id\":" + ChatID;
            }
            if (InlineMessageID != null)
            {
                jsonData += "\"inline_message_id\":" + InlineMessageID;
                jsonData += ",\"message_id\":" + MessageID;
            }
            if (ReplyMarkup != null)
            {
                jsonData += ",\"reply_markup\":" + ReplyMarkup;
            }
            jsonData += "}";
            ApiResult recData = postJson(apiUrl + "editMessageReplyMarkup", jsonData);
            return recData;
        }

        public ApiResult deleteMessage(long ChatID, int MessageID)
        {
            ApiResult recData = postWeb(apiUrl + "deleteMessage", "chat_id=" + ChatID + "&message_id=" + MessageID);
            return recData;
        }

        public bool getSelfPermission(long gid)
        {
            UserInfo userInfo;
            if (TempData.SelfInfo == null)
            {
                userInfo = getMe();
            }
            else
            {
                userInfo = TempData.SelfInfo;
            }
            return checkIsAdmin(gid, userInfo.id);
        }

        public bool checkIsAdmin(long gid, int uid)
        {
            GroupUserInfo[] list = getChatAdministrators(gid);
            foreach (GroupUserInfo admin in list)
            {
                if (admin.user.id == uid)
                {
                    return true;
                }
            }
            return false;
        }

        public SendMessageResult getSendMessageResult(ApiResult content)
        {
            SendMessageResult data = (SendMessageResult)new DataContractJsonSerializer(
                typeof(SendMessageResult)
            ).ReadObject(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(content.Content)
                )
            );
            data.httpContent = content;
            return data;
        }

        public SetActionResult getSetActionResult(ApiResult content)
        {
            SetActionResult data = (SetActionResult)new DataContractJsonSerializer(
                typeof(SetActionResult)
            ).ReadObject(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(content.Content)
                )
            );
            data.httpContent = content;
            return data;
        }

        public UserInfoRequest getMemberInfo(ApiResult content)
        {
            UserInfoRequest data = (UserInfoRequest)new DataContractJsonSerializer(
                typeof(UserInfoRequest)
            ).ReadObject(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(content.Content)
                )
            );
            data.httpContent = content;
            return data;
        }

        public string booleanToString(bool obj)
        {
            if (obj)
            {
                return "true";
            } else
            {
                return "false";
            }
        }

        public string jsonEncode(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }

        public ApiResult postWeb(string uri, string param)
        {
            byte[] bs = Encoding.UTF8.GetBytes(param);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }

            String returnText;
            HttpStatusCode statusCode;
            try
            {
                WebResponse wr = req.GetResponse();
                MemoryStream memoryStream = new MemoryStream();
                wr.GetResponseStream().CopyTo(memoryStream);
                returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                statusCode = HttpStatusCode.OK;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = e.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        resp.GetResponseStream().CopyTo(memoryStream);
                        returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                        statusCode = resp.StatusCode;
                    }
                    else throw e;
                }
                else throw e;
            }
            return new ApiResult(statusCode, returnText);
        }
        /*
        public HTTPContent sendFile(string uri, MultipartFormDataContent form)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(uri, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;
        }*/

        public ApiResult postJson(string uri, string json)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.ContentType = "application/json; charset=utf-8";

            using (StreamWriter streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            String returnText;
            HttpStatusCode statusCode;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                MemoryStream memoryStream = new MemoryStream();
                resp.GetResponseStream().CopyTo(memoryStream);
                returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                statusCode = resp.StatusCode;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = e.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        resp.GetResponseStream().CopyTo(memoryStream);
                        returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                        statusCode = resp.StatusCode;
                    }
                    else throw e;
                }
                else throw e;
            }
            return new ApiResult(statusCode, returnText);
        }

        public ApiResult getWeb(string uri)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "GET";

            String returnText;
            HttpStatusCode statusCode;
            try
            {
                WebResponse wr = req.GetResponse();
                MemoryStream memoryStream = new MemoryStream();
                wr.GetResponseStream().CopyTo(memoryStream);
                returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                statusCode = HttpStatusCode.OK;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse resp = e.Response as HttpWebResponse;
                    if (resp != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        resp.GetResponseStream().CopyTo(memoryStream);
                        returnText = Encoding.UTF8.GetString(memoryStream.ToArray());
                        statusCode = resp.StatusCode;
                    }
                    else throw e;
                }
                else throw e;
            }
            return new ApiResult(statusCode, returnText);
        }

    }

    public class ApiResult
    {
        internal ApiResult(HttpStatusCode status, string content)
        {
            StatusCode = status;
            Content = content;
        }

        public HttpStatusCode StatusCode { get; }
        public string Content { get; }
    }
}
