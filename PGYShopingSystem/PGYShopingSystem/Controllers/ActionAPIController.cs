using System;
using System.Collections.Generic;
using System.Web.Http;
using DBExecute;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using PGYShopingSystem.Common;

namespace PGYShopingSystem
{
    public class ActionAPIController : ApiController
    {
        // GET api/<controller>/5
        public string Get()
        {
            var res = new ActResult();
            res.Code = ComEnum.EnumActResult.Success;
            res.IsEncrypt = false;
            res.Msg = "调用测试接口成功!";

            return JsonConvert.SerializeObject(res);
        }

        // POST api/<controller>
        public string Post(APIParmer parmer)
        {
            var SQL = "";
            var res = new ActResult();
            try
            {
                var myenum = (ComEnum.ActEnum)parmer.Type;
                var type = (DBTypeEnum.DBType)Enum.Parse(typeof(DBTypeEnum.DBType), ComWebSetting.DBType);
                switch (myenum)
                {
                    case ComEnum.ActEnum.Select:
                        //查尽量带t {"Type":"1","Data":"{'tablename':'xs_zdjbxx t','fields':'t.zl,t.ysdm','WhereT':'t.zddm=^610821104200GB00039^'}"}
                        var select = parmer.GetAct() as ActSelect;
                        var selectsql = select.ToSQL();
                        SQL = selectsql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            var dt = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBSelectDT(SQL);
                            res.Data = JsonConvert.SerializeObject(dt);
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Insert:
                        //增values不加引号 {"Type":"2","Data":"{'tablename':'xs_zdjbxx','fields':'zl,ysdm','values':'神木县大柳塔镇光明路66666,666666'}"}
                        var insert = parmer.GetAct() as ActInsert;
                        var insertsql = insert.ToSQL();
                        SQL = insertsql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBInsert(SQL).ToString();
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Update:
                        //更新values不加引号 {"Type":"3","Data":"{'tablename':'xs_zdjbxx t','fields':'zl,ysdm','values':'神木县大柳塔镇光明路,6001010001','WhereT':'t.zddm=^610821104200GB00039^'}"}
                        var update = parmer.GetAct() as ActUpdate;
                        var updatesql = update.ToSQL();
                        SQL = updatesql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBUpdata(SQL).ToString();
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Delete:
                        //删除 {"Type":"4","Data":"{'tablename':'xs_zdjbxx','wheret':'ysdm=^666666^'}"}
                        var delete = parmer.GetAct() as ActDelete;
                        var deletesql = delete.ToSQL();
                        SQL = deletesql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBDelete(SQL).ToString();
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Other:
                        //多条不用begin不行 {"Type":"5","Data":"begin update xs_zdjbxx set zl=zl||'1' where zddm='610821104200GB00030';update xs_zdjbxx set zl=zl||'2' where zddm='610821104200GB00031'; end;"}
                        SQL = parmer.GetAct().ToString();
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBOther(SQL).ToString();
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.SelectPageProc:
                        PageParam pageparam = parmer.GetAct() as PageParam;
                        if (pageparam != null)
                        {
                            var intup = new Tuple<string, int, int>(pageparam.PageSQL, pageparam.PageSize, pageparam.PageCurrt);
                            var outtup = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBProcPage(intup);
                            ComPage page = new ComPage(outtup);
                            res.Data = JsonConvert.SerializeObject(page);
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Proc:
                        ComProcParam procparam = parmer.GetAct() as ComProcParam;
                        if (procparam != null)
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).Proc(procparam.ProcName, procparam.GetOracleParam(), procparam.IsRetTable).ToString();
                            res.Code = ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                }
                if (res.Code != ComEnum.EnumActResult.Success)
                {
                    res.Code = ComEnum.EnumActResult.Error;
                    res.Msg = "操作失败!参数转换不合规范";
                }
            }
            catch (Exception ex)
            {
                res.Code = ComEnum.EnumActResult.Exception;
                res.Msg = "接口异常!请确认参数是否正确!";
                Log.LogInBatchExceptWrite(ex, parmer.Data.ToString(), SQL);
            }
            return JsonConvert.SerializeObject(res);
        }
    }
}