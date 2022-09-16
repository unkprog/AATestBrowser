using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Xilium.CefGlue.Avalonia;

namespace AATestBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            browserApp = this.FindControl<AvaloniaCefBrowser>("browserApp");
            
            browserApp.Address = string.Concat( "file:///", System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location), "/spinner.html");
        }


        private void clickStart(object? sender, RoutedEventArgs args)
        {
            Window w = new WindowLogger();
            w.Show();
            WATestBrowserHelper.StartAsync(Array.Empty<string>());
            browserApp.Address = string.Concat("https://localhost:5001/weatherforecast");
        }
    }
}
