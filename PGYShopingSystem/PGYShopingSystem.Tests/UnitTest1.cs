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
            string Data = null;
            //string SQl = Data.Replace("~", "\"").Replace("!", "'");
            var a = Data.Split(',');
            Assert.IsTrue(a != null);
        }
    }
}