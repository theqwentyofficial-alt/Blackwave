using LibreHardwareMonitor.Hardware;
using DataProvider.Managers.HardwareManagers;
using DataProvider.Managers.PCManagers;
namespace DataProvider.Counters
{
    public class GpuCounters
    {
        public List<ISensor> core;
        public List<ISensor> memCtrl;
        public List<ISensor> gpuEngine;
        public List<ISensor> gpuBus;
        public List<ISensor> D3D3d;
        public List<ISensor> D3Doverlay;
        public List<ISensor> D3DvidDecode;
        public List<ISensor> D3Dcopy;
        public List<ISensor> D3Dsecurity;
        public List<ISensor> D3DvidEncode;
        public List<ISensor> D3DoptFlowAccel;
        public List<ISensor> D3Dvr;
        public List<ISensor> D3Dsecurity_1;
        public GpuCounters(Computer computer)
        {
            IHardware hw = GpuManager.GetGpu(computer);
            core = SensorManager.GetSensors(computer, hw, SensorType.Load, "GPU Core");
            memCtrl = SensorManager.GetSensors(computer, hw, SensorType.Load, "GPU Memory Controller");
            gpuEngine = SensorManager.GetSensors(computer, hw, SensorType.Load, "GPU Video Engine");
            gpuBus = SensorManager.GetSensors(computer, hw, SensorType.Load, "GPU Bus");
            D3D3d = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D 3D");
            D3Doverlay = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Overlay");
            D3DvidDecode = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Video Decode");
            D3Dcopy = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Copy");
            D3Dsecurity = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Security");
            D3DvidEncode = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Video Encode");
            D3DoptFlowAccel = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Optical Flow Accelerator 0");
            D3Dvr = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D VR");
            D3Dsecurity_1 = SensorManager.GetSensors(computer, hw, SensorType.Load, "D3D Security_1");
        }

    }
}
