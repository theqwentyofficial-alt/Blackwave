using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Counters;
using pcdatacollector.PCData.Hardware;
using System;
using System.Collections.Generic;
using System.Text;

namespace pcdatacollector.PCData.Managers.HardwareManagers
{
    public static class RamManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="computer"></param>
        /// <returns></returns>
        public static RAM GetFullInfo(Computer computer)
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
            return new RAM(memoryUsed, memoryAvailable, memory, memoryUsedVirtual, memoryAvailableVirtual, memoryVirtual);
        }
        public static async Task<float?> GetLoad(int delay)
        {
            RamCounters.ramUseCounter?.NextValue();
            await Task.Delay(delay);
            return RamCounters.ramUseCounter?.NextValue();
        }
        public static async Task<float?> GetAvailableMB(int delay)
        {
            RamCounters.ramAvailableMBCounter?.NextValue();
            await Task.Delay(delay);
            return RamCounters.ramAvailableMBCounter?.NextValue();
        }

    }
}
