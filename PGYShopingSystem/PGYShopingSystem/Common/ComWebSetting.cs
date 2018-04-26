using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PGYShopingSystem.Common
{
    public class ComWebSetting
    {
        public static string LogPath { get; private set; }

        public static void InitSetting()
        {
            string logpath = ConfigurationManager.AppSettings["LogPath"];
            LogPath = logpath;
        }
    }
}