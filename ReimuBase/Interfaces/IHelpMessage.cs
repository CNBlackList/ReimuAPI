using ReimuAPI.ReimuBase.TgData;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface IHelpMessage
    {
        string GetHelpMessage(TgMessage RawMessage, string MessageType);
    }
}
