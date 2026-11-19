using LibreHardwareMonitor.Hardware;
namespace DataProvider.Hardware
{
    public class GPU
    {

        public string? Name { get; set; }

        public float? MemTotal { get; set; }
        public float? MemFree { get; set; }
        public float? MemUsed { get; set; }
        public float? HotSpot { get; set; }
        public GPU(Computer computer,string? name, float? memTotal, float? memFree, float? memUsed, float? hotSpot)
        {
            Name = name;
            MemTotal = memTotal;
            MemFree = memFree;
            MemUsed = memUsed;
            HotSpot = hotSpot;
        }
        public GPU()
        {
        }
    }
}
