using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using Metis.Application.ViewModels;
using Metis.Core.Interaction;
using Metis.Core.Settings;

namespace Metis.Application
{
    internal partial class App
    {
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            DebugHandleException(e);
        }

        // Only handle when running in a debug build
        [Conditional("DEBUG")]
        private void DebugHandleException(DispatcherUnhandledExceptionEventArgs dispatcherException)
        {
            string[] crashFiles = Directory.GetFiles("./", ".crash-*.txt");
            int crashNumber = crashFiles
                                  .Select(f => f
                                      .Replace("./.crash-", string.Empty)
                                      .Replace(".txt", string.Empty))
                                  .Select(int.Parse)
                                  .Union(new[] { 0 })
                                  .Max() + 1;

            // Save a screenshot of the window
            var vml = Resources["Locator"] as ViewModelLocator;
            ISystemSettings sysSettings = vml!.Main.SystemSettings;
            IWindowCaptureHandler windowCaptureHandler = sysSettings.WindowCaptureHandlerInstance;
            string screenshotName = $"./crash-{crashNumber}.png";
            bool isRunning = sysSettings.TargetProcess != null;
            if (isRunning)
                windowCaptureHandler.GetWindow().Save(screenshotName);

            // Log the exception
            string logName = $"./.crash-{crashNumber}.txt";
            string content = $"{(isRunning ? "Process was running" : "Process was not running")}\n-----------------------------\n{dispatcherException.Exception}";
            File.WriteAllText(logName, content);

            // Mark as handled to avoid process exiting
            dispatcherException.Handled = true;
        }
    }
}
