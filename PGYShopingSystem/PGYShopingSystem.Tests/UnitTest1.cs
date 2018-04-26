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
            MyClass mc = new MyClass() { a = 1, b = "1" };
            string json = "{\"Type\":\"4\",\"Data\":\"{\'TableName\':\'xs_h\',\'WhereT\':\'1=1\'}\"}";
            var js = new APIParmer() { Type = 4, Data = "{'TableName':'xs_h','WhereT':'1=1'}" };
            Assert.IsTrue(js != null);
        }
    }

    class MyClass
    {
        public int a;
        public string b;
    }
}