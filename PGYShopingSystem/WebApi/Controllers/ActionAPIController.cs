using System.Collections.Generic;
using System.Web.Http;

namespace PGYShopingSystem
{
    public class ActionAPIController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public string Post(APIParmer value)
        {
            var myenum = (APIParmer.MyEnum) value.Type;
            var SQL = "";
            switch (myenum)
            {
                case APIParmer.MyEnum.Select:
                    var select = value.Data as ActSelect;
                    var selectsql = select.ToSQL();
                    SQL = selectsql;
                    break;
                case APIParmer.MyEnum.Insert:
                    var insert = value.Data as ActInsert;
                    var insertsql = insert.ToSQL();
                    SQL = insertsql;
                    break;
                case APIParmer.MyEnum.Update:
                    var update = value.Data as ActUpdate;
                    var updatesql = update.ToSQL();
                    SQL = updatesql;
                    break;
                case APIParmer.MyEnum.Delete:
                    var delete = value.Data as ActSelect;
                    var deletesql = delete.ToSQL();
                    SQL = deletesql;
                    break;
            }
            if (!string.IsNullOrEmpty(SQL))
            {
            }
            return "";
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