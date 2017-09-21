using ReimuAPI.ReimuBase.Caller;
using ReimuAPI.ReimuBase.TgData;
using System;
using System.Collections.Generic;

namespace ReimuAPI.ReimuBase
{
    internal static class TempData
    {
        internal static UserInfo SelfInfo = null;
        internal static Dictionary<long, GroupUserInfo[]> tempAdminList = null;
        internal static DateTime adminListUptime;
        internal static ReimuConfig reimuConfig = null;
        internal static TgApi tgApi = null;
        internal static ExceptionListener exceptionListener = null;
        internal static List<PluginObject> pluginsList { get; set; }
    }
}
