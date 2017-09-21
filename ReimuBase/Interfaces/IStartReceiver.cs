using ReimuAPI.ReimuBase.TgData;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface IStartReceiver
    {
        CallbackMessage OnStartReceive(TgMessage RawMessage, string JsonMessage, string StartMessage);

        CallbackMessage OnStartReceive(TgMessage RawMessage, string JsonMessage);
    }
}
