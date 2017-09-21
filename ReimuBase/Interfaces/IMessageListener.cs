using ReimuAPI.ReimuBase.TgData;
using ReimuAPI.ReimuBase.TgData.MediaMessage;
using ReimuAPI.ReimuBase.TgData.OtherMessage;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface IMessageListener
    {
        CallbackMessage OnStartReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);

        CallbackMessage OnPrivateCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);
        CallbackMessage OnGroupCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);
        CallbackMessage OnSupergroupCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);

        CallbackMessage OnPrivateMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);
        CallbackMessage OnGroupMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);
        CallbackMessage OnSupergroupMessageReceive(TgMessage RawMessage, string JsonMessage, string TextMessage);

        CallbackMessage OnPrivateForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);
        CallbackMessage OnGroupForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);
        CallbackMessage OnSupergroupForwardedUserMessageReceive(TgMessage RawMessage, string JsonMessage, UserInfo FromUser);

        CallbackMessage OnPrivateForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);
        CallbackMessage OnGroupForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);
        CallbackMessage OnSupergroupForwardedChatMessageReceive(TgMessage RawMessage, string JsonMessage, ChatInfo FromChat);

        CallbackMessage OnGroupMemberJoinReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
        CallbackMessage OnSupergroupMemberJoinReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);

        CallbackMessage OnGroupMemberLeftReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
        CallbackMessage OnSupergroupMemberLeftReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
        

        CallbackMessage OnPrivateAudioReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);
        CallbackMessage OnGroupAudioReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);
        CallbackMessage OnSupergroupAudioReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);

        CallbackMessage OnPrivateDocumentReceive(TgMessage RawMessage, string JsonMessage, Document document);
        CallbackMessage OnGroupDocumentReceive(TgMessage RawMessage, string JsonMessage, Document document);
        CallbackMessage OnSupergroupDocumentReceive(TgMessage RawMessage, string JsonMessage, Document document);

        CallbackMessage OnPrivateGameReceive(TgMessage RawMessage, string JsonMessage, Game game);
        CallbackMessage OnGroupGameReceive(TgMessage RawMessage, string JsonMessage, Game game);
        CallbackMessage OnSupergroupGameReceive(TgMessage RawMessage, string JsonMessage, Game game);

        CallbackMessage OnPrivatePhotoReceive(TgMessage RawMessage, string JsonMessage, Photo photo);
        CallbackMessage OnGroupPhotoReceive(TgMessage RawMessage, string JsonMessage, Photo photo);
        CallbackMessage OnSupergroupPhotoReceive(TgMessage RawMessage, string JsonMessage, Photo photo);

        CallbackMessage OnPrivateStickerReceive(TgMessage RawMessage, string JsonMessage, Sticker sticker);
        CallbackMessage OnGroupStickerReceive(TgMessage RawMessage, string JsonMessage, Sticker sticker);
        CallbackMessage OnSupergroupStickerReceive(TgMessage RawMessage, string JsonMessage, Sticker sticker);

        CallbackMessage OnPrivateVideoReceive(TgMessage RawMessage, string JsonMessage, Video video);
        CallbackMessage OnGroupVideoReceive(TgMessage RawMessage, string JsonMessage, Video video);
        CallbackMessage OnSupergroupVideoReceive(TgMessage RawMessage, string JsonMessage, Video video);

        CallbackMessage OnPrivateVoiceReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);
        CallbackMessage OnGroupVoiceReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);
        CallbackMessage OnSupergroupVoiceReceive(TgMessage RawMessage, string JsonMessage, AudioFile voice);

        CallbackMessage OnPrivateVideoNoteReceive(TgMessage RawMessage, string JsonMessage, Video video);
        CallbackMessage OnGroupVideoNoteReceive(TgMessage RawMessage, string JsonMessage, Video video);
        CallbackMessage OnSupergroupVideoNoteReceive(TgMessage RawMessage, string JsonMessage, Video video);

        CallbackMessage OnPrivateContactReceive(TgMessage RawMessage, string JsonMessage, Contact contact);
        CallbackMessage OnGroupContactReceive(TgMessage RawMessage, string JsonMessage, Contact contact);
        CallbackMessage OnSupergroupContactReceive(TgMessage RawMessage, string JsonMessage, Contact contact);

        CallbackMessage OnPrivateLocationReceive(TgMessage RawMessage, string JsonMessage, Location location);
        CallbackMessage OnGroupLocationReceive(TgMessage RawMessage, string JsonMessage, Location location);
        CallbackMessage OnSupergroupLocationReceive(TgMessage RawMessage, string JsonMessage, Location location);

        CallbackMessage OnPrivateVenueReceive(TgMessage RawMessage, string JsonMessage, Venue venue);
        CallbackMessage OnGroupVenueReceive(TgMessage RawMessage, string JsonMessage, Venue venue);
        CallbackMessage OnSupergroupVenueReceive(TgMessage RawMessage, string JsonMessage, Venue venue);

        CallbackMessage OnGroupNewChatTitleReceive(TgMessage RawMessage, string JsonMessage, string NewChatTitle);
        CallbackMessage OnSupergroupNewChatTitleReceive(TgMessage RawMessage, string JsonMessage, string NewChatTitle);

        CallbackMessage OnGroupNewChatPhotoReceive(TgMessage RawMessage, string JsonMessage, Photo[] photo);
        CallbackMessage OnSupergroupNewChatPhotoReceive(TgMessage RawMessage, string JsonMessage, Photo[] photo);

        CallbackMessage OnGroupChatPhotoDeletedReceive(TgMessage RawMessage, string JsonMessage);
        CallbackMessage OnSupergroupChatPhotoDeletedReceive(TgMessage RawMessage, string JsonMessage);

        CallbackMessage OnGroupCreatedReceive(TgMessage RawMessage, string JsonMessage);
        CallbackMessage OnSupergroupCreatedReceive(TgMessage RawMessage, string JsonMessage);
        CallbackMessage OnChannelCreatedReceive(TgMessage RawMessage, string JsonMessage);

        CallbackMessage OnMigrateToChatReceive(TgMessage RawMessage, string JsonMessage, int MigrateToChatID);
        CallbackMessage OnMigrateFromChatReceive(TgMessage RawMessage, string JsonMessage, int MigrateFromChatID);

        CallbackMessage OnGroupPinnedMessageReceive(TgMessage RawMessage, string JsonMessage, TgMessage PinnedMessage);
        CallbackMessage OnSupergroupPinnedMessageReceive(TgMessage RawMessage, string JsonMessage, TgMessage PinnedMessage);

        CallbackMessage OnPrivateInvoiceReceive(TgMessage RawMessage, string JsonMessage, Invoice invoice);
        CallbackMessage OnGroupInvoiceReceive(TgMessage RawMessage, string JsonMessage, Invoice invoice);
        CallbackMessage OnSupergroupInvoiceReceive(TgMessage RawMessage, string JsonMessage, Invoice invoice);
    }
}
