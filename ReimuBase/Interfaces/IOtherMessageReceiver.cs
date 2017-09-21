using ReimuAPI.ReimuBase.TgData;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface IOtherMessageReceiver
    {
        // 未知的消息
        CallbackMessage ReceiveOtherMessage(TgMessage RawMessage, string JsonMessage);
        CallbackMessage ReceiveAllNormalMessage(TgMessage BaseMessage, string JsonMessage);
        CallbackMessage ReceiveUnknownBaseMessage(TgBaseMessage BaseMessage, string JsonMessage);
    }
}
