using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Metis.Core.Console;
using Metis.Core.Display;
using Metis.Core.Module;
using Metis.Core.Settings;
using Metis.ImageProcessing.Utilities;

namespace Metis.Modules.PianoGame
{
    public class PianoGameModule: IModule
    {
        private readonly IConsole _console;

        public ISystemSettings SystemSettings { get; }
        public IDisplayHelper DisplayHelper { get; }

        private const int KeyAreaDistanceBase = 50;
        private const int KeyAreaSizeBase = 8;
        private static readonly TimeSpan WaitInterval = TimeSpan.FromMilliseconds(60);
        private static readonly TimeSpan RunTime = TimeSpan.FromSeconds(202);

        private Rectangle Key1Rect { get; }
        private IOutlineArea Key1Area { get; }

        private Rectangle Key2Rect { get; }
        private IOutlineArea Key2Area { get; }

        private Rectangle Key3Rect { get; }
        private IOutlineArea Key3Area { get; }

        private Rectangle Key4Rect { get; }
        private IOutlineArea Key4Area { get; }

        private IOutlineArea UpperLeftArea { get; }
        private IOutlineArea UpperRightArea { get; }
        private IOutlineArea LowerLeftArea { get; }
        private IOutlineArea LowerRightArea { get; }

        public PianoGameModule(ISystemSettings systemSettings, IDisplayHelper displayHelper, IConsoleFactory consoleFactory)
        {
            _console = consoleFactory.GetCurrentModuleConsole();
            SystemSettings = systemSettings;
            DisplayHelper = displayHelper;

            double centerX = DisplayHelper.DisplayCenterX;
            double centerY = DisplayHelper.DisplayCenterY;
            double scale = (DisplayHelper.DisplayWidth / 2560);
            double keyAreaDistance = KeyAreaDistanceBase * scale;
            double keyAreaSize = KeyAreaSizeBase * scale;

            Key1Rect = new Rectangle((int)(centerX - (keyAreaDistance / 2.0) - keyAreaSize - keyAreaDistance - keyAreaSize), (int) (centerY - (keyAreaSize / 2.0)), (int)keyAreaSize, (int)keyAreaSize);
            Key2Rect = new Rectangle((int)(centerX - (keyAreaDistance / 2.0) - keyAreaSize), (int)(centerY - (keyAreaSize / 2.0)), (int)keyAreaSize, (int)keyAreaSize);
            Key3Rect = new Rectangle((int)(centerX + (keyAreaDistance / 2.0)), (int)(centerY - (keyAreaSize / 2.0)), (int)keyAreaSize, (int)keyAreaSize);
            Key4Rect = new Rectangle((int)(centerX + (keyAreaDistance / 2.0) + keyAreaSize + keyAreaDistance), (int)(centerY - (keyAreaSize / 2.0)), (int)keyAreaSize, (int)keyAreaSize);

            UpperLeftArea = DisplayHelper.CreateOutlineArea();
            UpperLeftArea.OuterX = centerX - (309 * scale);
            UpperLeftArea.OuterY = centerY - (447 * scale);
            UpperLeftArea.OuterWidth = 12;
            UpperLeftArea.OuterHeight = 12;
            UpperLeftArea.ThicknessLeft = 3;
            UpperLeftArea.ThicknessTop = 3;
            UpperLeftArea.ThicknessRight = 0;
            UpperLeftArea.ThicknessBottom = 0;

            UpperRightArea = DisplayHelper.CreateOutlineArea();
            UpperRightArea.OuterX = centerX + (509 * scale);
            UpperRightArea.OuterY = centerY - (385 * scale);
            UpperRightArea.OuterWidth = 12;
            UpperRightArea.OuterHeight = 12;
            UpperRightArea.ThicknessLeft = 0;
            UpperRightArea.ThicknessTop = 3;
            UpperRightArea.ThicknessRight = 3;
            UpperRightArea.ThicknessBottom = 0;

            LowerLeftArea = DisplayHelper.CreateOutlineArea();
            LowerLeftArea.OuterX = centerX - (322 * scale);
            LowerLeftArea.OuterY = centerY + (237 * scale);
            LowerLeftArea.OuterWidth = 12;
            LowerLeftArea.OuterHeight = 12;
            LowerLeftArea.ThicknessLeft = 3;
            LowerLeftArea.ThicknessTop = 0;
            LowerLeftArea.ThicknessRight = 0;
            LowerLeftArea.ThicknessBottom = 3;

            LowerRightArea = DisplayHelper.CreateOutlineArea();
            LowerRightArea.OuterX = centerX + (517 * scale);
            LowerRightArea.OuterY = centerY + (212 * scale);
            LowerRightArea.OuterWidth = 12;
            LowerRightArea.OuterHeight = 12;
            LowerRightArea.ThicknessLeft = 0;
            LowerRightArea.ThicknessTop = 0;
            LowerRightArea.ThicknessRight = 3;
            LowerRightArea.ThicknessBottom = 3;

            Key1Area = DisplayHelper.CreateOutlineArea();
            Key1Area.OuterX = Key1Rect.X;
            Key1Area.OuterY = Key1Rect.Y;
            Key1Area.OuterWidth = keyAreaSize;
            Key1Area.OuterHeight = keyAreaSize;

            Key2Area = DisplayHelper.CreateOutlineArea();
            Key2Area.OuterX = Key2Rect.X;
            Key2Area.OuterY = Key2Rect.Y;
            Key2Area.OuterWidth = keyAreaSize;
            Key2Area.OuterHeight = keyAreaSize;

            Key3Area = DisplayHelper.CreateOutlineArea();
            Key3Area.OuterX = Key3Rect.X;
            Key3Area.OuterY = Key3Rect.Y;
            Key3Area.OuterWidth = keyAreaSize;
            Key3Area.OuterHeight = keyAreaSize;

            Key4Area = DisplayHelper.CreateOutlineArea();
            Key4Area.OuterX = Key4Rect.X;
            Key4Area.OuterY = Key4Rect.Y;
            Key4Area.OuterWidth = keyAreaSize;
            Key4Area.OuterHeight = keyAreaSize;
        }

        /// <inheritdoc />
        public string Name { get; } = "Piano Game";

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _console.WriteLine("Starting module");
            SetVisibility(true);
            try
            {
                const int redMin = 245;
                DateTime nextStop = DateTime.MinValue;
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (nextStop < DateTime.Now)
                    {
                        _console.WriteLine("Ending run");
                        await Task.Delay(TimeSpan.FromSeconds(7), cancellationToken);
                        await SystemSettings.InputHandlerInstance.SendKeyAsync('e');
                        await Task.Delay(TimeSpan.FromSeconds(6), cancellationToken);
                        nextStop = DateTime.Now + RunTime;
                        SetVisibility(false);
                    }

                    using Bitmap img = SystemSettings.WindowCaptureHandlerInstance.GetWindow();
                    using Bitmap k1img = GetArea(img, Key1Rect);
                    Color k1c = ColourAverager.GetAverageColourFromImage(k1img);
                    if (redMin <= k1c.R && k1c.R <= 255)
                    {
                        await SystemSettings.InputHandlerInstance.SendKeyAsync('Q');
                    }
                    else
                    {
                        using Bitmap k2img = GetArea(img, Key2Rect);
                        Color k2c = ColourAverager.GetAverageColourFromImage(k2img);
                        if (redMin <= k2c.R && k2c.R <= 255)
                        {
                            await SystemSettings.InputHandlerInstance.SendKeyAsync('W');
                        }
                        else
                        {
                            using Bitmap k3img = GetArea(img, Key3Rect);
                            Color k3c = ColourAverager.GetAverageColourFromImage(k3img);
                            if (redMin <= k3c.R && k3c.R <= 255)
                            {
                                await SystemSettings.InputHandlerInstance.SendKeyAsync('O');
                            }
                            else
                            {
                                using Bitmap k4img = GetArea(img, Key4Rect);
                                Color k4c = ColourAverager.GetAverageColourFromImage(k4img);
                                if (redMin <= k4c.R && k4c.R <= 255)
                                {
                                    await SystemSettings.InputHandlerInstance.SendKeyAsync('P');
                                }
                            }
                        }
                    }

                    await Task.Delay(WaitInterval);
                }
            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception e)
            {

            }

            SetVisibility(false);
            _console.WriteLine("Ending module");
        }

        private void SetVisibility(bool isVisible)
        {
            Key1Area.IsVisible = isVisible;
            Key2Area.IsVisible = isVisible;
            Key3Area.IsVisible = isVisible;
            Key4Area.IsVisible = isVisible;
            UpperLeftArea.IsVisible = isVisible;
            UpperRightArea.IsVisible = isVisible;
            LowerLeftArea.IsVisible = isVisible;
            LowerRightArea.IsVisible = isVisible;
        }

        private static Bitmap GetArea(Bitmap bitmap, int x, int y, int width, int height) =>
            GetArea(bitmap, new Rectangle(x, y, width, height));

        private static Bitmap GetArea(Bitmap bitmap, Rectangle sourceRect)
        {
            var newBitmap = new Bitmap(sourceRect.Width, sourceRect.Height);
            var targetRect = new Rectangle(0, 0, sourceRect.Width, sourceRect.Height);
            using Graphics grD = Graphics.FromImage(newBitmap);
            grD.DrawImage(bitmap, targetRect, sourceRect, GraphicsUnit.Pixel);
            return newBitmap;
        }

        public void Cleanup()
        {
        }
    }
}
