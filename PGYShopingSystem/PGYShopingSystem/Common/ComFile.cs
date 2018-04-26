using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace PGYShopingSystem.Common
{
    public class ComFile
    {
        /// <summary>
        /// 根据路径创建文件夹及文件
        /// </summary>
        /// <param name="floderpath">文件目录</param>
        /// <param name="filename">文件名</param>
        /// <returns>文件路径</returns>
        public static string CreateFile(string floderpath, string filename)
        {
            string path = Path.Combine(floderpath, filename);
            if (!Directory.Exists(floderpath))
            {
                Directory.CreateDirectory(floderpath);
            }
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            return path;
        }
        /// <summary>
        /// 根据文件路径创建文件夹及文件并返回文件目录路径
        /// </summary>
        /// <param name="filepath">文件路径含文件名</param>
        /// <returns>文件目录</returns>
        public static string CreateFile(string filepath)
        {
            var floderpath = Path.GetDirectoryName(filepath);
            var filename = Path.GetFileName(filepath);
            if (!Directory.Exists(floderpath))
            {
                Directory.CreateDirectory(floderpath);
            }
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Close();
            }
            return floderpath + filename;
        }
    }
}