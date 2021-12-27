using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using WPF.Utils.Services;

namespace WPF.Samples.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ObservableRecipient
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

            _navigationService = navigationService;
            LoadedCommand = new RelayCommand(ExecuteLoaded);
        }


        #region CommandHandler

        private void ExecuteLoaded()
        {
            //Initialize the Frame with the HomePage
            _navigationService.NavigateTo("Home");
        }

        #endregion CommandHandler
    }
}