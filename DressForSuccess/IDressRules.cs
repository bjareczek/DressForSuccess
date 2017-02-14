using System;
using System.Collections.Generic;

namespace DressForSuccess
{
    public interface IDressRules
    {
        List<Tuple<Rules, Temperatures, int>> FootwearPreReqRules { get; }
        List<Tuple<Rules, Temperatures, int>> HeadwearPreReqRules { get; }
        List<Tuple<Rules, Temperatures, int>> JacketPreReqRules { get; }
        List<Tuple<Rules, Temperatures, int>> LeaveHousePreReqRules { get; }
        List<Tuple<Rules, Temperatures, int>> PajamaRules { get; }

        bool AllOtherValidDressCommandsInList(Dictionary<int, string> dressCommands, Temperatures temperature, List<int> dressCommandExceptions = null);
        bool IsAfterRequiredDressCommand(int dressCommandPrerequisite, Dictionary<int, string> dressCommands);
        bool IsFirstInCommandList(Dictionary<int, string> dressCommands);
    }
}