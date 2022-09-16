using Avalonia.Controls;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using WATestBrowser;

namespace AATestBrowser
{
    public partial class WindowLogger : Window
    {
        public WindowLogger()
        {
            InitializeComponent();
            DataContext = new WindowLoggerViewModel();
            this.Closing += (s, e) =>
            {
                ServerLogging.LoggerOutput = null;
            };
        }
    }

    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; }

        public ViewModelBase()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* handle activation */
                Disposable
                    .Create(() => { /* handle deactivation */ })
                    .DisposeWith(disposables);
            });
        }
    }

    class WindowLoggerViewModel : ViewModelBase, ILoggerOutput
    {
       

        public WindowLoggerViewModel()
        {
            Items = new ObservableCollection<string>();
            ServerLogging.LoggerOutput = this;
        }

        public ObservableCollection<string>? Items { get; }

        public void Log(string logRecord)
        {
            Items?.Insert(0, logRecord);
        }
    }
}
