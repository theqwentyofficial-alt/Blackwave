using LibreHardwareMonitor.Hardware;
using Microsoft.Win32;
using pcdatacollector.PCData;
using System.Collections;
using System.Diagnostics;

namespace pcdatacollector
{
    public static class Helper
    {
       
        public static PerformanceCounter cpuLoadCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        public static PerformanceCounter cpuFreqCounter = new PerformanceCounter("Processor Information", "Actual Frequency", "_Total");
        public static PerformanceCounter memUseCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
        public static PerformanceCounter memAvailableMBCounter = new PerformanceCounter("Memory", "Available MBytes");
        /// <summary>
        /// Retrieves a value from the Windows registry.
        /// </summary>
        /// <typeparam name="ValueType">The type of the registry value.</typeparam>
        /// <param name="regPath">The path of the registry key.</param>
        /// <param name="valueName">The name of the value to retrieve.</param>
        /// <returns>The registry value, or <c>null</c> if the value is not found.</returns>
        public static ValueType? GetReg<ValueType>(string regPath, string valueName)
        {
            ValueType? data = (ValueType?)Registry.GetValue(regPath, valueName, null);
            return data;
        }
        /// <summary>
        /// Sets a registry value.
        /// </summary>
        /// <typeparam name="ValueType">The type of the value to store.</typeparam>
        /// <param name="regPath">The path of the registry key.</param>
        /// <param name="valueName">The name of the registry value.</param>
        /// <param name="value">The data to store in the registry value.</param>
        public static void SetReg<ValueType>(string? regPath, string? valueName, ValueType? value)
        {
            Registry.SetValue(regPath, valueName, value);
        }
        /// <summary>
        /// Lists the directories in the current working directory.
        /// </summary>
        /// <returns>A list of directory paths in the current working directory.</returns>
        public static List<string> GetDirectories()
        {
            List<string> list = new List<string>();
            string[] dirs = Directory.GetDirectories(Directory.GetCurrentDirectory());

            for (int i = 0; i < dirs.Length; i++)
            {
                list.Add(dirs[i]);
            }
            Logger.Log(Logger.LVL.LOW, "Got directories in current path.");
            return list;
        }

        public static List<string> GetDirectories(string path)
        {
            List<string> list = new List<string>();
            string[] dirs = Directory.GetDirectories(path);

            for (int i = 0; i < dirs.Length; i++)
            {
                list.Add(dirs[i]);
            }
            Logger.Log(Logger.LVL.LOW, $"Got directories in {path} path.");
            return list;
        }
        public static void SetCurrDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
            Logger.Log(Logger.LVL.LOW, $"Set current path to {path}");
        }
        public static string GetCurrDirectory()
        {
            Logger.Log(Logger.LVL.LOW, $"Got current directory.");
            return Directory.GetCurrentDirectory();
        }
        /// <summary>
        /// Gets the current processor load as a percentage.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the processor load as a
        /// percentage.</returns>
        public static async Task<float> GetProcessorLoad(int delay)
        {
           
            cpuLoadCounter.NextValue();
            await Task.Delay(delay);
            return cpuLoadCounter.NextValue();
        }
        /// <summary>
        /// Measures the current CPU clock speed in megahertz.
        /// </summary>
        /// <param name="delay">The delay between CPU counter readings, in milliseconds.</param>
        /// <returns>A task whose result is the measured CPU clock speed in megahertz.</returns>
        public static async Task<int> GetActualCPUMhz(int delay)
        {
            cpuFreqCounter.NextValue();
            await Task.Delay(delay);
            return (int)cpuFreqCounter.NextValue();
        }
        public static string? GetCPUName()
        {
            return GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "ProcessorNameString");
        }
        public static string? GetCPUVendor()
        {
            return GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "VendorIdentifier");
        }
        public static int? GetCPUMhz()
        {
            return GetReg<int>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", "~MHz");
        }
        public static string? GetComputerName()
        {
            return GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "USERDOMAIN");
        }
        public static string? GetUsername()
        {
            return GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "USERNAME");
        }
        public static string? GetHomeDrive()
        {
            return GetReg<string>("HKEY_CURRENT_USER\\Volatile Environment", "HOMEDRIVE");
        }
        public static string? GetBoardName()
        {
            return GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BaseBoardProduct");
        }
        public static string? GetBoardVendor()
        {
            return GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BaseBoardManufacturer");
        }
        public static string? GetBIOSReleaseDate()
        {
            return GetReg<string>("HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\System\\BIOS", "BIOSReleaseDate");
        }
        public static string? GetBoardVersion(Computer computer)
        {
           if(computer.IsMotherboardEnabled == false)
            {
                return null;
            }
            SMBios smBios = computer.SMBios;
            BaseBoardInformation info = smBios.Board;
            return info.Version;
        }
        /// <summary>
        /// Gets the full CPU information.Automatically fills full CPU class variables.
        /// </summary>
        /// <param name="computer">The computer to retrieve CPU information from. CPU monitoring must be enabled.</param>
        /// <returns>The CPU information, or <see langword="null"/> if CPU monitoring is disabled or no CPU is found.</returns>
        public static CPU? GetFullCpuInfo(Computer computer)
        {
            if(computer.IsCpuEnabled == false)
            {
                return null;
            }

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.Cpu)
                {
                    string? name = hardware.Name;
                    string? vendor = GetCPUVendor();
                    int? globalMhz = GetCPUMhz();
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

                    return new CPU(name, vendor, globalMhz,totalCores,totalThreads,socket,maxSpeed);
                }
            }
            return null;
        }
        /// <summary>
        /// Gets motherboard information and populates a <see cref="MOTHERBOARD"/> instance.
        /// </summary>
        /// <param name="computer">The computer to query for motherboard information.</param>
        /// <returns>A populated <see cref="MOTHERBOARD"/> instance, or <see langword="null"/> if motherboard information is
        /// disabled.</returns>
        public static MOTHERBOARD? GetFullBoardInfo(Computer computer)
        {
            if (computer.IsMotherboardEnabled == false)
            {
                return null;
            }
            SMBios smBios = computer.SMBios;
            BaseBoardInformation info = smBios.Board;
            string? ver = info.Version;
                    
            return new MOTHERBOARD(GetBoardName(),GetBoardVendor(),GetBIOSReleaseDate(),ver);       
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="computer"></param>
        /// <returns></returns>
        public static RAM? GetFullRamInfo(Computer computer)
        {
            if (computer.IsMemoryEnabled == false)
            {
                return null;
            }
            
            float? memoryUsed = 0;
            float? memoryAvailable = 0;
            float? memory = 0;
            float? memoryUsedVirtual = 0;
            float? memoryAvailableVirtual = 0;
            float? memoryVirtual = 0;
            
            foreach (var hardw in computer.Hardware)
            {
                hardw.Update();
                foreach (var sensor in hardw.Sensors)
                {      
                     if (sensor.Name == "Memory Used" && hardw.Name == "Total Memory")
                     {
                         memoryUsed = sensor.Value;
                     }
                     if (sensor.Name == "Memory Available" && hardw.Name == "Total Memory")
                     {
                        memoryAvailable = sensor.Value;
                     }
                    if (sensor.Name == "Memory" && hardw.Name == "Total Memory")
                    {
                        memory = sensor.Value;
                    }
                    if (sensor.Name == "Memory Used" && hardw.Name == "Virtual Memory")
                    {
                        memoryUsedVirtual = sensor.Value;
                    }
                    if (sensor.Name == "Memory Available" && hardw.Name == "Virtual Memory")
                    {
                        memoryAvailableVirtual = sensor.Value;
                    }
                    if (sensor.Name == "Memory" && hardw.Name == "Virtual Memory")
                    {
                        memoryVirtual = sensor.Value;
                    }

                }
            }
            return new RAM(memoryUsed,memoryAvailable,memory,memoryUsedVirtual,memoryAvailableVirtual,memoryVirtual);
        }
       
        public static List<IHardware> GetIHardwares(Computer computer, HardwareType hwType)
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
        public static List<IHardware> GetIHardwares(Computer computer, HardwareType hwType, string hwName)
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
        public static List<ISensor> GetISensors(Computer computer,HardwareType hwType,string sensorName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
        public static List<ISensor> GetISensors(Computer computer, HardwareType hwType, string sensorName, string hardwName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName && i.Name == hardwName)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
        public static List<ISensor> GetISensors(Computer computer, HardwareType hwType, SensorType sType, string sensorName)
        {
            List<ISensor> sensor = new List<ISensor>();
            foreach (var i in computer.Hardware)
            {
                if (i.HardwareType == hwType)
                {
                    foreach (var k in i.Sensors)
                    {
                        if (k.Name == sensorName && sType == k.SensorType)
                        {
                            sensor.Add(k);
                            break;
                        }
                    }
                }
            }
            return sensor;
        }
        public static async Task<float?> GetRamLoad(int delay) {
            memUseCounter?.NextValue();
            await Task.Delay(delay);
            return memUseCounter?.NextValue();
        }
        public static async Task<float?> GetRamAvailableMB(int delay)
        {
            memAvailableMBCounter?.NextValue();
            await Task.Delay(delay);
            return memAvailableMBCounter?.NextValue();
        }
    }
}
