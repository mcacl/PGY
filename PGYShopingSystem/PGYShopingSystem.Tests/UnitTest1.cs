using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;
using PGYShopingSystem.Common;

namespace PGYShopingSystem.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var datastr = "{'tablename':'xs_zdjbxx','wheret':'ysdm=^666666^'}";
            var datastr1 = "ËwÄ±²¼µ¾±½µwwÈÃ¯Ê´º²ÈÈw|wÇ¸µÂµÄwwÉÃ´½®®wÍ";
            var key = "PGY";
            var r1 = ComTool.CustomDecryption(datastr, key, false);
            var r2 = ComTool.CustomDecryption(r1, key, true);

            Assert.IsTrue(r1 == r2);
        }
    }
}