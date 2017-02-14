using System.Collections.Generic;

namespace DressForSuccess
{
    public interface IGetDressed
    {
        Dictionary<int, IDressOption> AvailableDressOptions { get; set; }
        Dictionary<int, string> IncomingDressCommands { get; set; }

        string ExecuteDressCommandsByTemperature(Temperatures temperature, List<int> dressCommands);
    }
}