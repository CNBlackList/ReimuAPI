using ReimuAPI.ReimuBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReimuAPI.ReimuBase.Caller
{
    class PluginsCaller
    {
        internal static void callPlugins(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects);
                        }
                        catch (NotImplementedException) { }
                        catch (System.Reflection.TargetInvocationException e) {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static void callTextReceiver(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            Type validType = typeof(ITextMessageListener);
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects, validType);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects, validType);
                        }
                        catch (NotImplementedException) { }
                        catch (StopProcessException) { return; }
                        catch (System.Reflection.TargetInvocationException e)
                        {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static void callCommandReceiver(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            Type validType = typeof(ICommandReceiver);
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects, validType);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects, validType);
                        }
                        catch (NotImplementedException) { }
                        catch (StopProcessException) { return; }
                        catch (System.Reflection.TargetInvocationException e)
                        {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static void callStartReceiver(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            Type validType = typeof(ICommandReceiver);
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects, validType);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects, validType);
                        }
                        catch (NotImplementedException) { }
                        catch (StopProcessException) { return; }
                        catch (System.Reflection.TargetInvocationException e)
                        {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static void callMemberJoinReceiver(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            Type validType = typeof(IMemberJoinLeftListener);
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects, validType);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects, validType);
                        }
                        catch (NotImplementedException) { }
                        catch (StopProcessException) { return; }
                        catch (System.Reflection.TargetInvocationException e)
                        {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static void callOtherMessageReceiver(List<PluginObject> plugins, string method, string JsonMessage, object[] objects = null)
        {
            Type validType = typeof(IMemberJoinLeftListener);
            foreach (PluginObject pl in plugins)
            {
                if (pl.IsImportant)
                {
                    try
                    {
                        pl.callMessage(method, objects, validType);
                    }
                    catch (NotImplementedException) { }
                    catch (StopProcessException) { return; }
                    catch (System.Reflection.TargetInvocationException e)
                    {
                        if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                        {
                            return;
                        }
                        throw e;
                    }
                    catch (Exception e)
                    {
                        RAPI.GetExceptionListener().OnException(e, JsonMessage);
                    }
                }
                else
                {
                    new Task(() =>
                    {
                        try
                        {
                            pl.callMessage(method, objects, validType);
                        }
                        catch (NotImplementedException) { }
                        catch (StopProcessException) { return; }
                        catch (System.Reflection.TargetInvocationException e)
                        {
                            if (e.InnerException.GetType().IsAssignableFrom(typeof(StopProcessException)))
                            {
                                return;
                            }
                            throw e;
                        }
                    }).Start();
                }
            }
        }

        internal static string getHelpMessage(List<PluginObject> plugins, TgData.TgMessage RawMessage, string MessageType)
        {
            string msg = "";
            foreach (PluginObject pl in plugins)
            {
                string pluginhelpmsg = null;
                try
                {
                    pluginhelpmsg = pl.getHelpContent(RawMessage, MessageType);
                }
                catch (NotImplementedException) { }
                catch (Exception e)
                {
                    RAPI.GetExceptionListener().OnException(e);
                }
                if (pluginhelpmsg != null || pluginhelpmsg != "")
                {
                    msg += "Plugin: " + pl.PluginName + "\n" + pluginhelpmsg + "\n\n";
                }
            }
            return msg;
        }
    }
}
