using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using PGYShopingSystem.Common;

namespace PGYShopingSystem
{
    public class ActionAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public string Post(APIParmer parmer)
        {
            var myenum = (APIParmer.MyEnum)parmer.Type;
            var SQL = "";
            var res = new ActResult();
            try
            {
                switch (myenum)
                {
                    case APIParmer.MyEnum.Select:
                        var select = parmer.GetAct() as ActSelect;
                        var selectsql = select.ToSQL();
                        SQL = selectsql;
                        var dt = DBExecute.DBAct.InitDBAct(ComWebSetting.ConnectString).DBSelectDT(SQL);
                        res.Data = JsonConvert.SerializeObject(dt);
                        break;
                    case APIParmer.MyEnum.Insert:
                        var insert = parmer.GetAct() as ActInsert;
                        var insertsql = insert.ToSQL();
                        SQL = insertsql;
                        res.Data = DBExecute.DBAct.InitDBAct(ComWebSetting.ConnectString).DBInsert(SQL).ToString();
                        break;
                    case APIParmer.MyEnum.Update:
                        var update = parmer.GetAct() as ActUpdate;
                        var updatesql = update.ToSQL();
                        SQL = updatesql;
                        res.Data = DBExecute.DBAct.InitDBAct(ComWebSetting.ConnectString).DBUpdata(SQL).ToString();
                        break;
                    case APIParmer.MyEnum.Delete:
                        var delete = parmer.GetAct() as ActDelete;
                        var deletesql = delete.ToSQL();
                        SQL = deletesql;
                        res.Data = DBExecute.DBAct.InitDBAct(ComWebSetting.ConnectString).DBDelete(SQL).ToString();
                        break;
                }
                res.Code = (int)ComEnum.EnumActResult.Success;
                res.Msg = "操作成功!";
            }
            catch (Exception ex)
            {
                res.Code = (int)ComEnum.EnumActResult.Exception;
                res.Msg = "接口异常!请确认参数是否正确!";
                Log.LogInBatchExceptWrite(ex, SQL);
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