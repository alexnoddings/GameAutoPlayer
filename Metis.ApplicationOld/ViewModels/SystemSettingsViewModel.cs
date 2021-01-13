using Metis.Application.Models;

namespace Metis.Application.ViewModels
{
    internal sealed class SystemSettingsViewModel : BaseViewModel
    {
        private SystemSettings _systemSettings;
        public SystemSettings SystemSettings
        {
            get => _systemSettings;
            set => SetProperty(ref _systemSettings, value);
        }
    }
}
