using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PGYShopingSystem.Common
{
    /// <summary>
    /// 使用工具类
    /// </summary>
    public class ComTool
    {
        /// <summary>
        /// 自定义加解密
        /// </summary>
        /// <param name="datastr">加密或解密字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="IsUnDo">是否是解密</param>
        /// <paramref name="step">步进值 step的倍数加密解密 相同的字符串加解密步进值需要一致</paramref>
        /// <returns>密文或明文</returns>
        public static string CustomDecryption(string datastr, string key, bool IsUnDo = false, int step = 1)
        {
            var data = "";
            step = step == 0 ? 1 : step;
            if (datastr.Length > 0 && key.Length > 0)
            {
                //C#字符串编码是unicode
                for (int a = 0; a < datastr.Length; a++)
                {
                    var code = datastr[a];
                    var Y = a % key.Length;//密钥加数
                    var keycode = key[Y];
                    int tempCode = IsUnDo ? code - keycode : code + keycode;
                    if (tempCode >= 65536) { tempCode = code; }
                    data += a % step == 0 ? (char)tempCode : datastr[a];
                }
            }
            return data;
        }
    }
}