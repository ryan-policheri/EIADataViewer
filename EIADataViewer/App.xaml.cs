using EIADataViewer.Services;
using EIADataViewer.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PoliCommon.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace EIADataViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _provider;
        private IConfigurationRoot _config;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            LoadConfiguration();
            _provider = BuildServiceProvider();

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

            services.AddTransient<ICredentialProvider, CredentialProvider>(x => new CredentialProvider("Personal_IV", "Personal_Key"));
            services.AddHttpClient("EiaClient", c =>
            {
                string baseAddress = _config["EiaBaseAddress"].TrimEnd('/');
                c.BaseAddress = new Uri(baseAddress);
                c.DefaultRequestHeaders.Add("Subscription-Key", credProvider.DecryptValue(_config["EiaApiKey"]));
            });

            services.AddTransient<EiaClient>(x => new EiaClient(x.GetRequiredService<IHttpClientFactory>().CreateClient("EiaClient")));

            services.AddTransient<MainViewModel>();
            services.AddTransient<FoobarViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
