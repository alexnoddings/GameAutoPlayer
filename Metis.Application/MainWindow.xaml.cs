using System;
using Metis.Application.ViewModels;

namespace Metis.Application
{
    internal partial class MainWindow
    {
        public MainWindow()
        {
            /*
                Work Priorities
                    - Console!
                    - Modules capable of providing views
                    - Have handlers check for readiness
                    - Preview area
                    - Testing framework with Moq
                    - Typing derby process entire line at a time?

                    - Have crosshairs disappear when a desktop sc is taken, or when focus to the target process is lost
                    - Add a tornado image-reading module
                    - More OCR work; typing derby/trivia
                    - Module status (text)
                    - OCR the score in PP
                    - Exit module early if error is detected (leave game UI at bottom could be marker)
                    - Small window preview pane
                    - Auto mode (detects module based on "E Play {game}" UI
             */
            InitializeComponent();
        }

        private MainViewModel ViewModel => (DataContext as MainViewModel)!;

        private void MainWindow_OnDeactivated(object? sender, EventArgs e)
        {
            if (ViewModel.ApplicationSettings.IsAlwaysOnTop)
            {
                Topmost = true;
                Activate();
            }
            else
            {
                Topmost = false;
            }
        }
    }
}
