using System;
using System.IO;

namespace PGYShopingSystem.Common
{
    public class ComClass
    {
        public static string LogPath
        {
            get
            {
                return Path.Combine(ComWebSetting.LogPath, DateTime.Now.ToString("yyyy-MM-dd") + ".log"); //log日志文件
            }
        }
    }
}