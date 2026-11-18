using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Managers;
namespace pcdatacollector.PCData.Managers.PCManagers
{
    public static class HardwareManager
    {
        public static List<IHardware> GetHardwares(Computer computer, HardwareType hwType)
        {
            List<IHardware> hw = new List<IHardware>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    hw.Add(i);
                    break;
                }
            }
            return hw;
        }
        public static List<IHardware> GetHardwares(Computer computer, HardwareType hwType, string hwName)
        {
            List<IHardware> hw = new List<IHardware>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType && i.Name == hwName)
                {
                    hw.Add(i);
                    break;
                }
            }
            return hw;
        }
    }
}
