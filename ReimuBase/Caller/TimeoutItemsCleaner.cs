using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReimuAPI.ReimuBase.Caller
{
    public class TimeoutItemsCleaner
    {
        private bool Lock = false;

        public void StartCleanDaemon()
        {
            if (Lock)
            {
                return;
            }
            Thread.Sleep(900);
            Log.i("Calling plugins to clean items.");
            callMemberJoinReceiver(TempData.pluginsList);
        }

        private void callMemberJoinReceiver(List<PluginObject> plugins)
        {
            TempData.tempAdminList = null;
            foreach (PluginObject pl in plugins)
            {
                try
                {
                    pl.callClear();
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
        }
    }
}
