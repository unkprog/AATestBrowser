using Avalonia.Controls;
using Xilium.CefGlue.Avalonia;

namespace AATestBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            browserApp = this.FindControl<AvaloniaCefBrowser>("browserApp");
            browserApp.Address = "file:///C:/temp/spinner.html";
        }
    }
}
