using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Dynamic;
using DBExecute;

namespace PGYShopingSystem.Common
{
    public class ComWebSetting
    {
        /// <summary>
        /// 数据连接配置集合
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectConfig { get; private set; }
        /// <summary>
        /// AppSetting配置集合
        /// </summary>
        public static NameValueCollection AppSetingConfig { get; private set; }
        /// <summary>
        /// 日志文件路径
        /// </summary>
        public static string LogPath { get; private set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectString { get; private set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public static string DBType { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public static string KEY { get; private set; }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="Type">数据库类型</param>
        public static void InitSetting(DBTypeEnum.DBType Type)
        {
            try
            {
                ConnectConfig = ConfigurationManager.ConnectionStrings;
                AppSetingConfig = ConfigurationManager.AppSettings;
                LogPath = ConfigurationManager.AppSettings["LogPath"];//日志文件路径不含文件名
                DBType = Enum.GetName(Type.GetType(), Type);
                ConnectString = ConfigurationManager.ConnectionStrings[DBType].ToString();
                KEY = ConfigurationManager.AppSettings["Key"];
            }
            catch (System.Exception ex)
            {
                Log.LogInBatchExceptWrite(ex, "文件读取配置部分错误！");
            }
        }
    }
}