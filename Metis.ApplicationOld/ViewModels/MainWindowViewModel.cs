using Metis.Application.Models;

namespace Metis.Application.ViewModels
{
    internal sealed class MainWindowViewModel : BaseViewModel
    {
        private SystemSettings _systemSettings;
        public SystemSettings SystemSettings
        {
            get => _systemSettings;
            set => SetProperty(ref _systemSettings, value);
        }

        public MainWindowViewModel(SystemSettings systemSettings)
        {
            SystemSettings = systemSettings;
        }
    }
}
