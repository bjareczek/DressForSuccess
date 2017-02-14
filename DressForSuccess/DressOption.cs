using System;
using System.Collections.Generic;

namespace DressForSuccess
{
    public class DressOption : IDressOption
    {
        public DressOption(string description, string hotResponse, string coldResponse, List<Tuple<Rules, Temperatures, int>> rules = null)
        {
            Description = description;
            HotResponse = hotResponse;
            ColdResponse = coldResponse;
            Rules = rules;
        }

        public string Description { get; set; }
        public string HotResponse { get; set; }
        public string ColdResponse { get; set; }
        public List<Tuple<Rules, Temperatures, int>> Rules { get; set; }

        public override string ToString()
        {
            return Description + ", " + HotResponse + ", " + ColdResponse;
        }
    }
}
