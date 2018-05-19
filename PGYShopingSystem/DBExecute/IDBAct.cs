using System.Data;

namespace DBExecute
{
    public interface IDBAct
    {
        /// <summary>
        ///     查询 返回dataset
        /// </summary>
        /// <param name="SQL">查询sql</param>
        /// <returns>dataset</returns>
        DataSet DBSelectDS(string SQL);

        /// <summary>
        ///     查询 返回datatable
        /// </summary>
        /// <param name="SQL">查询sql</param>
        /// <returns>datatable</returns>
        DataTable DBSelectDT(string SQL);

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="SQL">更新SQL语句</param>
        /// <returns>更新条数</returns>
        int DBUpdata(string SQL);

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="SQL">插入SQL语句</param>
        /// <returns>拆入条数</returns>
        int DBInsert(string SQL);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="SQL">删除SQL语句</param>
        /// <returns>删除条数</returns>
        int DBDelete(string SQL);

        /// <summary>
        /// 执行多条sql语句或特殊语句
        /// </summary>
        /// <param name="SQL">sql脚本</param>
        /// <returns>受影响条数</returns>
        int DBOther(string SQL);
    }
}