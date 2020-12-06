using System.Windows;
using NLog;

namespace NLogViewerExampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger Logger = LogManager.GetLogger("Common");
        
        public MainWindow()
        {
            InitializeComponent();

            Logger.Info("This is very very very very very very very very very very very very very very very very very very very very very very very very very very very very long message");
        }

        private void AddDebugMessage(object sender, RoutedEventArgs e)
        {
            Logger.Debug("This is debug message");
        }

        private void AddErrorMessage(object sender, RoutedEventArgs e)
        {
            Logger.Error("This is error message");
        }

        private void AddInfoMessage(object sender, RoutedEventArgs e)
        {
            Logger.Info("This is info message");
        }

        private void AddWarningMessage(object sender, RoutedEventArgs e)
        {
            Logger.Warn("This is warning message");
        }
    }
}
