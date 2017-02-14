using System;
using System.Collections.Generic;

namespace DressForSuccess
{
    public interface IDressOption
    {
        string Description { get; set; }
        string HotResponse { get; set; }
        string ColdResponse { get; set; }
        List<Tuple<Rules, Temperatures, int>> Rules { get; set; }
    }
}
