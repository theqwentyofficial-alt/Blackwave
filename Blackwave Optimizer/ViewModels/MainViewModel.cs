using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
namespace Blackwave_Optimizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public bool isStats;
    [ObservableProperty]
    public bool isOpt;
    

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
    [RelayCommand]
    private void SwitchToStats()
    {
        IsStats = true;
        IsOpt = false;
    }
    [RelayCommand]
    private void SwitchToOpt()
    {
        IsOpt = true;
        IsStats = false;
    }
}
