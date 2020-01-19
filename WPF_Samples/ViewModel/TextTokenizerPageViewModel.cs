using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Stein_Samples.Services.TextTokenizerService;
using Stein_Samples.Services.TextTokenizerService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Samples.Services;

namespace WPF_Samples.ViewModel
{
    /// <summary>
    /// ViewModel Logic for supporting the GUI of the TextTokenizer
    /// </summary>
    public class TextTokenizerPageViewModel : ViewModelBase
    {
        #region Fields
        
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();   //retrieve the logging instance
        private readonly ITokenizerService _TokenizerService;                                   //the associated services

        #endregion Fields


        #region Properties

        /// <summary>
        /// Window Title
        /// </summary>
        public string WindowTitle { get; }

        private string _Text;
        /// <summary>
        /// Text Input
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    RaisePropertyChanged("Text");
                }
            }
        }

        private IEnumerable<string> _Items;
        /// <summary>
        /// Binded items result list
        /// </summary>
        public IEnumerable<string> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (value != _Items)
                {
                    _Items = value;
                    RaisePropertyChanged("Items");
                }
            }
        }

        #endregion Properties


        #region Commands

        /// <summary>
        /// Command for triggering the Tokenizer action
        /// </summary>
        public RelayCommand TokenizeCommand { get; set; }

        /// <summary>
        /// Command for refreshing the GUI 
        /// </summary>
        public RelayCommand ClearCommand { get; set; }

        #endregion Commands


        /// <summary>
        /// Initializes a new instance of the TextTokenizerPageViewModel class.
        /// Inject the ITokenizerService Dependency
        /// </summary>
        public TextTokenizerPageViewModel(ITokenizerService tokenizerService)
        {
            WindowTitle = "Stein WPF Samples - Text Tokenizer";

            _TokenizerService = tokenizerService;

            TokenizeCommand = new RelayCommand(ExecuteTokenize);
            ClearCommand = new RelayCommand(ExecuteClear);
        }


        #region CommandHandler

        /// <summary>
        /// clear the UI
        /// </summary>
        private void ExecuteClear()
        {
            Items = null;
        }

        /// <summary>
        /// trigger the Tokenizing calculation
        /// </summary>
        private void ExecuteTokenize()
        {
            //clear
            Items = null;

            try
            {
                //retrieve result from the associated Service
                Items = _TokenizerService.TokenizeAndConvert(_Text);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);       //Log Exception
                MessageBox.Show("Unexpected Error. Please contact support!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion CommandHandler
    }
}
