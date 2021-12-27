using Microsoft.Toolkit.Mvvm.Input;
using System;
using WPF.Utils.Services;

namespace WPF.Samples.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IFrameNavigationService _navigationService;

        #endregion Fields


        #region Commands

        public RelayCommand LoadedCommand { get; set; }

        #endregion Commands


        public MainViewModel(IFrameNavigationService navigationService)
        {
            Logger.Info("Application started");

            _navigationService = SetupNavigation(navigationService);

            LoadedCommand = new RelayCommand(ExecuteLoaded);
        }


        #region CommandHandler

        private void ExecuteLoaded()
        {
            //Initialize the Frame with the HomePage
            _navigationService.NavigateTo("Home");
        }

        #endregion CommandHandler

        /// <summary>
        /// Configure and register NavigationService
        /// </summary>
        private static IFrameNavigationService SetupNavigation(IFrameNavigationService navigationService)
        {
            navigationService.Configure("Home", new Uri("../Views/HomePage.xaml", UriKind.Relative));
            navigationService.Configure("TextTokenizer", new Uri("../Views/TextTokenizerPage.xaml", UriKind.Relative));
            navigationService.Configure("FileSystemDiffSimulator", new Uri("../Views/FileSystemDiffSimulatorPage.xaml", UriKind.Relative));
            return navigationService;
        }
    }
}