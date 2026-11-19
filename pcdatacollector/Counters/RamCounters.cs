using System.Diagnostics;
namespace DataProvider.Counters
{
    public static class RamCounters
    {
        public static PerformanceCounter ramUseCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public static PerformanceCounter ramAvailableMBCounter = new PerformanceCounter("Memory", "Available MBytes");
    }
}
