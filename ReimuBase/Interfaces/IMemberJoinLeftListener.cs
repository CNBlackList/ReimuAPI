using ReimuAPI.ReimuBase.TgData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReimuAPI.ReimuBase.Interfaces
{
    public interface IMemberJoinLeftListener
    {
        CallbackMessage OnGroupMemberJoinReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
        CallbackMessage OnSupergroupMemberJoinReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);

        CallbackMessage OnGroupMemberLeftReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
        CallbackMessage OnSupergroupMemberLeftReceive(TgMessage RawMessage, string JsonMessage, UserInfo JoinedUser);
    }
}
