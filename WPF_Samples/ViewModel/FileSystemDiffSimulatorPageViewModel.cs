﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Stein_Samples.Services.FileSystemCompareService;
using Stein_Samples.Services.FileSystemCompareService.Helper;
using Stein_Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_Samples.Helper;
using WPF_Samples.Services;
using WPF_Samples.ViewModel.Helper;
using WPF_Samples.Views;

namespace WPF_Samples.ViewModel
{
    /// <summary>
    /// Controls the UI of the FileSystemDiff Simulator
    /// </summary>
    public class FileSystemDiffSimulatorPageViewModel : DataErrorViewModelBase
    {
        #region Fields

        private readonly IFileSystemCompareService _FileSystemCompareService;

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

        /// <summary>
        /// Binded items result list
        /// </summary>
        public ObservableCollection<FileSystemCompareOperation> Items { get; }

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

            Items = new ObservableCollection<FileSystemCompareOperation>();     //initialize Items

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

            Items.Clear();
        }

        /// <summary>
        /// trigger the diff calculation
        /// </summary>
        private void ExecuteCompare()
        {
            //clear
            Items.Clear();

            if (!IsValid())
            {
                //add the associated message to be displayed by the result control
                Items.Add(new FileSystemCompareOperation(message: this.Error));
                return;
            }

            if (_Path1 == _Path2)
            {
                //add the associated message to be displayed by the result control
                Items.Add(new FileSystemCompareOperation(message: "### Both Destinations are equal! ###"));
                return;
            }

            IEnumerable<FileSystemCompareOperation> result;
            try
            {
                //trigger the Diff calculation from the associated service
                result = _FileSystemCompareService.CompareFolder(_Path1, _Path2);
            }
            catch (UnauthorizedAccessException ex)
            {
                //Actually this should be delegated to a Logger or something
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;     //here we can continue execution
            }
            catch (Exception ex)
            {
                //Actually this should be delegated to a Logger or something
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }

            foreach (var item in result)
            {
                Items.Add(item);
            }

            if (!Items.Any())
            {
                //add the associated message to be displayed by the result control
                Items.Add(new FileSystemCompareOperation(message: "### Both Destinations are equal! ###"));
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

