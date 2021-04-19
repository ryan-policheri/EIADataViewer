using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotNetCommon.Security;
using DotNetCommon.EventAggregation;
using EIA.Services.Clients;
using EIADataViewer.Common;
using EIADataViewer.ViewModel;
using EIADataViewer.ViewModel.Base;
using DotNetCommon.Logging.File;
using Microsoft.Extensions.Logging;

namespace EIADataViewer
{
    public partial class App : Application
    {
        private IServiceProvider _provider;
        private IConfigurationRoot _config;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            LoadConfiguration();
            _provider = BuildServiceProvider();

            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            DataTemplateRegistrar templateRegistrar = new DataTemplateRegistrar(executingAssembly, "EIADataViewer.ViewModel", "EIADataViewer.View");
            templateRegistrar.RegisterAllTemplatesByConvention();

            MainViewModel viewModel = _provider.GetRequiredService<MainViewModel>();
            MainWindow window = new MainWindow();
            window.DataContext = viewModel;
            window.Show();
            await viewModel.LoadAsync();
        }

        private void LoadConfiguration()
        {
            IConfigurationRoot rawConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            _config = rawConfig;
        }

        private IServiceProvider BuildServiceProvider()
        {
            ICredentialProvider credProvider = new CredentialProvider("Personal_IV", "Personal_Key");


            ServiceCollection services = new ServiceCollection();

            string fileDirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            FileLoggerConfig config = new FileLoggerConfig(fileDirPath, fileDirPath.Trim('\\') + "EIADataViewerLog.log");
            FileLoggerProvider fileLoggerProvider = new FileLoggerProvider(config);

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddProvider(fileLoggerProvider);
            });

            services.AddTransient<ICredentialProvider, CredentialProvider>(x => new CredentialProvider("Personal_IV", "Personal_Key"));
            services.AddHttpClient("EiaClient", c =>
            {
                string baseAddress = _config["EiaBaseAddress"].TrimEnd('/');
                c.BaseAddress = new Uri(baseAddress);
                c.DefaultRequestHeaders.Add("Subscription-Key", credProvider.DecryptValue(_config["EiaApiKey"]));
            });

            services.AddTransient<EiaClient>(x => new EiaClient(x.GetRequiredService<IHttpClientFactory>().CreateClient("EiaClient")));

            services.AddSingleton<IMessageHub, MessageHub>();

            services.AddTransient<RobustViewModelDependencies>(x => new RobustViewModelDependencies(x.GetRequiredService<IServiceProvider>(), x.GetRequiredService<IMessageHub>(), x.GetRequiredService<ILogger<RobustViewModelDependencies>>()));
            services.AddTransient<RobustViewModelBase>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<DataExplorerViewModel>();
            services.AddTransient<DatasetFinderViewModel>();
            services.AddTransient<SeriesViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
