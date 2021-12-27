using Microsoft.Toolkit.Mvvm.Input;
using WPF.Utils.Services;

namespace WPF.Samples.ViewModel
{
    /// <summary>
    /// Very simple Navigation logic
    /// </summary>
    public class HomePageViewModel
    {
        #region Fields

        private readonly IFrameNavigationService _navigationService;

        #endregion Fields


        #region Properties

        public string WindowTitle { get; }

        #endregion Properties


        #region Commands

        public RelayCommand NavigateToTextTokenizerCommand { get; set; }
        public RelayCommand NavigateToFileSystemDiffCommand { get; set; }

        #endregion Commands


        public HomePageViewModel(IFrameNavigationService navigationService)
        {
            WindowTitle = "Stein WPF.Samples Samples - Home";

            _navigationService = navigationService;

            NavigateToTextTokenizerCommand = new RelayCommand(ExecuteNavigateToTextTokenizer);
            NavigateToFileSystemDiffCommand = new RelayCommand(ExecuteNavigateToFileSystemDiff);
        }


        #region CommandHandler

        private void ExecuteNavigateToTextTokenizer()
        {
            _navigationService.NavigateTo("TextTokenizer");
        }

        private void ExecuteNavigateToFileSystemDiff()
        {
            _navigationService.NavigateTo("FileSystemDiffSimulator");
        }

        #endregion CommandHandler
    }
}
