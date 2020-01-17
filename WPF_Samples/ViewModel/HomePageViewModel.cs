using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Samples.Services;

namespace WPF_Samples.ViewModel
{
    public class HomePageViewModel
    {
        #region Fields

        private readonly IFrameNavigationService _navigationService;

        #endregion Fields


        #region Properties

        /// <summary>
        /// Window Title
        /// </summary>
        public string WindowTitle { get; }

        #endregion Properties


        #region Commands
        /// <summary>
        /// Command for triggering the NavigateToTextTokenizer Page action
        /// </summary>
        public RelayCommand NavigateToTextTokenizerCommand { get; set; }

        /// <summary>
        /// Command for NavigateToFileSystemDiff Page
        /// </summary>
        public RelayCommand NavigateToFileSystemDiffCommand { get; set; }

        #endregion Commands


        /// <summary>
        /// Initializes a new instance of the HomeViewModel class.
        /// Inject the IFrameNavigationService Dependency
        /// </summary>
        public HomePageViewModel(IFrameNavigationService navigationService)
        {
            WindowTitle = "Stein WPF Samples - Home";

            _navigationService = navigationService;

            NavigateToTextTokenizerCommand = new RelayCommand(ExecuteNavigateToTextTokenizer);
            NavigateToFileSystemDiffCommand = new RelayCommand(ExecuteNavigateToFileSystemDiff);
        }


        #region CommandHandler

        private void ExecuteNavigateToTextTokenizer()
        {
            //Navigate to Page
            _navigationService.NavigateTo("TextTokenizer");
        }

        private void ExecuteNavigateToFileSystemDiff()
        {
            //Navigate to Page
            _navigationService.NavigateTo("FileSystemDiffSimulator");
        }

        #endregion CommandHandler
    }
}
