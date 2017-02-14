using System;
using System.Collections.Generic;
using System.Linq;

namespace DressForSuccess
{
    public class DressRules : IDressRules
    {
        IGetDressed _GetDressed;
        public DressRules(IGetDressed getDressed)
        {
            _GetDressed = getDressed;
        }
        public bool IsAfterRequiredDressCommand(int dressCommandPrerequisite, Dictionary<int, string> dressCommands)
        {
            try
            {
                int commandPreReqIndex = dressCommands.Keys.ToList().IndexOf(dressCommandPrerequisite);
                if (commandPreReqIndex == -1) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsFirstInCommandList(Dictionary<int, string> dressCommands)
        {
            try
            {
                if (dressCommands.Count == 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool AllOtherValidDressCommandsInList(Dictionary<int, string> dressCommands, Temperatures temperature, List<int> dressCommandExceptions = null)
        {
            try
            {
                dressCommandExceptions = dressCommandExceptions ?? new List<int>();
                IEnumerable<int> validDressCommandsByTemperature;
                if (temperature == Temperatures.Hot)
                {
                    validDressCommandsByTemperature = _GetDressed.AvailableDressOptions.Where(hr => hr.Value.HotResponse != null).Select(r => r.Key).Except(dressCommandExceptions);
                }
                else
                {
                    validDressCommandsByTemperature = _GetDressed.AvailableDressOptions.Where(hr => hr.Value.ColdResponse != null).Select(r => r.Key).Except(dressCommandExceptions);
                }
                return Enumerable.SequenceEqual(dressCommands.Keys.OrderBy(t => t), validDressCommandsByTemperature.OrderBy(t => t));
            }
            catch
            {
                return false;
            }
        }

        public List<Tuple<Rules, Temperatures, int>> PajamaRules
        {
            get
            {
                return new List<Tuple<Rules, Temperatures, int>>
                {
                    new Tuple<Rules, Temperatures, int>(Rules.IsFirstInCommandList, Temperatures.Any, -1)
                };
            }
        }

        public List<Tuple<Rules, Temperatures, int>> FootwearPreReqRules
        {
            get
            {
                return new List<Tuple<Rules, Temperatures, int>>
                {
                    new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Cold, (int)ClothingTypes.Socks),
                    new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Hot, (int)ClothingTypes.Pants),
                    new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Cold, (int)ClothingTypes.Pants)
                };
            }
        }

        public List<Tuple<Rules, Temperatures, int>> HeadwearPreReqRules
        {
            get
            {
                return new List<Tuple<Rules, Temperatures, int>>
                {
                    new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Any, (int)ClothingTypes.Shirt)
                };
            }
        }

        public List<Tuple<Rules, Temperatures, int>> JacketPreReqRules
        {
            get
            {
                return new List<Tuple<Rules, Temperatures, int>>
                {
                    new Tuple<Rules, Temperatures, int>(Rules.IsAfterRequiredDressCommand, Temperatures.Cold, (int)ClothingTypes.Shirt)
                };
            }
        }

        public List<Tuple<Rules, Temperatures, int>> LeaveHousePreReqRules
        {
            get
            {
                return new List<Tuple<Rules, Temperatures, int>>
                {
                    new Tuple<Rules, Temperatures, int>(Rules.AllOtherValidDressCommandsInList, Temperatures.Any, (int)ClothingTypes.NA)
                };
            }
        }
    }
}
