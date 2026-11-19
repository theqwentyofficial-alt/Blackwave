using LibreHardwareMonitor.Hardware;
using DataProvider.Managers.HardwareManagers;
namespace DataProvider.Hardware
{
    public class RAM
    {
        public float? TotalUsed { get; set; }
        public float? TotalAvailable { get; set; }
public float? Load { get; set; }
        public float? TotalUsedVirtual { get; set; }
        public float? VirtualAvailable { get; set; }
        public float? LoadVirtual { get; set; }
        public RAM(float? totalUsed, float? totalAvailable, float? totalMem, float? totalUsedVirtual, float? virtualAvailable, float? totalMemVirtual)
        {
            TotalUsed = totalUsed;
            TotalAvailable = totalAvailable;
            Load = totalMem;
            TotalUsedVirtual = totalUsedVirtual;
            VirtualAvailable = virtualAvailable;
            LoadVirtual = totalMemVirtual;
        }
        public RAM(Computer computer)
        {
            RAM ram = RamManager.GetFullInfo(computer);
            this.TotalUsed = ram.TotalUsed;
            TotalAvailable = ram.TotalAvailable;
            Load = ram.Load;
            TotalUsedVirtual = ram.TotalUsedVirtual;
            VirtualAvailable = ram.VirtualAvailable;
            LoadVirtual = ram.LoadVirtual;
        }
    }
}
