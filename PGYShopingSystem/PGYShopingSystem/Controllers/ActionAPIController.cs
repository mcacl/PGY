using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            ActResult res = new ActResult();
            try
            {
                switch (myenum)
                {
                    case APIParmer.MyEnum.Select:
                        ActSelect select = parmer.GetAct() as ActSelect;
                        var selectsql = select.ToSQL();
                        SQL = selectsql;
                        break;
                    case APIParmer.MyEnum.Insert:
                        ActInsert insert = parmer.GetAct() as ActInsert;
                        var insertsql = insert.ToSQL();
                        SQL = insertsql;
                        break;
                    case APIParmer.MyEnum.Update:
                        ActUpdate update = parmer.GetAct() as ActUpdate;
                        var updatesql = update.ToSQL();
                        SQL = updatesql;
                        break;
                    case APIParmer.MyEnum.Delete:
                        ActDelete delete = parmer.GetAct() as ActDelete;
                        var deletesql = delete.ToSQL();
                        SQL = deletesql;
                        break;
                }
            }
            catch (System.Exception ex)
            {
                res.Code = (int)ComEnum.EnumError.Exception;
                res.Msg = "接口异常!请确认参数是否正确!";
            }
            if (!string.IsNullOrEmpty(SQL))
            {

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