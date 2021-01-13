using Metis.Application.Models.Console;
using Metis.Core.Console;

namespace Metis.Application.ViewModels
{
    internal class ConsoleViewModel : BaseViewModel
    {
        private const int MaxMessages = 100;

        private ConsoleFactory _consoleFactory;

        /// <summary>
        ///     Gets the console instance.
        /// </summary>
        [AutoInject(registeredType: typeof(IConsoleFactory))]
        public ConsoleFactory ConsoleFactory
        {
            get => _consoleFactory;
            private set => Set(ref _consoleFactory, value);
        }
    }
}
