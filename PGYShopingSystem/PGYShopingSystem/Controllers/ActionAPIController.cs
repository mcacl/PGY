﻿using System;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public string Post(APIParmer parmer)
        {
            var SQL = "";
            var res = new ActResult();
            var type = DBTypeEnum.DBType.Sqlite;
            try
            {
                var myenum = (ComEnum.ActEnum)parmer.Type;
                //DBAct dbact = new DBAct();
                switch (myenum)
                {
                    case ComEnum.ActEnum.Select:
                        var select = parmer.GetAct() as ActSelect;
                        var selectsql = select.ToSQL();
                        SQL = selectsql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            //var dt = dbact.InitDBAct(type, ComWebSetting.ConnectString).DBSelectDS1(SQL);
                            var dt = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBSelectDS1(SQL);
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                            res.Data = JsonConvert.SerializeObject(dt);
                        }
                        break;
                    /*case ComEnum.ActEnum.Insert:
                        var insert = parmer.GetAct() as ActInsert;
                        var insertsql = insert.ToSQL();
                        SQL = insertsql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBInsert(SQL).ToString();
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Update:
                        var update = parmer.GetAct() as ActUpdate;
                        var updatesql = update.ToSQL();
                        SQL = updatesql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBUpdata(SQL).ToString();
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Delete:
                        var delete = parmer.GetAct() as ActDelete;
                        var deletesql = delete.ToSQL();
                        SQL = deletesql;
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBDelete(SQL).ToString();
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Other:
                        SQL = parmer.GetAct().ToString();
                        if (!string.IsNullOrEmpty(SQL))
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).DBOther(SQL).ToString();
                            res.Code = (int)ComEnum.EnumActResult.Success;
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
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;
                    case ComEnum.ActEnum.Proc:
                        ComProcParam procparam = parmer.GetAct() as ComProcParam;
                        if (procparam != null)
                        {
                            res.Data = DBExecute.DBAct.InitDBAct(type, ComWebSetting.ConnectString).Proc(procparam.ProcName, procparam.GetOracleParam(), procparam.IsRetTable).ToString();
                            res.Code = (int)ComEnum.EnumActResult.Success;
                            res.Msg = "操作成功!";
                        }
                        break;*/
                }
                if (res.Code != (int)ComEnum.EnumActResult.Success)
                {
                    res.Code = (int)ComEnum.EnumActResult.Error;
                    res.Msg = "操作失败!参数转换不合规范";
                }
            }
            catch (Exception ex)
            {
                res.Code = (int)ComEnum.EnumActResult.Exception;
                res.Msg = "接口异常!请确认参数是否正确!";
                Log.LogInBatchExceptWrite(ex, parmer.Data.ToString(), SQL);
            }
            return JsonConvert.SerializeObject(res);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}