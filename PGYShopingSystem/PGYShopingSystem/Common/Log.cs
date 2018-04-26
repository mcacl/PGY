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
        /// <summary>
        /// 根据文件路径写日志 文件必须存在且目录存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="logtext">日志内容</param>
        public static void LogWrite(string logtext, string path)
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
        /// <summary>
        /// 根据文件路径写日志 文件或目录可以不存在
        /// </summary>
        /// <param name="logtext">日志内容</param>
        /// <param name="path">文件路径 为空时读取公共类配置的日志路径</param>
        public static void LogInBatchWrite(string logtext, string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = ComClass.LogPath;
            }
            ComFile.CreateFile(path);
            using (FileStream fs = File.Open(path, FileMode.Append))
            {
                byte[] bt = Encoding.UTF8.GetBytes(logtext);
                fs.Write(bt, 0, bt.Length);
                fs.Close();
            }
        }
    }
}