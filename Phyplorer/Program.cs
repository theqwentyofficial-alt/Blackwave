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
computer.Close();

