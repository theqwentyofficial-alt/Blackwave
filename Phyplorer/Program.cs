using LibreHardwareMonitor.Hardware;
using pcdatacollector;
using pcdatacollector.PCData;
using pcdatacollector.PCData.Managers.PCManagers;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;
Computer computer = new Computer()
{
    IsCpuEnabled = true,
    IsGpuEnabled = true,
    IsMemoryEnabled = true
};
computer.Open();
PC pc = new PC(computer);

List<IHardware> hw = HardwareManager.GetHardwares(computer);
IHardware gpu = null!;
foreach (var i in hw)
{
 if(i.HardwareType == HardwareType.GpuNvidia)
    {
        gpu = i;
    }
 else if (i.HardwareType == HardwareType.GpuIntel)
    {
        gpu = i;
    }
    else if (i.HardwareType == HardwareType.GpuAmd)
    {
        gpu = i;
    }
}
foreach (var i in computer.Hardware)
{
    foreach (var k in i.Sensors)
    {
        Console.WriteLine($"{i.HardwareType}, {k.Name}");

    }
}


computer.Close();

