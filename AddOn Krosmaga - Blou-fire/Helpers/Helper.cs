using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog.Fluent;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
    public class Helper
    {
        public static bool TryOpenUrl(string url, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "")
        {
            try
            {
               // Log.Info("[Helper.TryOpenUrl] " + url, memberName, sourceFilePath);
                Process.Start(url);
                return true;
            }
            catch (Exception e)
            {
               // Log.Error("[Helper.TryOpenUrl] " + e, memberName, sourceFilePath);
                return false;
            }
        }

    }
}
