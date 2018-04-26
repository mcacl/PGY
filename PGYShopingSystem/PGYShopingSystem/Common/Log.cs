using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace PGYShopingSystem.Common
{
    public class Log
    {
        public static void LogWrite(string path, string logtext)
        {
            if (File.Exists(path))
            {
                using (FileStream fs = File.Open(path, FileMode.Append))
                {
                    byte[] bt = Encoding.UTF8.GetBytes(logtext);
                    fs.Write(bt, 0, bt.Length);
                    fs.Close();
                }
            }
        }

        public static void LogInBatchWrite()
        {

        }
    }
}