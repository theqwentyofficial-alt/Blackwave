using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Counters;
using pcdatacollector.PCData.Hardware;
using pcdatacollector.PCData.Managers.OSManagers;
using System;
using System.Collections.Generic;
using System.Text;
namespace pcdatacollector.PCData.Managers.HardwareManagers
{
    public static class CpuManager
    {
        public static string? GetName()
        {
            return RegistryManager.GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString");
        }
        public static string? GetVendor()
        {
            return RegistryManager.GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "VendorIdentifier");
        }
        public static int? GetMhz()
        {
            return RegistryManager.GetReg<int>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "~MHz");
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
                    string? vendor = CpuManager.GetVendor();
                    int? globalMhz = CpuManager.GetMhz();
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

                    return new CPU(name, vendor, globalMhz, totalCores, totalThreads, socket, maxSpeed);
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
