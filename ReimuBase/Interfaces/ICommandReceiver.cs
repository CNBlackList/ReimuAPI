using ReimuAPI.ReimuBase.TgData;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface ICommandReceiver
    {
        CallbackMessage OnPrivateCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);
        CallbackMessage OnGroupCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);
        CallbackMessage OnSupergroupCommandReceive(TgMessage RawMessage, string JsonMessage, string Command);
    }
}
