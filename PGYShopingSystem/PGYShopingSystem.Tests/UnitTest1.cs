using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace PGYShopingSystem.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[]
            {
                new DataColumn("A", typeof(string)), new DataColumn("B", typeof(int))
            });
            var dr = dt.NewRow();
            dr[0] = "a1";
            dr[1] = 1;
            dt.Rows.Add(dr);
            var js = JsonConvert.SerializeObject(dt);
            Assert.IsTrue(js != null);
        }
    }
}