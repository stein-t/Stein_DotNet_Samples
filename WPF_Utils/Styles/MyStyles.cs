using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_Utils.Styles
{
    /// <summary>
    /// enhance styles with some UI interactions
    /// </summary>
    public partial class MyStyles : ResourceDictionary
    {
        #region Select Text on focus, see https://social.msdn.microsoft.com/Forums/vstudio/en-US/564b5731-af8a-49bf-b297-6d179615819f/how-to-selectall-in-textbox-when-textbox-gets-focus-by-mouse-click?forum=wpf

        private void SelectText(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

        #endregion Select Text on focus

        private void FocusOnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.Focus();
            }
        }
    }
}
