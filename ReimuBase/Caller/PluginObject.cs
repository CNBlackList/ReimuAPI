using ReimuAPI.ReimuBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReimuAPI.ReimuBase.Caller
{
    public class PluginObject
    {

        internal Assembly assembly { get; }
        internal List<CallablePlugin> messageListener { get; } = null;
        internal List<CallablePlugin> helpObjects { get; } = null;
        internal List<CallablePlugin> clearTimeoutListener { get; } = null;
        public string PluginName { get; }
        internal bool IsImportant { get; }

        public PluginObject(string DllPath, bool IsImportant, string PluginName)
        {
            assembly = Assembly.LoadFrom(DllPath);
            Type[] AllowInterfaces = new Type[]
            {
                typeof(ITextMessageListener),
                typeof(IOtherMessageReceiver),
                typeof(IMemberJoinLeftListener),
                typeof(IMessageListener),
                typeof(ICommandReceiver),
                typeof(IStartReceiver)

            };
            Type helpMsgType = typeof(IHelpMessage);
            Type clearTimeouteType = typeof(IClearItemsReceiver);
            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                foreach (Type at in AllowInterfaces)
                {
                    if (at.IsAssignableFrom(t))
                    {
                        object obj = Activator.CreateInstance(t);
                        if (messageListener == null)
                        {
                            messageListener = new List<CallablePlugin> { new CallablePlugin(t, obj) };
                        }
                        else
                        {
                            messageListener.Add(new CallablePlugin(t, obj));
                        }
                        break;
                    }
                }
                if (helpMsgType.IsAssignableFrom(t))
                {
                    object obj = Activator.CreateInstance(t);
                    if (helpObjects == null)
                    {
                        helpObjects = new List<CallablePlugin> { new CallablePlugin(t, obj) };
                    }
                    else
                    {
                        helpObjects.Add(new CallablePlugin(t, obj));
                    }
                }
                if (clearTimeouteType.IsAssignableFrom(t))
                {
                    object obj = Activator.CreateInstance(t);
                    if (clearTimeoutListener == null)
                    {
                        clearTimeoutListener = new List<CallablePlugin> { new CallablePlugin(t, obj) };
                    }
                    else
                    {
                        clearTimeoutListener.Add(new CallablePlugin(t, obj));
                    }
                }
            }
            this.IsImportant = IsImportant;
            this.PluginName = PluginName;
        }

        internal void callMessage(string MethodName, object[] parameters)
        {
            if (messageListener == null)
            {
                return;
            }
            foreach (CallablePlugin plugin in messageListener)
            {
                if (typeof(IMessageListener).IsAssignableFrom(plugin.type))
                {
                    try
                    {
                        CallbackMessage resultobj = (CallbackMessage)plugin.callPlugin(MethodName, parameters);
                        GetException(resultobj);
                    }
                    catch (NotImplementedException) { }
                }
            }
        }

        internal void callMessage(string MethodName, object[] parameters, Type type)
        {
            if (messageListener == null)
            {
                return;
            }
            Type allmsgreciver = typeof(IOtherMessageReceiver);
            Type messagelistener = typeof(IMessageListener);
            object[] processAllInterfacesParamters = new object[] { parameters[0], parameters[1] };
            foreach (CallablePlugin plugin in messageListener)
            {
                if (allmsgreciver.IsAssignableFrom(plugin.type))
                { // 判断当前循环中的类是不是处理所有消息的接口
                    if (allmsgreciver.IsAssignableFrom(type))
                    { // 判断传入的类是不是处理所有消息的接口
                        try
                        {
                            CallbackMessage resultobj = (CallbackMessage)plugin.callPlugin(MethodName, parameters);
                            GetException(resultobj);
                        }
                        catch (NotImplementedException) { }
                    }
                    else
                    {
                        try
                        {
                            CallbackMessage resultobj = (CallbackMessage)plugin.callPlugin("ReceiveAllNormalMessage", processAllInterfacesParamters);
                            GetException(resultobj);
                        }
                        catch (NotImplementedException) { }
                    }
                    continue;
                }
                if (allmsgreciver.IsAssignableFrom(type))
                { // 如果传入的是处理所有消息的接口则忽略（因为没有共同的方法）
                    continue;
                }
                else if (messagelistener.IsAssignableFrom(plugin.type))
                { // 如果这个类是处理普通消息的接口则运行这个插件
                    try
                    {
                        CallbackMessage resultobj = (CallbackMessage)plugin.callPlugin(MethodName, parameters);
                        GetException(resultobj);
                    }
                    catch (NotImplementedException) { }
                    continue;
                }
                if (type.IsAssignableFrom(plugin.type))
                { // 如果这个类与传入的接口相同则运行这个插件
                    try
                    {
                        CallbackMessage resultobj = (CallbackMessage)plugin.callPlugin(MethodName, parameters);
                        GetException(resultobj);
                    }
                    catch (NotImplementedException) { }
                }
            }
        }
        
        internal void callClear()
        {
            if (clearTimeoutListener == null)
            {
                return;
            }
            foreach (CallablePlugin plugin in clearTimeoutListener)
            {
                try
                {
                    plugin.callPlugin("ClearItems", new object[] { });
                }
                catch (NotImplementedException) { }
            }
        }

        internal string getHelpContent(TgData.TgMessage RawMessage, string MessageType)
        {
            if (helpObjects == null)
            {
                return "";
            }
            string helpmsg = "";
            foreach (CallablePlugin plugin in helpObjects)
            {
                try
                {
                    object helpobj = plugin.callPlugin("GetHelpMessage", new object[] { RawMessage, MessageType });
                    if (helpobj != null)
                    {
                        helpmsg += (string)helpobj;
                    }
                }
                catch (NotImplementedException) { }
            }
            return helpmsg;
        }

        internal void GetException(CallbackMessage msg)
        {
            if (msg.StopProcess)
            {
                throw new StopProcessException();
            }
        }
    }

    public class CallablePlugin
    {
        internal Type type { get; }
        internal object obj { get; }

        internal CallablePlugin(Type type, object obj)
        {
            this.type = type;
            this.obj = obj;
        }

        internal Object callPlugin(string MethodName, object[] parameters)
        {
            MethodInfo[] methodInfoList = type.GetMethods();
            MethodInfo methodInfo = null;
            foreach (MethodInfo method in methodInfoList)
            {
                if (method.Name == MethodName)
                {
                    methodInfo = method;
                }
            }
            if (methodInfo == null)
            {
                Log.w("Call plugin failed: Cannot find method: " + MethodName);
                return new CallbackMessage();
            }
            return methodInfo.Invoke(obj, parameters);
        }
    }
}
