using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ReimuAPI.ReimuBase
{
    public static class Log
    {
        public static void i(string content)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            Console.WriteLine("[INFO] [" + methodBase.DeclaringType.FullName + "] " + content);
        }

        public static void w(string content)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            Console.WriteLine("[WARN] [" + methodBase.DeclaringType.FullName + "] " + content);
        }

        public static void e(string content)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            System.Reflection.MethodBase methodBase = stackFrame.GetMethod();
            Console.WriteLine("[ERROR] [" + methodBase.DeclaringType.FullName + "] " + content);
        }
    }
}
