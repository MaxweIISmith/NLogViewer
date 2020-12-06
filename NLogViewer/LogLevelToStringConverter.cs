using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using NLog;

namespace NLogViewer
{
    public class LogLevelToStringConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var level = value as LogLevel;

            return level.GetLogLevelDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}