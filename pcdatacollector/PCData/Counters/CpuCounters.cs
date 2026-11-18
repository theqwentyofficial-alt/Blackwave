using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace pcdatacollector.PCData.Counters
{
    public static class CpuCounters
    {
        public static PerformanceCounter cpuLoadCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter cpuFreqCounter = new PerformanceCounter("Processor Information", "Actual Frequency", "_Total");
    }
}
