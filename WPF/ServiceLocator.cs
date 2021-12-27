/*
  In App.xaml:
  <Application.Resources>
      <vm:ServiceLocator xmlns:vm="clr-namespace:WPF.Samples.ViewModel.Helper"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Samples.Services.FileSystemCompareService;
using Samples.Services.TextTokenizerService;
using System;
using WPF.Samples.ViewModel;
using WPF.Utils.Services;

namespace WPF.Samples
{
    /// <summary>
    /// This class responsible for building the service collection.
    /// It contains static references to all the required services and view models and provides an entry point for the bindings.
    /// </summary>
    public class ServiceLocator
    {
        public ServiceLocator()
        {
            var services = new ServiceCollection();

            // Services
            services
                .AddSingleton<IFileSystemCompareService, FileSystemCompareService>()
                .AddSingleton<ITokenizerService, TokenizerService>();

            // Viewmodels
            services
                .AddTransient<MainViewModel>()
                .AddTransient<HomePageViewModel>()
                .AddTransient<FileSystemDiffSimulatorPageViewModel>()
                .AddTransient<TextTokenizerPageViewModel>();

            SetupNavigation(services);

            Ioc.Default.ConfigureServices(services.BuildServiceProvider());
        }

        /// <summary>
        /// Configure and register NavigationService
        /// </summary>
        private static void SetupNavigation(IServiceCollection services)
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure("Home", new Uri("../Views/HomePage.xaml", UriKind.Relative));
            navigationService.Configure("TextTokenizer", new Uri("../Views/TextTokenizerPage.xaml", UriKind.Relative));
            navigationService.Configure("FileSystemDiffSimulator", new Uri("../Views/FileSystemDiffSimulatorPage.xaml", UriKind.Relative));

            services.AddSingleton<IFrameNavigationService>(navigationService);
        }

        public static MainViewModel Main
        {
            get
            {
                return Ioc.Default.GetService<MainViewModel>();
            }
        }

        public static FileSystemDiffSimulatorPageViewModel FileSystemDiffSimulator
        {
            get
            {
                return Ioc.Default.GetService<FileSystemDiffSimulatorPageViewModel>();
            }
        }

        public static TextTokenizerPageViewModel TextTokenizer
        {
            get
            {
                return Ioc.Default.GetService<TextTokenizerPageViewModel>();
            }
        }

        public static HomePageViewModel Home
        {
            get
            {
                return Ioc.Default.GetService<HomePageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}