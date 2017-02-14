using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DressForSuccess.Tests
{
    [TestClass()]
    public class DressOptionTests
    {
        [TestMethod]
        public void DressOptionsToStringTest_WhenInstantiated_ExpectFourFields()
        {
            //IF THIS FAILS, UPDATE DressOptions .ToString() override for proper test comparisons!
            DressOption dressOptions = new DressOption("put on scarf", "fail", "scarf");
            Assert.IsTrue(dressOptions.GetType().GetProperties().Count() == 4);
        }

        [TestMethod]
        public void DressOptionsToStringTest_WhenCalled_ExpectCorrectString()
        {
            DressOption dressOptions = new DressOption("put on scarf", "fail", "scarf");
            Assert.IsTrue(dressOptions.ToString() == "put on scarf, fail, scarf");
        }

        [TestMethod]
        public void DressOptionsToStringTest_WhenCalledWithRules_ExpectCorrectString()
        {
            var rules = new List<Tuple<Rules, Temperatures, int>> { new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Any, -1) };
            DressOption dressOptions = new DressOption("put on scarf", "fail", "scarf", rules);
            Assert.IsTrue(dressOptions.ToString() == "put on scarf, fail, scarf");
        }
    }
}