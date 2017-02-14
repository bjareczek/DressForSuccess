using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DressForSuccess.Tests
{
    [TestClass()]
    public class DressRulesTests
    {
        GetDressed _GetDressed = new GetDressed();
        DressRules _DressRules;

        [TestInitialize]
        public void TestInitialize()
        {
            _DressRules = new DressRules(_GetDressed);
        }

        [TestMethod()]
        public void IsAfterRequiredDressCommandTest_IfEmptyCommands_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            bool isBefore = _DressRules.IsAfterRequiredDressCommand(3, dressCommands);
            Assert.IsFalse(isBefore);
        }

        [TestMethod()]
        public void IsAfterRequiredDressCommandTest_IfOneCommand_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            dressCommands.Add(4, "test1");
            bool isBefore = _DressRules.IsAfterRequiredDressCommand(3, dressCommands);
            Assert.IsFalse(isBefore);
        }

        [TestMethod()]
        public void IsAfterRequiredDressCommandTest_IfTwoCommands_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            int testCommand1 = 2;
            int testCommand2 = 1;
            int testCommand3 = 3;
            dressCommands.Add(testCommand1, "test1");
            dressCommands.Add(testCommand2, "test2");
            bool isBefore = _DressRules.IsAfterRequiredDressCommand(testCommand3, dressCommands);
            Assert.IsFalse(isBefore);
        }

        [TestMethod()]
        public void IsAfterRequiredDressCommandTest_IfTwoCommands_InCorrectOrder_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            int testPreReq = 2;
            int testCommand = 1;
            dressCommands.Add(testPreReq, "test1");
            dressCommands.Add(testCommand, "test2");
            bool isBefore = _DressRules.IsAfterRequiredDressCommand(testPreReq, dressCommands);
            Assert.IsTrue(isBefore);
        }


        [TestMethod()]
        public void IsFirstInCommandListTest_IfOneCommandAlready_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            int testCommand = 1;
            dressCommands.Add(testCommand, "test1");
            bool isFirst = _DressRules.IsFirstInCommandList(dressCommands);
            Assert.IsFalse(isFirst);
        }

        [TestMethod()]
        public void IsFirstInCommandListTest_IfMultipleCommandsAlready_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            int testCommand = 1;
            int testCommandTwo = 2;
            dressCommands.Add(testCommand, "test1");
            dressCommands.Add(testCommandTwo, "test2");
            bool isFirst = _DressRules.IsFirstInCommandList(dressCommands);
            Assert.IsFalse(isFirst);
        }

        [TestMethod()]
        public void IsFirstInCommandListTest_IfNoCommandsYet_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            bool isFirst = _DressRules.IsFirstInCommandList(dressCommands);
            Assert.IsTrue(isFirst);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithExceptions_Hot_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            List<int> dressCommandsToExclude = new List<int> { 7, 8 };
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(2, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Hot, dressCommandsToExclude);
            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithoutExceptions_Hot_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(2, "test");
            dressCommands.Add(7, "test");
            dressCommands.Add(8, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Hot);
            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfInvalidValidWithExceptions_Hot_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            List<int> dressCommandsToExclude = new List<int> { 7, 8 };
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(3, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Hot, dressCommandsToExclude);
            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithoutExceptions_Hot_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(3, "test");
            dressCommands.Add(2, "test");
            dressCommands.Add(7, "test");
            dressCommands.Add(8, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Hot);
            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithExceptions_Cold_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            List<int> dressCommandsToExclude = new List<int> { 7, 8 };
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(2, "test");
            dressCommands.Add(3, "test");
            dressCommands.Add(5, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Cold, dressCommandsToExclude);
            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithoutExceptions_Cold_ExpectTrue()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(2, "test");
            dressCommands.Add(3, "test");
            dressCommands.Add(5, "test");
            dressCommands.Add(7, "test");
            dressCommands.Add(8, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Cold);
            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfInvalidValidWithExceptions_Cold_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            List<int> dressCommandsToExclude = new List<int> { 7, 8 };
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(4, "test");
            dressCommands.Add(3, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Cold, dressCommandsToExclude);
            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void AllOtherValidDressCommandsInListTest_IfValidWithoutExceptions_Cold_ExpectFalse()
        {
            Dictionary<int, string> dressCommands = new Dictionary<int, string>();
            dressCommands.Add(1, "test");
            dressCommands.Add(6, "test");
            dressCommands.Add(3, "test");
            dressCommands.Add(2, "test");
            dressCommands.Add(7, "test");
            dressCommands.Add(8, "test");
            var isValid = _DressRules.AllOtherValidDressCommandsInList(dressCommands, Temperatures.Cold);
            Assert.IsFalse(isValid);
        }
    }
}