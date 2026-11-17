using LibreHardwareMonitor.Hardware;
using System.Net.NetworkInformation;
using pcdatacollector.PCData;
using pcdatacollector;
Computer computer = new Computer()
{
    IsCpuEnabled = true,
    IsGpuEnabled = true,
    IsMemoryEnabled = true
};
computer.Open();
PC pc = new PC(computer);
foreach (var i in computer.Hardware)
{
    if (i.HardwareType == HardwareType.Memory)
    {
        foreach (var k in i.Sensors)
        {
            Console.WriteLine(k.Name);
        }
    }
}
Console.WriteLine(GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / (1024*1024));

computer.Close();

