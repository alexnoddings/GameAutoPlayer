namespace Metis.Application.Models
{
    using Metis.Core.Settings;

    /// <summary>
    ///     Settings used by the application.
    /// </summary>
    internal class ApplicationSettings : SavedSettings
    {
        /// <inheritdoc />
        protected override string SettingsName { get; } = "Application";

        private double _windowX = 100;
        [SavedSetting]
        public double WindowX
        {
            get => _windowX;
            set => Set(ref _windowX, value);
        }

        private double _windowY = 100;
        [SavedSetting]
        public double WindowY
        {
            get => _windowY;
            set => Set(ref _windowY, value);
        }

        private double _windowWidth = 100;
        [SavedSetting]
        public double WindowWidth
        {
            get => _windowWidth;
            set => Set(ref _windowWidth, value);
        }

        private double _windowHeight = 100;
        [SavedSetting]
        public double WindowHeight
        {
            get => _windowHeight;
            set => Set(ref _windowHeight, value);
        }

        private bool _isAlwaysOnTop = false;
        [SavedSetting]
        public bool IsAlwaysOnTop
        {
            get => _isAlwaysOnTop;
            set => Set(ref _isAlwaysOnTop, value);
        }
    }
}
