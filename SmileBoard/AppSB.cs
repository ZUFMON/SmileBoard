using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smile
{
    /// <summary>
    /// Static Application Variables
    /// </summary>
    public static class AppSB
    {
        public static TypeShowLog showMsgLog = TypeShowLog.MsgBox;

        public static event EventHandlerLog LogEvent;
        public delegate void EventHandlerLog(string msg);

        public static void Log(string msg, bool msgbox = false)
        {
            if (LogEvent != null) LogEvent(msg);

            if (showMsgLog == TypeShowLog.MsgBox && msgbox)
                System.Windows.MessageBox.Show(msg);
            else
                Console.WriteLine(msg);
        }
    }

    public enum TypeShowLog
    {
        MsgBox,
        Console,
    }
}
