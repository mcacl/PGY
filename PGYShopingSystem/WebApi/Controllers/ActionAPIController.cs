using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace PGYShopingSystem
{
    public class ActionAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public string Post(APIParmer value)
        {
            APIParmer.MyEnum myenum = (APIParmer.MyEnum)value.Type;
            string SQL = "";
            switch (myenum)
            {
                case APIParmer.MyEnum.Select:
                    var select = value.Data as ActSelect;
                    string selectsql = select.ToSQL();
                    SQL = selectsql; break;
                case APIParmer.MyEnum.Insert:
                    var insert = value.Data as ActInsert;
                    string insertsql = insert.ToSQL();
                    SQL = insertsql; break;
                case APIParmer.MyEnum.Update:
                    var update = value.Data as ActUpdate;
                    string updatesql = update.ToSQL();
                    SQL = updatesql; break;
                case APIParmer.MyEnum.Delete:
                    var delete = value.Data as ActSelect;
                    string deletesql = delete.ToSQL();
                    SQL = deletesql; break;
            }
            if (!string.IsNullOrEmpty(SQL))
            {

            }
            return "";
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}