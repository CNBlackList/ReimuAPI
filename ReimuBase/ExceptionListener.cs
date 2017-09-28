using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ReimuAPI.ReimuBase
{
    public class ExceptionListener
    {
        private long AdminGroupID;

        public ExceptionListener()
        {
            AdminGroupID = new ConfigManager().getConfig().admin_group;
        }

        public void OnException(Exception exception, string JsonString = null)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            string errmsg = "[ERROR] [" + methodBase.DeclaringType.FullName + "] ";
            errmsg += "Error: Have an exception: " + exception.ToString();
            if (JsonString != null)
            {
                errmsg += "\n\nRAW Json: " + JsonString;
            }
            Console.WriteLine(errmsg);
            if (AdminGroupID != 0)
            {
                TgApi.getDefaultApiConnection().sendMessage(AdminGroupID, errmsg);
            }
        }

        public void OnJsonDecodeError(Exception exception, string JsonString)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            string errmsg = "[ERROR] [" + methodBase.DeclaringType.FullName + "] ";
            errmsg += "Error: JSON decode error: " + exception.ToString();
            if (JsonString != null)
            {
                errmsg += "\n\nRAW Json: " + JsonString;
            }
            Console.WriteLine(errmsg);
            if (AdminGroupID != 0)
            {
                TgApi.getDefaultApiConnection().sendMessage(AdminGroupID, errmsg);
            }
        }
    }
}
