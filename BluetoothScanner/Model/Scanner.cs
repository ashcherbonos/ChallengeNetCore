using System;
using wclBluetooth;
using BluetoothScanner.Model.Entities;
using static BluetoothScanner.Model.IScanner;

namespace BluetoothScanner.Model
{
    /// <summary>
    /// Facade of the Model.
    /// Scanner class delegates it functionality to 2 classes:
    /// <see cref="DevicesScanner"/> and <see cref="ServicesScanner"/>
    /// </summary>
    public class Scanner: IScanner
    {
        #region Private fields
        private readonly wclBluetoothManager bluetoothManager;
        private readonly DevicesScanner devicesScanner;
        #endregion

        #region Public events
        /// <summary>
        /// Fires on every device found
        /// </summary>
        public event Action<IDevice> DeviceFound;

        /// <summary>
        /// Fires on changing scanner state between <see cref="State.Idle"/> and <see cref="State.Scanning"/>
        /// </summary>
        public event Action<State> StateChanged;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Scanner()
        {
            bluetoothManager = new wclBluetoothManager();
            devicesScanner = new DevicesScanner(bluetoothManager);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Model initialization. Need to be called once before use.
        /// Opens the Bluetooth Manager and allocated required hardware resources.
        /// Binds event handlers.
        /// </summary>
        public void Start()
        {
            bluetoothManager.Open();
            bluetoothManager.OnDiscoveringStarted += (sender, radio) => StateChanged(State.Scanning);
            bluetoothManager.OnDiscoveringCompleted += (sender, radio, error) => StateChanged(State.Idle);
            devicesScanner.DeviceFound += DeviceFound;
        }

        /// <summary>
        /// Closes the Bluetooth Manager and releases all allocated resources. 
        /// </summary>
        public void Stop()
        {
            bluetoothManager.Close();
        }

        /// <summary>
        /// Starts scanning for devices.
        /// Calls <see cref="DeviceFound"/> handler for every device found.
        /// </summary>
        public void GetDevices()
        {
            devicesScanner.StartScan();
        }
        #endregion
    }
}
