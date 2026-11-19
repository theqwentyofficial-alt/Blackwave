using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibreHardwareMonitor.Hardware;
using System;
namespace Blackwave_Optimizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public float? ramUsed2;
    [ObservableProperty]
    public float? gpuLoadInGraduses;
    [ObservableProperty]
    public string? gpuLoad;
    [ObservableProperty]
    public string? gpuMemFree;
    [ObservableProperty]
    public string? gpuMemUsed;
    [ObservableProperty]
    public float? gpuMemTotal;
    [ObservableProperty]
    public string? gpuName;
    [ObservableProperty]
    public string? gpuTemp;
    [ObservableProperty]
    public long? totalRam = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / (1024 * 1024);
    [ObservableProperty]
    public float? ramLoadInGraduses;
    [ObservableProperty]
    public string? ramLoad;
    [ObservableProperty]
    public float? ramAvailable2;
    [ObservableProperty]
    public string? ramAvailable;
    [ObservableProperty]
    public string? ramUsed;
    [ObservableProperty]
    public string? virtRamAvailable;
    [ObservableProperty]
    public string? virtRamUsed;
    [ObservableProperty]
    public float? loadInGraduses;
    [ObservableProperty]
    public string? processorName;
    [ObservableProperty]
    public int? totalCores;
    [ObservableProperty]
    public int? totalThreads;
    [ObservableProperty]
    public int? currentMhz;
    [ObservableProperty]
    public string? load;
    [ObservableProperty]
    public bool isStats = true;


    [RelayCommand]
    private void Exit()
    {
        Environment.Exit(0);
    }
    [RelayCommand]
    private void Hide()
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.WindowState = Avalonia.Controls.WindowState.Minimized;
        }
    }
}
