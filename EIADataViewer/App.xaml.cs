using System;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EIADataViewer.Common;
using EIADataViewer.ViewModel;
using EIADataViewer.Startup;

namespace EIADataViewer
{
    public partial class App : Application
    {
        private IConfiguration _config;
        private IServiceProvider _provider;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            _config = Bootstrapper.LoadConfiguration();
            _provider = Bootstrapper.BuildServiceProvider(_config);

            RegisterApplicationDataTemplates();

            MainViewModel viewModel = _provider.GetRequiredService<MainViewModel>();
            MainWindow window = new MainWindow();
            window.DataContext = viewModel;
            window.Show();
            await viewModel.LoadAsync();
        }

        private void RegisterApplicationDataTemplates()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            DataTemplateRegistrar templateRegistrar = new DataTemplateRegistrar(executingAssembly, "EIADataViewer.ViewModel", "EIADataViewer.View");
            templateRegistrar.RegisterAllTemplatesByConvention();
        }
    }
}
