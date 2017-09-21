using ReimuAPI.ReimuBase.Caller;
using ReimuAPI.ReimuBase.TgData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReimuAPI.ReimuBase
{
    public class RAPI
    {
        public static ExceptionListener GetExceptionListener()
        {
            if (TempData.exceptionListener == null)
            {
                ExceptionListener el = new ExceptionListener();
                TempData.exceptionListener = el;
                return el;
            } else
            {
                return TempData.exceptionListener;
            }
        }

        public static void loadPlugins(ExceptionListener exceptionListener = null)
        {
            string[] importantPlugins = new ConfigManager().getConfig().plugins.important;
            string[] normalPlugins = new ConfigManager().getConfig().plugins.normal;
            List<PluginObject> pluginsList = new List<PluginObject> { };
            foreach (string i in importantPlugins)
            {
                string pluginsBaseDir = AppDomain.CurrentDomain.BaseDirectory + "plugins\\";
                try
                {
                    PluginObject pluginObject = new PluginObject(pluginsBaseDir + i + ".dll", true, i);
                    pluginsList.Add(pluginObject);
                    Log.i("Plugin \"" + i + "\" (important) load success");
                }
                catch (Exception e)
                {
                    GetExceptionListener().OnException(e);
                }
            }
            foreach (string i in normalPlugins)
            {
                string pluginsBaseDir = AppDomain.CurrentDomain.BaseDirectory + "plugins\\";
                try
                {
                    PluginObject pluginObject = new PluginObject(pluginsBaseDir + i + ".dll", false, i);
                    pluginsList.Add(pluginObject);
                    Log.i("Plugin \"" + i + "\" (normal) load success");
                }
                catch (Exception e)
                {
                    GetExceptionListener().OnException(e);
                }
            }
            TempData.pluginsList = pluginsList;
        }

        public static string getHelpContent(TgMessage message)
        {
            return PluginsCaller.getHelpMessage(TempData.pluginsList, message, message.chat.type);
        }

        public static bool getIsBotAdmin(int UserID)
        {
            ReimuConfig config = new ConfigManager().getConfig();
            foreach (int i in config.admin_list)
            {
                if (i == UserID) return true;
            }
            return false;
        }
    }
}
