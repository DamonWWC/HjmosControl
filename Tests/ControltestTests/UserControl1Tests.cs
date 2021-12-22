using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controltest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controltest.Tests
{
    [TestClass()]
    public class UserControl1Tests
    {
        [TestMethod()]
        public void AddTest()
        {
            int expected = 3;
            var dt = new UserControl1();
            int actual = dt.Add(1, 2);

            Assert.AreEqual(expected, actual);
            //Assert.Fail();
        }
    }
}