using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Blackwave_Optimizer.ViewModels;
using Blackwave_Optimizer.Views;
using LibreHardwareMonitor.Hardware;
using pcdatacollector;
using pcdatacollector.PCData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using pcdatacollector.PCData.Managers.PCManagers;
using pcdatacollector.PCData.Managers.HardwareManagers;
using pcdatacollector.PCData.Managers.OSManagers;
namespace Blackwave_Optimizer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Computer computer = new Computer()
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true
        };
        
        computer.Open();
        MainViewModel? vm = new MainViewModel();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(),
            };
            if (desktop.MainWindow.DataContext is MainViewModel)
            {
                vm = desktop.MainWindow.DataContext as MainViewModel;
            }
        }

    
        base.OnFrameworkInitializationCompleted();
        PC pc = new PC(computer);
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.Memory, "Memory Used", "Total Memory");

            while (true)
            {
                hw[0].Update();
                vm.RamUsed = (((vm.TotalRam - vm.RamAvailable2))).ToString() + "MB";
                await Task.Delay(1000);
            }

        });
        Task.Run(async () =>
        {

            Dispatcher.UIThread.Post(() =>
            {
                vm?.ProcessorName = pc?.cpu?.Name;
                vm?.TotalCores = pc?.cpu?.TotalCores;
                vm?.TotalThreads = pc?.cpu?.TotalThreads;
            });

            while (true)
            {
                int cpuMhz = await CpuManager.GetActualMhz(1000);
                Dispatcher.UIThread.Post(() =>
                {
                    vm?.CurrentMhz = cpuMhz;
                });
            }
        });
         Task.Run(async () =>
          {
              
        List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Cpu);
  List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.Cpu, "CPU Total");
  while(true)
  {
      hw[0].Update();
      vm?.LoadInGraduses = (int)(sensor[0]?.Value * 3.6f);
      vm?.Load = ((int)sensor[0]?.Value).ToString() + "%";
      await Task.Delay(1000);
  }
          });
        Task.Run(async () =>
        {
            while (true)
            {
                float? available = await RamManager.GetAvailableMB(1000);
                vm.RamAvailable = (available.ToString() + "MB");
                vm.RamAvailable2 = available;
            }
        });
        Task.Run(async () =>
        {
            while (true)
            {
             float? load = await RamManager.GetLoad(1000);
                vm.RamLoad = ((int)load).ToString() + "%";
                vm.RamLoadInGraduses = load * 3.6f;
            }
        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.Memory, "Memory Used", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
               vm.VirtRamUsed = ((int)sensor[0].Value * 1024).ToString();
                await Task.Delay(1000);
            }

        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.Memory, "Memory Available", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
                vm.VirtRamAvailable = ((int)sensor[0].Value * 1024).ToString();
                await Task.Delay(1000);
            }

        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.GpuNvidia);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, "GPU Core");
            vm.GpuName = hw[0].Name;
            while (true)
            {
                hw[0].Update();
                vm.GpuTemp = (sensor[0].Value).ToString() + "℃";
                await Task.Delay(1000);
            }

        });
        Task.Run(() =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.GpuNvidia);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, "GPU Memory Total");
            vm.GpuMemTotal = sensor[0].Value;
        });
  
               Task.Run(async () =>
               {
                   List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.GpuNvidia);
                   List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, "GPU Memory Used");
                   while (true)
                   {
                       hw[0].Update();
                       vm.GpuMemUsed = (sensor[0].Value).ToString() + "MB";
                       await Task.Delay(1000);
                   }

               });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.GpuNvidia);
            List<ISensor> sensor = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, "GPU Memory Free");
            while (true)
            {
                hw[0].Update();
                vm.GpuMemFree = (sensor[0].Value).ToString() + "MB";
                await Task.Delay(1000);
            }

        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.GpuNvidia);
            List<ISensor> core = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "GPU Core");
            List<ISensor> memCtrl = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "GPU Memory Controller");
            List<ISensor> gpuEngine = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "GPU Video Engine");
            List<ISensor> gpuBus = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "GPU Bus");
            List<ISensor> D3D3d = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D 3D");
            List<ISensor> D3Doverlay = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Overlay");
            List<ISensor> D3DvidDecode = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Video Decode");
            List<ISensor> D3Dcopy = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Copy");
            List<ISensor> D3Dsecurity = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Security");
            List<ISensor> D3DvidEncode = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Video Encode");
            List<ISensor> D3DoptFlowAccel = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Optical Flow Accelerator 0");
            List<ISensor> D3Dvr = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D VR");
            List<ISensor> D3Dsecurity_1 = SensorManager.GetSensors(computer, HardwareType.GpuNvidia, SensorType.Load, "D3D Security_1");
            List<float> arr = new List<float>();
            float percent = 0;
            while (true)
            {
                arr.Clear();
                hw[0].Update();
                arr.Add((float)core[0]?.Value);
                arr.Add((float)memCtrl[0]?.Value);
                arr.Add((float)gpuEngine[0]?.Value);
                arr.Add((float)gpuBus[0]?.Value);
                arr.Add((float)D3D3d[0]?.Value);
                arr.Add((float)D3Doverlay[0]?.Value);
                arr.Add((float)D3DvidDecode[0]?.Value);
                arr.Add((float)D3Dcopy[0]?.Value);
                arr.Add((float)D3Dsecurity[0]?.Value);
                arr.Add((float)D3DvidEncode[0]?.Value);
                arr.Add((float)D3DoptFlowAccel[0]?.Value);
                arr.Add((float)D3Dvr[0]?.Value);
                arr.Add((float)D3Dsecurity_1[0]?.Value);
                if (arr.Max() > 100)
                {
                    percent = 100;
                } else
                {
                    percent = (float)arr.Max();
                }
                                
                vm.GpuLoad = ((int)percent).ToString() + "%";
                vm.GpuLoadInGraduses = (float)(percent * 3.6f);
                await Task.Delay(1000);
            }

        });
    }
}