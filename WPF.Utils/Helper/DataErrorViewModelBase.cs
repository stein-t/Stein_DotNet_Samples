﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace WPF.Utils.Helper
{
    /// <summary>
    /// Abstract base class for your ViewModels to provide error handling per IDataErrorInfo implementation.
    /// Inherits MVVM Toolkit features, see https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/observablerecipient
    /// </summary>
    public abstract class DataErrorViewModelBase : ObservableRecipient, IDataErrorInfo
    {
        /// <summary>
        /// helper to provide overall validation status. In child classes, add all properties that are to be validated.
        /// </summary>
        protected string[] ValidatedProperties { get; set; }

        #region IDataErrorInfo Member
        public string Error { get; set; }

        public string this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }
        #endregion IDataErrorInfo Member

        /// <summary>
        /// specify how validate in child classes
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected abstract string GetValidationError(string propertyName);

        /// <summary>
        /// returns the overall validation status
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsValid()
        {
            foreach (var property in ValidatedProperties)
            {
                if (GetValidationError(property) != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
