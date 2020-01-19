using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_Samples.Helper
{
    /// <summary>
    /// Converter to do some value formatting for displaying purposes, i.e. put a dot "." after the step number  
    /// </summary>
    public class StepConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                //Just add a dot "." after the row number
                return string.Concat(value.ToString(), ".");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter to do some value formatting for displaying purposes, i.e. highlight error messages in the UI  
    /// </summary>
    public class MessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                //Just post and prefix (highlight) the message
                return string.Concat("### ", value, " ###");    
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
