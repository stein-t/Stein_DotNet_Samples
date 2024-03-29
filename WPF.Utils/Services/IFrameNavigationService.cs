﻿using System;

namespace WPF.Utils.Services
{
    /// <summary>
    /// Taken from then original GalaSoft.MvvmLight.Views
    /// An interface defining how navigation between pages should be performed in various frameworks such as Windows, Windows Phone, Android, iOS etc.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        string CurrentPageKey
        {
            get;
        }

        /// <summary>
        /// If possible, instructs the navigation service to discard the current page and display the previous page on the navigation stack.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Instructs the navigation service to display a new page corresponding to the given key.
        /// Depending on the platforms, the navigation service might have to be configured with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed</param>
        void NavigateTo(string pageKey);

        /// <summary>
        /// Instructs the navigation service to display a new page corresponding to the given key,
        /// and passes a parameter to the new page. Depending on the platforms,
        /// the navigation service might have to be Configure with a key/page list.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page that should be displayed</param>
        /// <param name="parameter">The parameter that should be passed to the new page</param>
        void NavigateTo(string pageKey, object parameter);
    }

    /// <summary>
    /// IFrameNavigationService Interface
    /// see https://github.com/SamTheDev/SampleMvvmLightNavigation
    /// see https://stackoverflow.com/questions/28966819/mvvm-light-5-0-how-to-use-the-navigation-service
    /// </summary>
    public interface IFrameNavigationService : INavigationService
    {
        object Parameter { get; }

        void Configure(string key, Uri pageType);
    }
}
