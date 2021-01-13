using System;
using System.Windows;
using Metis.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Metis.Application
{
    public partial class App
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureServices)
                .Build();
        }

        private void ConfigureLogging(IConfiguration configuration, ILoggingBuilder logging) {}
        private void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging) =>
            ConfigureLogging(context.Configuration, logging);

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services) =>
            services
                .AddSingleton<MainWindow>()
                .AddSingleton<SystemSettings>();
        private void ConfigureServices(HostBuilderContext context, IServiceCollection services) =>
            ConfigureServices(context.Configuration, services);

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host) await _host.StopAsync(TimeSpan.FromSeconds(2));

            base.OnExit(e);
        }
    }
}
