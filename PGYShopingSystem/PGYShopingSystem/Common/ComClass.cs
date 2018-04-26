using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PGYShopingSystem.Common
{
    public class ComClass
    {
        public static string LogPath
        {
            get
            {
                return Path.Combine(ComWebSetting.LogPath, DateTime.Now.ToString("yyyy-MM-DD") + ".log");//log日志文件
            }
        }

    }
}