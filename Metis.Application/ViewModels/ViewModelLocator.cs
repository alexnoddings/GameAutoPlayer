using System;
using GalaSoft.MvvmLight.Ioc;
using Metis.Application.Models;
using Metis.Application.Models.Console;
using Metis.Core.Console;
using Metis.Core.Display;
using Metis.Core.ImageProcessing.Ocr;
using Metis.Core.Settings;
using Metis.ImageProcessing.Ocr.Tesseract;

namespace Metis.Application.ViewModels
{
    internal class ViewModelLocator
    {
        public ViewModelLocator()
        {
            RegisterServices();
            RegisterViewModels();
        }

        private static void RegisterServices()
        {
            SimpleIoc.Default.Register<ISystemSettings, SystemSettings>();
            SimpleIoc.Default.Register<IConsoleFactory, ConsoleFactory>();
            SimpleIoc.Default.Register<IDisplayHelper, DisplayHelper>();
            SimpleIoc.Default.Register<IOcrHandler, TesseractOcrHandler>();
            SimpleIoc.Default.Register<ApplicationSettings>();
        }

        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ConsoleViewModel>();
            SimpleIoc.Default.Register<ModulesViewModel>();
            SimpleIoc.Default.Register<SystemSettingsViewModel>();
        }

        public MainViewModel Main => GetViewModel<MainViewModel>();

        public ConsoleViewModel Console => GetViewModel<ConsoleViewModel>();

        public ModulesViewModel Modules => GetViewModel<ModulesViewModel>();

        public SystemSettingsViewModel SystemSettings => GetViewModel<SystemSettingsViewModel>();

        public static object GetInstanceOf(Type type) => SimpleIoc.Default.GetInstance(type);

        private static T GetViewModel<T>() => SimpleIoc.Default.GetInstance<T>();
    }
}
