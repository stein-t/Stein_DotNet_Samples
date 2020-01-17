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
    /// Log to controls the UI of the TextTokenizer
    /// </summary>
    public class TextTokenizerPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly ITokenizerService _TokenizerService;

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

        /// <summary>
        /// Binded items result list
        /// </summary>
        public ObservableCollection<string> Items { get; }

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

            Items = new ObservableCollection<string>();    //initialize list
        }


        #region CommandHandler

        /// <summary>
        /// clear the UI
        /// </summary>
        private void ExecuteClear()
        {
            Items.Clear();
        }

        /// <summary>
        /// trigger the Tokenizing calculation
        /// </summary>
        private void ExecuteTokenize()
        {
            //clear
            Items.Clear();

            IEnumerable<Word> result;
            try
            {
                //trigger the Diff calculation from the associated service
                result = _TokenizerService.Tokenize(_Text);
            }
            catch (Exception ex)
            {
                //Actually this should be delegated to a Logger or something
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            foreach (var w in result.Where(w => !string.IsNullOrEmpty(w.Message)))
            {
                //Display any information/warning/error messages
                Items.Add(w.Message + "\n");
            }

            //query all words
            var words = result.Where(w => string.IsNullOrEmpty(w.Message)).ToList();

            if (!string.IsNullOrEmpty(_Text))
            {
                Items.Add(String.Format("This text has {0} words", words.Count()));
            }

            if (words.Any())
            {
                var maxLength = words.Max(w => w.Length);       //retrieve the maximum length

                //query words by length
                for (int l = 1; l <= maxLength; l++)
                {
                    var wordsQuery = words.Where(w => w.Length == l).Select(w => w.Value);
                    if (!wordsQuery.Any())
                    {
                        Items.Add(String.Format("words with {0} letter did not occur", l));
                    }
                    else
                    {
                        Items.Add(String.Format("words with {0} letters occured {1} times (words={2})", l, wordsQuery.Count(), String.Join(", ", wordsQuery)));
                    }
                }
            }
        }

        #endregion CommandHandler
    }
}
