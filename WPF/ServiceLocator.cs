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
using Samples.Services.FileSystemCompareService;
using Samples.Services.TextTokenizerService;
using System;
using WPF.Samples.ViewModel;
using WPF.Utils.Services;

namespace WPF.Samples
{
    /// <summary>
    /// This class responsible for building the service collection.
    /// It contains references for all viewmodels and provides an entry point for the bindings.
    /// </summary>
    public class ServiceLocator
    {
        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                // Services
                .AddSingleton<IFrameNavigationService, FrameNavigationService>()
                .AddSingleton<IFileSystemCompareService, FileSystemCompareService>()
                .AddSingleton<ITokenizerService, TokenizerService>()
                // Viewmodels
                .AddTransient<MainViewModel>()
                .AddTransient<HomePageViewModel>()
                .AddTransient<FileSystemDiffSimulatorPageViewModel>()
                .AddTransient<TextTokenizerPageViewModel>()
                .BuildServiceProvider();
        }

        private readonly IServiceProvider _serviceProvider;

        public ServiceLocator()
        {
            _serviceProvider = ConfigureServices();
        }

        public MainViewModel Main
        {
            get
            {
                return _serviceProvider.GetService<MainViewModel>();
            }
        }

        public FileSystemDiffSimulatorPageViewModel FileSystemDiffSimulator
        {
            get
            {
                return _serviceProvider.GetService<FileSystemDiffSimulatorPageViewModel>();
            }
        }

        public TextTokenizerPageViewModel TextTokenizer
        {
            get
            {
                return _serviceProvider.GetService<TextTokenizerPageViewModel>();
            }
        }

        public HomePageViewModel Home
        {
            get
            {
                return _serviceProvider.GetService<HomePageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}