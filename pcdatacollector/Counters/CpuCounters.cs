using System.Diagnostics;
namespace DataProvider.Counters
{
    public static class CpuCounters
    {
        public static PerformanceCounter cpuLoadCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter cpuFreqCounter = new PerformanceCounter("Processor Information", "Actual Frequency", "_Total");
    }
}
