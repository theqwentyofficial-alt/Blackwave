using LibreHardwareMonitor.Hardware;
using DataProvider.Counters;
using DataProvider.Hardware;
namespace DataProvider.Managers.HardwareManagers
{
    public static class CpuManager
    {
        public static string? GetName(IHardware cpu)
        {
            return cpu.Name;
        }
        /// <summary>
        /// Gets the full CPU information.Automatically fills full CPU class variables.
        /// </summary>
        /// <param name="computer">The computer to retrieve CPU information from. CPU monitoring must be enabled.</param>
        /// <returns>The CPU information, or <see langword="null"/> if CPU monitoring is disabled or no CPU is found.</returns>
        public static CPU? GetFullInfo(Computer computer)
        {
            if (computer.IsCpuEnabled == false)
            {
                return null;
            }

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    string? name = hardware.Name;
                    int? totalCores;
                    int? totalThreads;
                    string? socket;
                    ushort? maxSpeed;

                    SMBios smBios = computer.SMBios;
                    ProcessorInformation[] pInfos = smBios.Processors;

                    totalCores = pInfos[0].CoreCount;
                    totalThreads = pInfos[0].ThreadCount;
                    socket = pInfos[0].Socket.ToString();
                    maxSpeed = pInfos[0].MaxSpeed;

                    return new CPU(name, totalCores, totalThreads, socket, maxSpeed);
                }
            }
            return new CPU();
        }
        public static async Task<int> GetActualMhz(int delay)
        {
            CpuCounters.cpuFreqCounter.NextValue();
            await Task.Delay(delay);
            return (int)CpuCounters.cpuFreqCounter.NextValue();
        }
    }
}
