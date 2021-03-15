using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using PoliCommon.Security;
using EIA.Services.Clients;
using EIADataViewer.Core.ViewModels;

namespace EIADataViewer.Core
{
    public class App : MvxApplication
    {
        private IServiceProvider _provider;
        private IConfigurationRoot _config;

        public override void Initialize()
        {
            LoadConfiguration();
            ConfigureIoc();
            RegisterAppStart<MainViewModel>();
        }

        private void LoadConfiguration()
        {
            IConfigurationRoot rawConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            _config = rawConfig;
        }

        private void ConfigureIoc()
        {
            ICredentialProvider credProvider = new CredentialProvider("Personal_IV", "Personal_Key");

            ServiceCollection services = new ServiceCollection();
            services.AddHttpClient("EiaClient", c =>
            {
                string baseAddress = _config["EiaBaseAddress"].TrimEnd('/');
                c.BaseAddress = new Uri(baseAddress);
                c.DefaultRequestHeaders.Add("Subscription-Key", credProvider.DecryptValue(_config["EiaApiKey"]));
            });
            IServiceProvider netCoreServiceProvider = services.BuildServiceProvider();

            Mvx.IoCProvider.RegisterType<EiaClient>(() => new EiaClient(netCoreServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient("EiaClient")));
            Mvx.IoCProvider.RegisterType<DatasetFinderViewModel>();
            Mvx.IoCProvider.RegisterType<MainViewModel>();
        }
    }
}
