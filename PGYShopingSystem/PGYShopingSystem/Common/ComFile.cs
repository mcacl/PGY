using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PGYShopingSystem.Common
{
    public class ComFile
    {
        /// <summary>
        /// 查找对应行政区的文件夹路径
        /// </summary>
        /// <param name="xian"></param>
        /// <returns></returns>
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
    }
}