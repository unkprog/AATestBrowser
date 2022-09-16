using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using Xilium.CefGlue.Common.Shared;
using Xilium.CefGlue.Common;
using Xilium.CefGlue;
using Avalonia.ReactiveUI;

namespace AATestBrowser
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToTrace()
                .With(new Win32PlatformOptions
                {
                    UseWindowsUIComposition = false
                })
                      .AfterSetup(_ => CefRuntimeLoader.Initialize(new CefSettings()
                      {
#if WINDOWLESS
                          WindowlessRenderingEnabled = true
#else
                          WindowlessRenderingEnabled = false
#endif
                      }));
    }
}
