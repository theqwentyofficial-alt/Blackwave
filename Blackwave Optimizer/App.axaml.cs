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
using System.Threading.Tasks;

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
            List<IHardware> hw = Helper.GetIHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = Helper.GetISensors(computer, HardwareType.Memory, "Memory Used", "Total Memory");

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
                int cpuMhz = await Helper.GetActualCPUMhz(1000);
                Dispatcher.UIThread.Post(() =>
                {
                    vm?.CurrentMhz = cpuMhz;
                });
            }
        });
         Task.Run(async () =>
          {
              
        List<IHardware> hw = Helper.GetIHardwares(computer, HardwareType.Cpu);
  List<ISensor> sensor = Helper.GetISensors(computer, HardwareType.Cpu, "CPU Total");
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
                float? available = await Helper.GetRamAvailableMB(1000);
                vm.RamAvailable = (available.ToString() + "MB");
                vm.RamAvailable2 = available;
            }
        });
        Task.Run(async () =>
        {
            while (true)
            {
             float? load = await Helper.GetRamLoad(1000);
                vm.RamLoad = ((int)load).ToString() + "%";
                vm.RamLoadInGraduses = load * 3.6f;
            }
        });
        Task.Run(async () =>
        {
            List<IHardware> hw = Helper.GetIHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = Helper.GetISensors(computer, HardwareType.Memory, "Memory Used", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
               vm.VirtRamUsed = ((int)sensor[0].Value * 1024).ToString();
                await Task.Delay(1000);
            }

        });
        Task.Run(async () =>
        {
            List<IHardware> hw = Helper.GetIHardwares(computer, HardwareType.Memory);
            List<ISensor> sensor = Helper.GetISensors(computer, HardwareType.Memory, "Memory Available", "Virtual Memory");

            while (true)
            {
                hw[0].Update();
                vm.VirtRamAvailable = ((int)sensor[0].Value * 1024).ToString();
                await Task.Delay(1000);
            }

        });
    }
}