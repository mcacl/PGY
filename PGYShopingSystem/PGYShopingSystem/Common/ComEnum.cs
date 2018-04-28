namespace PGYShopingSystem.Common
{
    public class ComEnum
    {
        /// <summary>
        /// 操作结果枚举
        /// </summary>
        public enum EnumActResult
        {
            Exception = -1, //异常
            Success = 1, //成功
            Error = 0 //失败
        }
        /// <summary>
        /// 操作枚举
        /// </summary>
        public enum ActEnum
        {
            Select = 1,
            Insert = 2,
            Update = 3,
            Delete = 4,
            Other = 5
        }
    }
}