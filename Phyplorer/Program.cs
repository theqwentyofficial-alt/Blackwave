using LibreHardwareMonitor.Hardware;
using pcdatacollector;
using pcdatacollector.PCData;
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

foreach (var i in computer.Hardware)
{
    foreach (var k in i.Sensors)
    {
        if (k.SensorType == SensorType.Load && i.HardwareType == HardwareType.GpuNvidia)
        {
          
            Console.WriteLine(k.Name);
        }
    }
}


computer.Close();

