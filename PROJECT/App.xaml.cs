using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repo.Hotel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PROJECT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }

        public IConfiguration Configuration { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var navigationService = ServiceProvider.GetRequiredService<Login>();
            navigationService.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Login>();
            services.AddScoped<HotelContext>();
        }
    }

}

