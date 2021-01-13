using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Metis.Core.Console;
using Metis.Core.Display;
using Metis.Core.Extensions;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Module;
using Metis.Core.Settings;
using Metis.ImageProcessing.Utilities;
using Bitmap = System.Drawing.Bitmap;

namespace Metis.Modules.TypingGame
{
    public class TypingGameModule: IModule
    {
        private readonly ISystemSettings systemSettings;
        private readonly IDisplayHelper displayHelper;
        private readonly IOcrHandler ocrHandler;
        private readonly IConsole _console;

        private static readonly TimeSpan Interval = TimeSpan.FromMilliseconds(150);
        private static readonly Color SelectedAreaColorUpper = Color.FromArgb(75, 75, 75);
        private static readonly Color SelectedAreaColorLower = Color.FromArgb(55, 55, 55);

        private static readonly ImageOcrPadder OcrPadder = new ImageOcrPadder("./assets/FrameOpening.png", "./assets/FrameClosing.png");
        private const string OcrPadLeftText = "Frame Opening";
        private const string OcrPadRightText = "Frame Closing";

        public TypingGameModule(ISystemSettings systemSettings, IDisplayHelper displayHelper, IConsoleFactory consoleFactory)
        {
            _console = (consoleFactory ?? throw new ArgumentNullException(nameof(consoleFactory))).GetCurrentModuleConsole();

            this.systemSettings = systemSettings ?? throw new ArgumentNullException(nameof(systemSettings));
            this.displayHelper = displayHelper ?? throw new ArgumentNullException(nameof(displayHelper));
            ocrHandler = this.systemSettings.OcrHandlerInstance;
        }

        /// <inheritdoc />
        public string Name { get; } = "Typing Game";

        private bool CanExecute()
        {
            using Bitmap window = systemSettings.WindowCaptureHandlerInstance.GetWindow();
            double scale = displayHelper.DisplayWidth / 2560;
            var submitWordArea = new Rectangle((int) (1114 * scale), (int) (1335 * scale), (int) (331 * scale), (int) (49 * scale));
            using Bitmap submitWordImg = GetArea(window, submitWordArea);
            string text = ocrHandler.GetText(submitWordImg);
            return text.Contains("space", StringComparison.OrdinalIgnoreCase) ||
                   text.Contains("submit", StringComparison.OrdinalIgnoreCase) ||
                   text.Contains("word", StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _console.WriteLine("Starting module");
            var scale = displayHelper.DisplayWidth / 2560;
            var topRowArea = new Rectangle(814, 1045, 932, 56).Scale(scale);
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (CanExecute())
                    {
                        using Bitmap window = systemSettings.WindowCaptureHandlerInstance.GetWindow();
                        using Bitmap topRowImg = GetArea(window, topRowArea);
                        var startX = GetAreaStartingX(topRowImg);
                        if (startX == -1)
                        {
                            await Task.Delay(Interval);
                            continue;
                        }
                        var endX = GetAreaEndingX(topRowImg, startX);
                        if (endX == -1)
                        {
                            await Task.Delay(Interval);
                            continue;
                        }

                        using var wordImg = GetArea(topRowImg, new Rectangle(startX, 0, endX - startX, topRowImg.Height));
                        using var paddedImg = OcrPadder.Pad(wordImg);
                        var text = ocrHandler.GetText(paddedImg)
                            .Replace(OcrPadLeftText, string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace(OcrPadRightText, string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase)
                            .Replace("\n", string.Empty, StringComparison.OrdinalIgnoreCase);
                        _console.WriteLine("Trying word " + text);
                        foreach (char c in text)
                        {
                            await systemSettings.InputHandlerInstance.SendKeyAsync(c);
                            await Task.Delay(15);
                        }

                        var invc = System.IO.Path.GetInvalidFileNameChars();
                        var sb = new StringBuilder();
                        foreach (char c in text)
                            sb.Append(invc.Contains(c) ? '_' : c);

                        Task sendKeyTask = systemSettings.InputHandlerInstance.SendKeyAsync(' ');
                        await sendKeyTask;
                    }

                    await Task.Delay(Interval);
                }
            }
            catch (TaskCanceledException)
            {
            }
            _console.WriteLine("Ending module");
        }

        private static int GetAreaStartingX(Bitmap bitmap)
        {
            for (var x = 0; x < bitmap.Width; x++)
                if (bitmap.GetPixel(x, 3).IsInRange(SelectedAreaColorLower, SelectedAreaColorUpper))
                    return x;
            return -1;
        }

        private static int GetAreaEndingX(Bitmap bitmap, int startX)
        {
            for (var x = startX; x < bitmap.Width; x++)
                if (!bitmap.GetPixel(x, 3).IsInRange(SelectedAreaColorLower, SelectedAreaColorUpper))
                    return x;
            return -1;
        }

        private static Bitmap GetArea(Bitmap bitmap, Rectangle area)
        {
            var newBitmap = new Bitmap(area.Width, area.Height);
            for (int x = area.X; x < area.X + area.Width; x++)
            for (int y = area.Y; y < area.Y + area.Height; y++)
            {
                var pixel = bitmap.GetPixel(x, y);
                newBitmap.SetPixel(x - area.X, y - area.Y, pixel);
            }
            return newBitmap;
        }
    }
}
