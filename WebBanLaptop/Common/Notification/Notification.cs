using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLaptop
{
    public class Notification
    {
        public static bool Has_flash()
        {
            if (HttpContext.Current.Application["Notification"].Equals("")) return false;
            return true;
        }
        public static void SetNotification1_5s(string msg1_5s, string msgType1_5s)
        {
            var tb = new ModelNotification();
            tb.Msg1_5s = msg1_5s;
            tb.MsgType1_5s = msgType1_5s;
            HttpContext.Current.Application["Notification"] = tb;
        }
        public static void SetNotification3s(string msg3s, string msgType3s)
        {
            var tb = new ModelNotification();
            tb.Msg3s = msg3s;
            tb.MsgType3s = msgType3s;
            HttpContext.Current.Application["Notification"] = tb;
        }
        public static void SetNotification5s(string msg5s, string msgType5s)
        {
            var tb = new ModelNotification();
            tb.Msg5s = msg5s;
            tb.MsgType5s = msgType5s;
            HttpContext.Current.Application["Notification"] = tb;
        }
        public static ModelNotification Get_flash()
        {
            var Notifi = (ModelNotification)HttpContext.Current.Application["Notification"];
            HttpContext.Current.Application["Notification"] = "";
            return Notifi;
        }
    }
}