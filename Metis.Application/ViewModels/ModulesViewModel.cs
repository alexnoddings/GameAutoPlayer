using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Metis.Core.Console;
using Metis.Core.Display;
using Metis.Core.Module;
using Metis.Core.Settings;
using Metis.Modules.PianoGame;
using Metis.Modules.TypingGame;

namespace Metis.Application.ViewModels
{
    internal class ModulesViewModel : BaseViewModel
    {
        private ISystemSettings _systemSettings;
        [AutoInject]
        public ISystemSettings SystemSettings
        {
            get => _systemSettings;
            set => Set(ref _systemSettings, value);
        }

        private IDisplayHelper _displayHelper;
        [AutoInject]
        public IDisplayHelper DisplayHelper
        {
            get => _displayHelper;
            set => Set(ref _displayHelper, value);
        }

        private IConsoleFactory _consoleFactory;
        [AutoInject]
        public IConsoleFactory ConsoleFactory
        {
            get => _consoleFactory;
            set => Set(ref _consoleFactory, value);
        }

        private Task _task;
        public Task Task
        {
            get => _task;
            set => Set(ref _task, value);
        }

        public IList<IModule> Modules { get; }

        private IModule _selectedModule;
        public IModule SelectedModule
        {
            get => _selectedModule;
            set => Set(ref _selectedModule, value);
        }

        public bool IsRunning => !Task.IsCompleted;

        public RelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        public ModulesViewModel()
        {
            Task = Task.CompletedTask;

            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);

            Modules = new List<IModule>
            {
                CreateModule<PianoGameModule>(),
                CreateModule<TypingGameModule>(),
            };
            SelectedModule = Modules.First();
        }

        private void Start()
        {
            if (IsRunning) return;
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _task = SelectedModule.RunAsync(_cancellationToken);
        }

        private void Stop()
        {
            if (!IsRunning) return;
            _cancellationTokenSource.Cancel();
        }

        private static TModule CreateModule<TModule>()
            where TModule : IModule
        {
            Type type = typeof(TModule);
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            ConstructorInfo targetConstructor;
            switch (constructors.Length)
            {
                case 0:
                    throw new ArgumentException($"Module {type.Name} does not provide any public constructors");
                case 1:
                    targetConstructor = constructors[0];
                    break;
                default:
                {
                    var preferredConstructors = constructors
                        .Where(c => c.GetCustomAttribute<PreferredModuleConstructorAttribute>() != null)
                        .ToList();
                    targetConstructor = preferredConstructors.Count switch
                    {
                        0 => throw new ArgumentException($"Module {type.Name} has more than 1 public constructors but none marked with {nameof(PreferredModuleConstructorAttribute)}"),
                        1 => preferredConstructors[0],
                        _ => throw new ArgumentException($"Module {type.Name} has more than 1 public constructors marked with {nameof(PreferredModuleConstructorAttribute)}")
                    };
                    break;
                }
            }

            object[] parameters = targetConstructor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .Select(ViewModelLocator.GetInstanceOf)
                .ToArray();

            object module = targetConstructor.Invoke(parameters);

            return (TModule)module;
        }
    }
}
