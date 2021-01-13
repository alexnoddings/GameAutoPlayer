using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;
using Metis.Core;
using Metis.Core.Console;

namespace Metis.Application.Models.Console
{
    internal class ConsoleFactory : Observable, IConsoleFactory
    {
        private const int MaxMessages = 50;

        public ObservableCollection<ConsoleMessage> Messages { get; } = new ObservableCollection<ConsoleMessage>();

        public void AddMessage(ConsoleMessage message)
        {
            // The UI can only be updated on the UI thread. Updating an ObservableCollection invokes an update event, triggering a UI update.
            // Therefore, updating an ObservableCollection on a non-UI thread fails.
            // As such, if this is invoked outside of the UI thread, this method should be invoked on the UI thread by the dispatcher instead.
            bool hasUiAccess = System.Windows.Application.Current.Dispatcher.CheckAccess();
            if (hasUiAccess)
            {
                while (Messages.Count >= MaxMessages)
                    Messages.RemoveAt(0);
                Messages.Add(message);
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => AddMessage(message)));
            }
        }

        /// <inheritdoc />
        public IConsole GetCurrentModuleConsole()
        {
            // Skip 1 frame as the top frame is always this method
            MethodBase? callingMethod = new StackFrame(1, false).GetMethod();
            string callingModuleFullName = callingMethod?.Module.Name ?? "unknown";
            string name = callingModuleFullName
                .Replace(".dll", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Split(".").Last();
            return new ModuleConsole(name, this);
        }

        private class ModuleConsole : IConsole
        {
            private readonly ConsoleFactory _consoleFactory;
            private readonly string _moduleName;

            public ModuleConsole(string moduleName, ConsoleFactory consoleFactory)
            {
                _moduleName = moduleName;
                _consoleFactory = consoleFactory;
            }

            /// <inheritdoc />
            public void WriteLine(string text) =>
                _consoleFactory.AddMessage(new ConsoleMessage(_moduleName, text, DateTime.Now));
        }
    }
}
