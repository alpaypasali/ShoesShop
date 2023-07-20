using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace UnitTestDemo
{
    [TestClass]
    public class ProductContoller
    {
        [TestMethod]
        public void TestMethod1()
        {
            var controller = new ProductContoller();
            var result = (controller.Index()) as ViewResult;
        }
    }
}
