using System;
using System.Windows;
using BluetoothScanner.Model;
using BluetoothScanner.ViewModel;

namespace BluetoothScanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private fields
        /// <summary>
        /// This is Model
        /// </summary>
        private readonly IScanner scanner;

        /// <summary>
        /// This is ViewModel
        /// </summary>
        private readonly ApplicationViewModel viewModel;
        #endregion

        #region Constructors
        /// <summary>
        /// Entry point
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            scanner = new Scanner();
            viewModel = new ApplicationViewModel(scanner);
            DataContext = viewModel;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Starts scanning devices as soon as the window is loaded
        /// </summary>
        private void FmMain_Loaded(object sender, RoutedEventArgs e)
        {
            scanner.Start();
            scanner.GetDevices();
        }

        /// <summary>
        /// Stops all services and releases all allocated resources on window closed
        /// </summary>
        private void FmMain_Closed(object sender, EventArgs e)
        {
            scanner.Stop();
        }
        #endregion
    }    
}
