using Metis.Application.ViewModels;

namespace Metis.Application.Views
{
    public sealed partial class SystemSettingsView
    {
        public SystemSettingsView()
        {
            InitializeComponent();
            DataContext = new SystemSettingsViewModel();
        }
    }
}
