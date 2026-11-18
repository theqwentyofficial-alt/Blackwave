using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace pcdatacollector.PCData.Counters
{
    public static class RamCounters
    {
        public static PerformanceCounter ramUseCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public static PerformanceCounter ramAvailableMBCounter = new PerformanceCounter("Memory", "Available MBytes");
    }
}
