using DiskInfoToolkit.Interop.Structures;
using LibreHardwareMonitor.Hardware;
using pcdatacollector.PCData.Counters;
using pcdatacollector.PCData.Managers.PCManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace pcdatacollector.PCData.Managers.HardwareManagers
{
    public static class GpuManager
    {
        public static IHardware GetGpu(Computer computer)
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer);
            IHardware gpu = null!;
            foreach (var i in hw)
            {
                if (i.HardwareType == HardwareType.GpuNvidia)
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
            return gpu;
        }
        public static List<float> GetSensorsValues(GpuCounters GpuCounters)
        {
            List<float> arr = new List<float>();
            for (int i = 0; i < GpuCounters.core.Count; i++)
            {
                arr.Add((float)GpuCounters.core[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.memCtrl.Count; i++)
            {
                arr.Add((float)GpuCounters.memCtrl[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.gpuEngine.Count; i++)
            {
                arr.Add((float)GpuCounters.gpuEngine[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.gpuBus.Count; i++)
            {
                arr.Add((float)GpuCounters.gpuBus[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3Dsecurity.Count; i++)
            {
                arr.Add((float)GpuCounters.D3Dsecurity[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3DvidEncode.Count; i++)
            {
                arr.Add((float)GpuCounters.D3DvidEncode[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3D3d.Count; i++)
            {
                arr.Add((float)GpuCounters.D3D3d[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3Doverlay.Count; i++)
            {
                arr.Add((float)GpuCounters.D3Doverlay[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3DvidDecode.Count; i++)
            {
                arr.Add((float)GpuCounters.D3DvidDecode[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3Dcopy.Count; i++)
            {
                arr.Add((float)GpuCounters.D3Dcopy[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3DoptFlowAccel.Count; i++)
            {
                arr.Add((float)GpuCounters.D3DoptFlowAccel[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3Dvr.Count; i++)
            {
                arr.Add((float)GpuCounters.D3Dvr[i]?.Value!);
            }
            for (int i = 0; i < GpuCounters.D3Dsecurity_1.Count; i++)
            {
                arr.Add((float)GpuCounters.D3Dsecurity_1[i]?.Value!);
            }
            return arr;
        }
    }
}
