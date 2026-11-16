using Avalonia.Controls;
using Avalonia.Input;
namespace Blackwave_Optimizer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if(e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
         
            BeginMoveDrag(e);
        
        }
    }
}