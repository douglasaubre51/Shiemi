using Shiemi.ViewModels;

namespace Shiemi;

public partial class TitleBarWindow : Window
{
    public TitleBarWindow(BaseVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}