/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPF_Samples"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Stein_Samples.Services.FileSystemCompareService;
using Stein_Samples.Services.TextTokenizerService;
using System;
using WPF_Samples.Services;

namespace WPF_Samples.ViewModel.Helper
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();

            //register Pages
            SimpleIoc.Default.Register<FileSystemDiffSimulatorPageViewModel>();
            SimpleIoc.Default.Register<TextTokenizerPageViewModel>();
            SimpleIoc.Default.Register<HomePageViewModel>();

            SetupNavigation();

            RegisterServices();
        }

        /// <summary>
        /// register Services
        /// </summary>
        private void RegisterServices()
        {
            SimpleIoc.Default.Register< IFileSystemCompareService, FileSystemCompareService>();
            SimpleIoc.Default.Register<ITokenizerService, TokenizerService>();
        }

        /// <summary>
        /// Configure and register NavigationService
        /// </summary>
        private void SetupNavigation()
        {
            if (SimpleIoc.Default.IsRegistered<IFrameNavigationService>())
            {
                //prevent from registering multiple times in Design mode
                return;
            }

            var navigationService = new FrameNavigationService();

            navigationService.Configure("Home", new Uri("../Views/HomePage.xaml", UriKind.Relative));
            navigationService.Configure("TextTokenizer", new Uri("../Views/TextTokenizerPage.xaml", UriKind.Relative));
            navigationService.Configure("FileSystemDiffSimulator", new Uri("../Views/FileSystemDiffSimulatorPage.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }


        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public FileSystemDiffSimulatorPageViewModel FileSystemDiffSimulator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FileSystemDiffSimulatorPageViewModel>();
            }
        }

        public TextTokenizerPageViewModel TextTokenizer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TextTokenizerPageViewModel>();
            }
        }

        public HomePageViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomePageViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}