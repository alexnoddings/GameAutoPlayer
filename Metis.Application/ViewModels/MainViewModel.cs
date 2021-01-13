using Metis.Application.Models;
using Metis.Core.Console;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Settings;

namespace Metis.Application.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ISystemSettings _systemSettings;
        [AutoInject]
        public ISystemSettings SystemSettings
        {
            get => _systemSettings;
            set => Set(ref _systemSettings, value);
        }

        private ApplicationSettings _applicationSettings;
        [AutoInject]
        public ApplicationSettings ApplicationSettings
        {
            get => _applicationSettings;
            set => Set(ref _applicationSettings, value);
        }

        private IOcrHandler _ocrHandler;
        [AutoInject]
        public IOcrHandler OcrHandler
        {
            get => _ocrHandler;
            set => Set(ref _ocrHandler, value);
        }

        private IConsoleFactory _consoleFactory;
        [AutoInject]
        public IConsoleFactory ConsoleFactory
        {
            get => _consoleFactory;
            set => Set(ref _consoleFactory, value);
        }

        private readonly IConsole _console;

        public MainViewModel()
        {
            _console = ConsoleFactory.GetCurrentModuleConsole();
            _console.WriteLine("Initialising system...");
        }
    }
}
