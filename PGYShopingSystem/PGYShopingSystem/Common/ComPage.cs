using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace PGYShopingSystem.Common
{
    public class ComPage
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageCurrt { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageNum { get; private set; }
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int PageDataCount { get; private set; }
        /// <summary>
        /// 当前页数据
        /// </summary>
        public DataTable PageData { get; private set; }
        /// <summary>
        ///1.curpage当前页 2.pagenum页数 3.总条数 4.pagesize 5.分页数据表
        /// </summary>
        /// <param name="pagetup"></param>
        public ComPage(Tuple<int, int, int, int, DataTable> pagetup)
        {
            if (pagetup != null)
            {
                this.PageCurrt = pagetup.Item1;
                this.PageNum = pagetup.Item2;
                this.PageDataCount = pagetup.Item3;
                this.PageSize = pagetup.Item4;
                this.PageData = pagetup.Item5;
            }
        }
    }
    public class PageParam
    {
        public string PageSQL { get; set; }
        public int PageSize { get; set; }
        public int PageCurrt { get; set; }
    }
}