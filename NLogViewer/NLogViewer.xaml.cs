using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using NLog;
using NLog.Common;

namespace NLogViewer
{
    /// <summary>
    /// Interaction logic for NLogViewer.xaml
    /// </summary>
    public partial class NLogViewer : UserControl
    {
        #region Public Fields

        public static readonly DependencyProperty IsLoggerNameVisibleProperty =
            DependencyProperty.Register("IsLoggerNameVisible", typeof(bool), typeof(NLogViewer), new PropertyMetadata(true));

        public static readonly DependencyProperty IsLogLevelVisibleProperty =
            DependencyProperty.Register("IsLogLevelVisible", typeof(bool), typeof(NLogViewer), new PropertyMetadata(true));

        #endregion Public Fields

        #region Private Fields

        private bool _autoScrollToLast = true;

        #endregion Private Fields

        #region Public Constructors

        public NLogViewer()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                foreach (NlogViewerTarget target in LogManager.Configuration.AllTargets
                    .Where(t => t is NlogViewerTarget).Cast<NlogViewerTarget>())
                {
                    //IsTargetConfigured = true;
                    target.LogReceived += LogReceived;
                }
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public bool AutoScrollToLast
        {
            get { return _autoScrollToLast; }
            set
            {
                _autoScrollToLast = value;

                if (_autoScrollToLast)
                    ScrollToLast();
            }
        }

        public bool IsLoggerNameVisible
        {
            get { return (bool)GetValue(IsLoggerNameVisibleProperty); }
            set { SetValue(IsLoggerNameVisibleProperty, value); }
        }

        public bool IsLogLevelVisible
        {
            get { return (bool)GetValue(IsLogLevelVisibleProperty); }
            set { SetValue(IsLogLevelVisibleProperty, value); }
        }

        public ObservableCollection<LogEventInfo> LogEntries { get; private set; } =
            new ObservableCollection<LogEventInfo>();

        public int MaxLogEntries { get; set; } = 0;

        #endregion Public Properties

        #region Private Methods

        private void ClearLogEntries(object sender, RoutedEventArgs e)
        {
            LogEntries.Clear();
        }

        private void CopySelectedItems(object sender, RoutedEventArgs e)
        {
            var selectedItems = DataGrid.SelectedItems;

            var text = new StringBuilder();

            foreach (LogEventInfo item in selectedItems)
            {
                var message = $"{item.TimeStamp:yyyy.MM.dd hh:mm:ss}: {(IsLoggerNameVisible ? $"[{item.LoggerName}]" : String.Empty) }[{item.Level.GetLogLevelDescription()}] {item.FormattedMessage}";
                text.AppendLine(message);
            }

            Clipboard.SetText(text.ToString());
        }

        private void LogReceived(AsyncLogEventInfo log)
        {
            var logEvent = log.LogEvent;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (MaxLogEntries > 0 && LogEntries.Count >= MaxLogEntries)
                {
                    while (LogEntries.Count >= MaxLogEntries)
                    {
                        LogEntries.RemoveAt(0);
                    }
                }

                LogEntries.Add(logEvent);

                if (AutoScrollToLast)
                    ScrollToLast();
            }));
        }

        private void ScrollToLast()
        {
            if (LogEntries.Any())
                DataGrid.ScrollIntoView(LogEntries.Last());
        }

        #endregion Private Methods
    }
}