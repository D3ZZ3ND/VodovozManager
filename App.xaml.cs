using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using VodovozManager.Data.Repositories; 
using VodovozManager.ViewModels;
using NHibernate.Cfg;

namespace VodovozManager
{
  public partial class App : Application
  {
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
      ServiceCollection services = new ServiceCollection();
      ConfigureServices(services);
      _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
      // Фабрика сессий, как синглтон.
      services.AddSingleton<ISessionFactory>(provider => {
        var cfg = new NHibernate.Cfg.Configuration();

        // из hibernate.cfg.xml.
        cfg.Configure(); 
        return cfg.BuildSessionFactory();
      });

      // Регистрация репозитория и ViewModel.
      services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
      services.AddTransient<MainViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      var mainWindow = new MainWindow
      {
        // GetRequiredService для получения ViewModel.
        DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
      };
      mainWindow.Show();
    }
  }
}

