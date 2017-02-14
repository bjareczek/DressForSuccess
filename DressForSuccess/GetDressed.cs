using System;
using System.Collections.Generic;
using System.Linq;

namespace DressForSuccess
{
    public class GetDressed : IGetDressed
    {
        public GetDressed()
        {
            DressingRules = new DressRules(this);
            IncomingDressCommands = new Dictionary<int, string>();
            GetAvailableDressOptions();
        }

        private IDressRules DressingRules { get; set; }
        public Dictionary<int, IDressOption> AvailableDressOptions {get; set;}

        public Dictionary<int, string> IncomingDressCommands{ get; set; }

        public string ExecuteDressCommandsByTemperature(Temperatures temperature, List<int> dressCommands)
        {
            if (dressCommands == null || dressCommands.Count < 1) throw new ArgumentException("Dress commands input required.");
            bool exitLoop = false;
            foreach (var dressCommand in dressCommands)
            {
                if (exitLoop) break;
                try
                {
                    var dressOption = AvailableDressOptions[dressCommand];
                    string dressOutputResponse = temperature == Temperatures.Hot ? dressOption.HotResponse : dressOption.ColdResponse;
                    var dressOptionsToExcludeFromRules = temperature == Temperatures.Hot ? 
                        //TODO:  I would extract these exclusions into the individual dress rules for more abstraction and flexibility, but time did not allow.
                        new List<int> {(int)ClothingTypes.Socks, (int)ClothingTypes.Jacket, (int)ClothingTypes.Leave} : new List<int> {(int)ClothingTypes.Leave };
                    if (dressOutputResponse == null) throw new ArgumentNullException("Command for given temperature is not valid");
                    if(dressOption.Rules != null) ValidateDressCommandRules(temperature, dressOption, dressOptionsToExcludeFromRules);
                    IncomingDressCommands.Add(dressCommand, dressOutputResponse);
                }
                catch (Exception)
                {
                    IncomingDressCommands.Add(-1, "fail");
                    exitLoop = true;
                    break;
                }
            }
            return string.Join(", ", IncomingDressCommands.Values);
        }

        private void ValidateDressCommandRules(Temperatures temperature, IDressOption dressOption, List<int> dressOptionsToExclude)
        {
            foreach (var rule in dressOption.Rules.Where(r => r.Item2 == Temperatures.Any || r.Item2 == temperature))
            {
                switch (rule.Item1)
                {
                    case Rules.IsFirstInCommandList:
                        if (!DressingRules.IsFirstInCommandList(IncomingDressCommands)) throw new Exception("Rule broken");
                        break;
                    case Rules.IsAfterRequiredDressCommand:
                        if (!DressingRules.IsAfterRequiredDressCommand(rule.Item3, IncomingDressCommands)) throw new Exception("Rule broken");
                        break;
                    case Rules.AllOtherValidDressCommandsInList:
                        if (!DressingRules.AllOtherValidDressCommandsInList(IncomingDressCommands, temperature, dressOptionsToExclude)) throw new Exception("Rule broken");
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetAvailableDressOptions()
        {
            AvailableDressOptions = new Dictionary<int, IDressOption>();
            AvailableDressOptions.Add(1, new DressOption("Put on footwear", "sandals", "boots", DressingRules.FootwearPreReqRules));
            AvailableDressOptions.Add(2, new DressOption("Put on headwear", "sun visor", "hat", DressingRules.HeadwearPreReqRules));
            AvailableDressOptions.Add(3, new DressOption("Put on socks", null, "socks"));
            AvailableDressOptions.Add(4, new DressOption("Put on shirt", "t-shirt", "shirt"));
            AvailableDressOptions.Add(5, new DressOption("Put on jacket", null, "jacket", DressingRules.JacketPreReqRules));
            AvailableDressOptions.Add(6, new DressOption("Put on pants", "shorts", "pants"));
            AvailableDressOptions.Add(7, new DressOption("Leave house", "leaving house", "leaving house", DressingRules.LeaveHousePreReqRules));
            AvailableDressOptions.Add(8, new DressOption("Take off pajamas", "Removing PJs", "Removing PJs", DressingRules.PajamaRules));
        }
    }
}
