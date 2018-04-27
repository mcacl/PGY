using System.Configuration;

namespace PGYShopingSystem.Common
{
    public class ComWebSetting
    {
        public static string LogPath { get; private set; }
        public static string ConnectString { get; private set; }

        public static void InitSetting()
        {
            try
            {
                var logpath = ConfigurationManager.AppSettings["LogPath"];
                LogPath = logpath;//日志文件路径不含文件名
                ConnectString = ConfigurationManager.ConnectionStrings["PGYConstring"].ToString();
            }
            catch (System.Exception ex)
            {
                Log.LogInBatchExceptWrite(ex, "文件读取配置部分错误！");
            }
        }
    }
}