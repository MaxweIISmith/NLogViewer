using System.Windows;
using System.Windows.Controls;
using NLog;

namespace NLogViewer
{
    public class LogItemStyleSelector : StyleSelector
    {
        #region Public Properties

        public Style DefaultStyle { get; set; }

        public Style ErrorStyle { get; set; }

        public Style WarningStyle { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override Style SelectStyle(object item, DependencyObject container)
        {
            var logEventInfo = item as LogEventInfo;

            if (logEventInfo == null)
                return DefaultStyle;

            if (logEventInfo.Level == LogLevel.Error || logEventInfo.Level == LogLevel.Fatal)
                return ErrorStyle;

            if (logEventInfo.Level == LogLevel.Warn)
                return WarningStyle;

            return DefaultStyle;
        }

        #endregion Public Methods
    }
}