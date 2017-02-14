using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DressForSuccess;
using System.Collections.Generic;
using System.Linq;

namespace DressForSuccessTest
{
    [TestClass]
    public class GetDressedTest
    {
        Dictionary<int, DressOption> AvailableDressOptionsTest;
        GetDressed _GetDressed = new GetDressed();
        DressRules _DressRules;

        [TestInitialize]
        public void TestInitialize()
        {
            _DressRules = new DressRules(_GetDressed);
            AvailableDressOptionsTest = new Dictionary<int, DressOption>();
            AvailableDressOptionsTest.Add(1, new DressOption("Put on footwear", "sandals", "boots"));
            AvailableDressOptionsTest.Add(2, new DressOption("Put on headwear", "sun visor", "hat"));
            AvailableDressOptionsTest.Add(3, new DressOption("Put on socks", null, "socks"));
            AvailableDressOptionsTest.Add(4, new DressOption("Put on shirt", "t-shirt", "shirt"));
            AvailableDressOptionsTest.Add(5, new DressOption("Put on jacket", null, "jacket"));
            AvailableDressOptionsTest.Add(6, new DressOption("Put on pants", "shorts", "pants"));
            AvailableDressOptionsTest.Add(7, new DressOption("Leave house", "leaving house", "leaving house"));
            AvailableDressOptionsTest.Add(8, new DressOption("Take off pajamas", "Removing PJs", "Removing PJs"));
        }

        [TestMethod]
        public void GetDressedConstructorTest_WhenInstantiated_CorrectDressOptionsAreAvailable()
        {
            for (int i = 0; i < _GetDressed.AvailableDressOptions.Count; i++)
            {
                var availableDressOption = _GetDressed.AvailableDressOptions[_GetDressed.AvailableDressOptions.Keys.ElementAt(i)];
                var availableDressOptionTest = AvailableDressOptionsTest[AvailableDressOptionsTest.Keys.ElementAt(i)];
                Assert.AreEqual(availableDressOption.ToString(), availableDressOptionTest.ToString());
            }
        }

        [TestMethod]
        public void GetDressedConstructorTest_WhenInstantiated_EmptyDressCommandsIsReady()
        {
            GetDressed getDressed = new GetDressed();
            Assert.IsTrue(getDressed.IncomingDressCommands.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "A null command array was allowed but should not be allowed as input.")]
        public void AddDressCommandsTest_WhenNullInput_ExpectException()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = null;
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "A 0 count command array was allowed but should not be allowed as input.")]
        public void AddDressCommandsTest_WhenZeroCountInput_ExpectException()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenOneInputAdded_ExpectOneDressCommandInList()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add((int)ClothingTypes.Headwear);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Count == 1);
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenOneInputAddedCold_ExpectCorrectDressCommandInList()
        {
            int commandTest = (int)ClothingTypes.Shirt;
            var testTemperature = Temperatures.Cold;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandTest);
            getDressed.ExecuteDressCommandsByTemperature(testTemperature, dressCommands);
            var expectCommand = AvailableDressOptionsTest[commandTest];
            var actualCommand = getDressed.IncomingDressCommands[commandTest];
            Assert.AreEqual(expectCommand.ColdResponse, actualCommand.ToString());
        }

        [TestMethod]
        public void AddDressCommandsTest_WheOneInputAddedHot_ExpectCorrectDressCommandInList()
        {
            int commandTest = (int)ClothingTypes.Shirt;
            var testTemperature = Temperatures.Hot;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandTest);
            getDressed.ExecuteDressCommandsByTemperature(testTemperature, dressCommands);
            var expectCommand = AvailableDressOptionsTest[commandTest];
            var actualCommand = getDressed.IncomingDressCommands[commandTest];
            Assert.AreEqual(expectCommand.HotResponse, actualCommand.ToString());
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenNullInputAdded_ExpectFailCommand()
        {
            int commandTest = (int)ClothingTypes.Socks;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenBadCommandAdded_ExpectLastAddedCommandToBeFail()
        {
            int commandBadTest = (int)ClothingTypes.Socks;
            int commandGoodTest = (int)ClothingTypes.Headwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandBadTest);
            dressCommands.Add(commandGoodTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfFirstNotTakeOffPajamas_ExpectFail()
        {
            int commandFootwearTest = (int)ClothingTypes.Footwear;
            int commandPajamasOffTest = (int)ClothingTypes.RemovePajamas;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandFootwearTest);
            dressCommands.Add(commandPajamasOffTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfFirstTakeOffPajamas_ExpectSuccess()
        {
            int commandFootwearTest = (int)ClothingTypes.Footwear;
            int commandPajamasOffTest = (int)ClothingTypes.RemovePajamas;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandPajamasOffTest);
            dressCommands.Add(commandFootwearTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.First().Key == commandPajamasOffTest);
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfDuplicateCommandAdded_ExpectFail()
        {
            int commandFootwearTest = (int)ClothingTypes.Footwear;
            int commandFootwearTest2 = (int)ClothingTypes.Footwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandFootwearTest);
            dressCommands.Add(commandFootwearTest2);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfHotAndSock_ExpectFail()
        {
            int commandSockTest = (int)ClothingTypes.Socks;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandSockTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfHotAndJacket_ExpectFail()
        {
            int commandJacketTest = (int)ClothingTypes.Jacket;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandJacketTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfColdAndSock_ExpectTrue()
        {
            int commandSockTest = (int)ClothingTypes.Socks;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(commandSockTest);
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "socks");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfBootsBeforeSocks_ExpectFail()
        {
            int commandBootsTest = (int)ClothingTypes.Footwear;
            int commandSocksTest = (int)ClothingTypes.Socks;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int>{ commandBootsTest, commandSocksTest});
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfBootsBeforePants_ExpectFail()
        {
            int commandBootsTest = (int)ClothingTypes.Footwear;
            int commandPantsTest = (int)ClothingTypes.Pants;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandBootsTest, commandPantsTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfSocksAndPantsBeforeFootwear_Cold_ExpectSuccess()
        {
            int commandFootwearTest = (int)ClothingTypes.Footwear;
            int commandSocksTest = (int)ClothingTypes.Socks;
            int commandPantsTest = (int)ClothingTypes.Pants;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandPantsTest, commandSocksTest, commandFootwearTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "boots");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfPantsBeforeFootwear_Hot_ExpectSuccess()
        {
            int commandFootwearTest = (int)ClothingTypes.Footwear;
            int commandPantsTest = (int)ClothingTypes.Pants;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandPantsTest, commandFootwearTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "sandals");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfJacketBeforeShirt_Cold_ExpectFail()
        {
            int commandJacketTest = (int)ClothingTypes.Jacket;
            int commandShirtTest = (int)ClothingTypes.Shirt;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandJacketTest, commandShirtTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfShirtBeforeJacket_Cold_ExpectSuccess()
        {
            int commandJacketTest = (int)ClothingTypes.Jacket;
            int commandShirtTest = (int)ClothingTypes.Shirt;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandShirtTest, commandJacketTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "jacket");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfHeadwearBeforeShirt_Hot_ExpectFail()
        {
            int commandShirtTest = (int)ClothingTypes.Shirt;
            int commandHeadwearTest = (int)ClothingTypes.Headwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandHeadwearTest, commandShirtTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfHeadwearBeforeShirt_Cold_ExpectFail()
        {
            int commandShirtTest = (int)ClothingTypes.Shirt;
            int commandHeadwearTest = (int)ClothingTypes.Headwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandHeadwearTest, commandShirtTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "fail");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfShirtBeforeHeadwear_Hot_ExpectSuccess()
        {
            int commandShirtTest = (int)ClothingTypes.Shirt;
            int commandHeadwearTest = (int)ClothingTypes.Headwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandShirtTest, commandHeadwearTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "sun visor");
        }

        [TestMethod]
        public void AddDressCommandsTest_WhenAddDressCommands_IfShirtBeforeHeadwear_Cold_ExpectSuccess()
        {
            int commandShirtTest = (int)ClothingTypes.Shirt;
            int commandHeadwearTest = (int)ClothingTypes.Headwear;
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.AddRange(new List<int> { commandShirtTest, commandHeadwearTest });
            getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(getDressed.IncomingDressCommands.Last().Value == "hat");
        }

        [TestMethod()]
        public void GetDressed_Hot_ExpectSuccess()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(4);
            dressCommands.Add(2);
            dressCommands.Add(1);
            dressCommands.Add(7);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(results == "Removing PJs, shorts, t-shirt, sun visor, sandals, leaving house");
        }

        [TestMethod()]
        public void GetDressed_Cold_ExpectSuccess()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(3);
            dressCommands.Add(4);
            dressCommands.Add(2);
            dressCommands.Add(5);
            dressCommands.Add(1);
            dressCommands.Add(7);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(results == "Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house");
        }

        [TestMethod()]
        public void GetDressed_Hot_ExpectFail1()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(6);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(results == "Removing PJs, shorts, fail");
        }

        [TestMethod()]
        public void GetDressed_Hot_ExpectFail2()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(3);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(results == "Removing PJs, shorts, fail");
        }

        [TestMethod()]
        public void GetDressed_Cold_ExpectFail1()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(3);
            dressCommands.Add(4);
            dressCommands.Add(2);
            dressCommands.Add(5);
            dressCommands.Add(7);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Cold, dressCommands);
            Assert.IsTrue(results == "Removing PJs, pants, socks, shirt, hat, jacket, fail");
        }

        [TestMethod()]
        public void GetDressed_Cold_ExpectFail2()
        {
            GetDressed getDressed = new GetDressed();
            List<int> dressCommands = new List<int>();
            dressCommands.Add(8);
            dressCommands.Add(6);
            dressCommands.Add(3);
            string results = getDressed.ExecuteDressCommandsByTemperature(Temperatures.Hot, dressCommands);
            Assert.IsTrue(results == "Removing PJs, shorts, fail");
        }
    }
}
