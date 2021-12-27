using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Samples.Services.TextTokenizerService;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF.Samples.ViewModel
{
    /// <summary>
    /// ViewModel Logic for supporting the GUI of the TextTokenizer
    /// </summary>
    public class TextTokenizerPageViewModel : ObservableRecipient
    {
        #region Fields
        
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();   // retrieve the logging instance
        private readonly ITokenizerService _TokenizerService;                                   // the associated services

        #endregion Fields


        #region Properties

        public string WindowTitle { get; }

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
                    OnPropertyChanged();
                }
            }
        }
        private string _Text;

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
                    OnPropertyChanged();
                }
            }
        }
        private IEnumerable<string> _Items;

        #endregion Properties


        #region Commands

        public RelayCommand TokenizeCommand { get; set; }
        public RelayCommand ClearCommand { get; set; }

        #endregion Commands


        public TextTokenizerPageViewModel(ITokenizerService tokenizerService)
        {
            WindowTitle = "Stein WPF.Samples Samples - Text Tokenizer";

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
            Items = null;       // clear result

            try
            {
                // retrieve result from the associated Service
                Items = _TokenizerService.TokenizeAndConvert(_Text);
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);
                MessageBox.Show("Critical Error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion CommandHandler
    }
}
