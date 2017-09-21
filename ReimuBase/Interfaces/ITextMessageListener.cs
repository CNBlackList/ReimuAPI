using ReimuAPI.ReimuBase.TgData;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface ITextMessageListener
    {

        CallbackMessage OnPrivateForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);
        CallbackMessage OnGroupForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);
        CallbackMessage OnSupergroupForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);

        CallbackMessage OnPrivateForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);
        CallbackMessage OnGroupForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);
        CallbackMessage OnSupergroupForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);

        CallbackMessage OnPrivateMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);
        CallbackMessage OnGroupMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);
        CallbackMessage OnSupergroupMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);
    }
}
