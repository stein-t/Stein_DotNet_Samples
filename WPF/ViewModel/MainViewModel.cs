using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using WPF_Utils.Services;

namespace WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ObservableRecipient
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IFrameNavigationService _navigationService;

        #endregion Fields


        #region Commands
        /// <summary>
        /// Command for triggering the Loaded action
        /// </summary>
        public RelayCommand LoadedCommand { get; set; }

        #endregion Commands


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// Inject the IFrameNavigationService Dependency
        /// </summary>
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