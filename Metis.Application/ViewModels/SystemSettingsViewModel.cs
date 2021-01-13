using Metis.Application.Models;
using Metis.Core.Settings;

namespace Metis.Application.ViewModels
{
    internal class SystemSettingsViewModel : BaseViewModel
    {
        private SystemSettings _systemSettings;
        [AutoInject(registeredType: typeof(ISystemSettings))]
        public SystemSettings SystemSettings
        {
            get => _systemSettings;
            set => Set(ref _systemSettings, value);
        }
    }
}
