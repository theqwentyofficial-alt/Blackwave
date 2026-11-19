using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Blackwave_Optimizer.ViewModels;
using Blackwave_Optimizer.Views;
using LibreHardwareMonitor.Hardware;
using DataProvider;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataProvider.Counters;
using DataProvider.Managers.HardwareManagers;
using DataProvider.Managers.PCManagers;
namespace Blackwave_Optimizer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        const int refreshRate = 1000;
        Computer computer = new Computer()
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true
        };
    computer.Open();
        MainViewModel? vm = new MainViewModel();
        IHardware gpu = GpuManager.GetGpu(computer);
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
            List<ISensor> sensor = SensorManager.GetSensors(computer, hw[0], "Memory Used", "Total Memory");

            while (true)
            {
                hw[0].Update();
                vm.RamUsed = (((vm.TotalRam - vm.RamAvailable2))).ToString() + "MB";
                await Task.Delay(refreshRate);
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
                int cpuMhz = await CpuManager.GetActualMhz(refreshRate);
                Dispatcher.UIThread.Post(() =>
                {
                    vm?.CurrentMhz = cpuMhz;
                });
            }
        });
         Task.Run(async () =>
          {
              
        List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Cpu);
  List<ISensor> sensor = SensorManager.GetSensors(computer, hw[0], "CPU Total");
  while(true)
  {
      hw[0].Update();
      vm?.LoadInGraduses = (int)(sensor[0]?.Value * 3.6f);
      vm?.Load = ((int)sensor[0]?.Value).ToString() + "%";
      await Task.Delay(refreshRate);
  }
          });
        Task.Run(async () =>
        {
            while (true)
            {
                float? available = await RamManager.GetAvailableMB(refreshRate);
                vm.RamAvailable = (available.ToString() + "MB");
                vm.RamAvailable2 = available;
            }
        });

        Task.Run(async () =>
        {
            while (true)
            {
             float? load = await RamManager.GetLoad(refreshRate);
                vm.RamLoad = ((int)load).ToString() + "%";
                vm.RamLoadInGraduses = load * 3.6f;
            }
        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = SensorManager.GetSensors(computer, hw[0], "Memory Used", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
               vm.VirtRamUsed = ((int)sensor[0].Value * 1024).ToString() + "MB";
                await Task.Delay(refreshRate);
            }

        });
        Task.Run(async () =>
        {
            List<IHardware> hw = HardwareManager.GetHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = SensorManager.GetSensors(computer, hw[0], "Memory Available", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
                vm.VirtRamAvailable = ((int)sensor[0].Value * 1024).ToString() + "MB";
                await Task.Delay(refreshRate);
            }

        });
        Task.Run(() =>
        {
            while(true)
            {
                gpu.Update();
                Thread.Sleep(refreshRate);
            }
        });
        Task.Run(async () =>
        {
            List<ISensor> sensor = SensorManager.GetSensors(computer, gpu, "GPU Core");
            vm.GpuName = gpu.Name;
            while (true)
            {
                vm.GpuTemp = (sensor[0].Value).ToString() + "℃";
                await Task.Delay(refreshRate);
            }

        });
        Task.Run(() =>
        {
            List<ISensor> sensor = SensorManager.GetSensors(computer, gpu, "GPU Memory Total");
            vm.GpuMemTotal = sensor[0].Value;
        });
  
               Task.Run(async () =>
               { 
                   List<ISensor> sensor = SensorManager.GetSensors(computer, gpu, "GPU Memory Used");
                   while (true)
                   {
                       vm.GpuMemUsed = (sensor[0].Value).ToString() + "MB";
                       await Task.Delay(refreshRate);
                   }

               });
        Task.Run(async () =>
        {
            List<ISensor> sensor = SensorManager.GetSensors(computer, gpu, "GPU Memory Free");
            while (true)
            {
                vm.GpuMemFree = (sensor[0].Value).ToString() + "MB";
                await Task.Delay(refreshRate);
            }
        });
       
        Task.Run(async () =>
        { 
            GpuCounters gpuCounters = new GpuCounters(computer);
            List<float> arr = new List<float>();
            float percent = 0;
            while (true)
            {
                arr.Clear();
                gpu.Update();
                
                await Task.Run(() =>
                {
                    arr = GpuManager.GetSensorsValues(gpuCounters);
                });
                if (arr.Max() > 100)
                {
                    percent = 100;
                } else
                {
                    percent = (float)arr.Max();
                }
                                
                vm.GpuLoad = ((int)percent).ToString() + "%";
                vm.GpuLoadInGraduses = (float)(percent * 3.6f);
                await Task.Delay(refreshRate);
            }

        });
    }
}