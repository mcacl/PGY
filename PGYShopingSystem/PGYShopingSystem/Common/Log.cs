using System;
using System.IO;
using System.Text;

namespace PGYShopingSystem.Common
{
    public class Log
    {
        /// <summary>
        ///     根据文件路径写日志 文件必须存在且目录存在
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="logtext">日志内容</param>
        public static void LogWrite(string logtext, string path)
        {
            if (File.Exists(path))
                using (var fs = File.Open(path, FileMode.Append))
                {
                    var bt = Encoding.UTF8.GetBytes(logtext);
                    fs.Write(bt, 0, bt.Length);
                    fs.Close();
                }
        }

        /// <summary>
        ///     根据文件路径写日志 文件或目录可以不存在
        /// </summary>
        /// <param name="logtext">日志内容</param>
        /// <param name="path">文件路径 为空时读取公共类配置的日志路径</param>
        public static void LogInBatchWrite(string logtext, string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = ComClass.LogPath;
            ComFile.CreateFile(path);
            using (var fs = File.Open(path, FileMode.Append))
            {
                var bt = Encoding.UTF8.GetBytes(logtext);
                fs.Write(bt, 0, bt.Length);
                fs.Close();
            }
        }
        /// <summary>
        /// 根据异常记录日志 自动创建文件路径及日志文件
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="infomsg">附加消息</param>
        /// <param name="path">日志文件路径 默认取公共类日志路径</param>
        public static void LogInBatchExceptWrite(Exception ex, string parmdata, string infomsg = "", string path = "")
        {
            if (string.IsNullOrEmpty(path)) path = ComClass.LogPath;
            ComFile.CreateFile(path);
            using (var fs = File.Open(path, FileMode.Append))
            {
                string logtext = "时间:" + DateTime.Now.ToString("yyyy-MM-DD HH:mm:ss") + Environment.NewLine + "异常消息:" +
                                 ex.Message + Environment.NewLine + "内部异常:" + ex.InnerException + Environment.NewLine +
                                 "附加信息:" + infomsg + Environment.NewLine + "SQL参数:" + parmdata + Environment.NewLine;
                var bt = Encoding.UTF8.GetBytes(logtext);
                fs.Write(bt, 0, bt.Length);
                fs.Close();
            }
        }
    }
}