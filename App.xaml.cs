using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace RogueFeature
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static void MyHandler(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string message =  e.Exception.Message + "\n\n" + e.Exception.StackTrace;
            if(!DebugLogging.DebugLogger.LogIt("$E " + e.Exception.Message + "\n\n" + e.Exception.StackTrace))
            {
                MessageBox.Show("$E " + e.Exception.Message + "\n\n" + e.Exception.StackTrace);
            }
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.Dispatcher.UnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(MyHandler);
        }
    }
}
