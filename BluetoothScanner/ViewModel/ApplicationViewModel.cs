using BluetoothScanner.Model;
using BluetoothScanner.Model.Entities;
using BluetoothScanner.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BluetoothScanner.ViewModel
{
    /// <summary>
    /// ViewModel for the <see cref="MainWindow"/>
    /// </summary>
    class ApplicationViewModel: INotifyPropertyChanged
    {
        #region Public properies
        public ObservableCollection<DeviceViewModel> Devices { get; private set; }

        /// <summary>
        /// Scanner is not busy and a user can click on the Refresh button
        /// </summary>
        private bool refreshIsPossible = false;
        public bool RefreshIsPossible
        {
            get { return refreshIsPossible; }
            set
            {
                refreshIsPossible = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Device selected by user
        /// </summary>
        private DeviceViewModel selectedDevice;
        public DeviceViewModel SelectedDevice
        {
            get { return selectedDevice; }
            set
            {
                selectedDevice = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Private fields        
        private readonly IScanner scanner;
        #endregion

        #region Commands
        /// <summary>
        /// Command for the "Rescan" button
        /// </summary>
        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (refreshCommand == null)
                {
                    refreshCommand = new RelayCommand(obj =>
                    {
                        Devices.Clear();
                        scanner.GetDevices();
                    },
                    obj => RefreshIsPossible);
                }
                return refreshCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="scanner">Facade for the Model <see cref="Scanner"/></param>
        public ApplicationViewModel(IScanner scanner)
        {
            Devices = new ObservableCollection<DeviceViewModel>();

            this.scanner = scanner;

            scanner.DeviceFound += OnDeviceFound;
            scanner.StateChanged += state => RefreshIsPossible = state == IScanner.State.Idle;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Handler for the <see cref="Scanner.DeviceFound"/> event
        /// </summary>
        /// <param name="device"></param>
        private void OnDeviceFound(IDevice device)
        {
            Devices.Add(new DeviceViewModel(device));
        }
        #endregion

        #region INotifyPropertyChanged implementation
        /// <summary>
        /// INotifyPropertyChanged interface implementation
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged?view=netcore-3.1"/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion        
    }
}
