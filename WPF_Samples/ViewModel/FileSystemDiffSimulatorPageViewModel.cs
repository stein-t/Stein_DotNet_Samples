using GalaSoft.MvvmLight.Command;
using Stein_Samples.Services.FileSystemCompareService;
using Stein_Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;using System.Windows;
using WPF_Infrastructure.Helper;
using WPF_Samples.Helper;

namespace WPF_Samples.ViewModel
{
    /// <summary>
    /// ViewModel Logic for supporting the GUI of the FileSystemDiffSimulator
    /// </summary>
    public class FileSystemDiffSimulatorPageViewModel : DataErrorViewModelBase
    {
        #region Fields

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();   //retrieve the logging instance
        private readonly IFileSystemCompareService _FileSystemCompareService;                   //the associated services

        #endregion Fields


        #region Properties

        /// <summary>
        /// Window Title
        /// </summary>
        public string WindowTitle { get; }

        private string _Path1;
        /// <summary>
        /// first folder destination
        /// </summary>
        public string Path1
        {
            get
            {
                return _Path1;
            }
            set
            {
                if (value != _Path1)
                {
                    _Path1 = Utils.CheckPathEnding(value);
                    RaisePropertyChanged("Path1");
                }
            }
        }

        private string _Path2;
        /// <summary>
        /// second folder destination
        /// </summary>
        public string Path2
        {
            get
            {
                return _Path2;
            }
            set
            {
                if (value != _Path2)
                {
                    _Path2 = Utils.CheckPathEnding(value);
                    RaisePropertyChanged("Path2");
                }
            }
        }

        private IEnumerable<FileSystemCompareOperation> _Items;
        /// <summary>
        /// Binded items result list
        /// </summary>
        public IEnumerable<FileSystemCompareOperation> Items
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
        /// Command for triggering the DIFF action
        /// </summary>
        public RelayCommand CompareCommand { get; set; }

        /// <summary>
        /// Command for refreshing the GUI 
        /// </summary>
        public RelayCommand ClearCommand { get; set; }

        #endregion Commands


        /// <summary>
        /// Initializes a new instance of the FileSystemDiffViewModel class.
        /// Inject IFileSystemCompareService
        /// </summary>
        public FileSystemDiffSimulatorPageViewModel(IFileSystemCompareService fileSystemCompareService)
        {
            WindowTitle = "Stein WPF Samples - Filesystem Diff Simulator";

            _FileSystemCompareService = fileSystemCompareService;

            //specify properties that are to be validated
            ValidatedProperties = new string[] { "Path1", "Path2" };

            CompareCommand = new RelayCommand(ExecuteCompare);
            ClearCommand = new RelayCommand(ExecuteClear);

            _Path1 = @"C:\";
            _Path2 = @"C:\";
        }


        #region CommandHandler

        /// <summary>
        /// clear the UI
        /// </summary>
        private void ExecuteClear()
        {
            ////Provide valid inputs to prevent the input validation from firing on initialization
            //Path1 = @"C:\";
            //Path2 = @"C:\";

            Items = null;
        }

        /// <summary>
        /// trigger the diff calculation
        /// </summary>
        private void ExecuteCompare()
        {
            //clear
            Items = null;

            Logger.Debug(string.Concat("Input 1: ", _Path1));         //Log Input1
            Logger.Debug(string.Concat("Input 2: ", _Path2));         //Log Input2

            if (!IsValid())
            {
                //add the associated message to be displayed by the result control
                Items = new List<FileSystemCompareOperation>() { new FileSystemCompareOperation(message: this.Error) };
                Logger.Error(this.Error);       //Log as Error
                return;
            }

            try
            {
                //retrieve result from the associated Service
                Items = _FileSystemCompareService.CompareFolder(_Path1, _Path2);
            }
            catch (UnauthorizedAccessException ex)
            {
                Logger.Error(ex.Message);       //Log Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;     //here we can continue execution
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message);       //Log Exception
                MessageBox.Show("Unexpected Error. Please contact support!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        #endregion CommandHandler


        #region Validation

        /// <summary>
        /// define input property validation
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        override protected string GetValidationError(string propertyName)
        {
            //string error = null;

            switch (propertyName)
            {
                case "Path1":
                    Error = _FileSystemCompareService.CheckDirectoryExists(Path1);
                    break;
                case "Path2":
                    Error = _FileSystemCompareService.CheckDirectoryExists(Path2);
                    break;
                default:
                    break;
            }

            return Error;
        }

        #endregion Validation
    }
}

