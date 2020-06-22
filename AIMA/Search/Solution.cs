using System.Collections.Generic;

namespace AIMA
{
    public class Solution
    {
        public List<string> Actions { get; set; }
        public int Cost { get; set; }
        public int NodesExpanded { get; set; }
        public int SearchDepth { get; set; }
        public int FringeSize { get; set; }
        public int MaximumSearchDepth { get; set; }
        public int RunningTime { get; set; }
        public long MaximumRamUsage { get; set; }
    }
}