using System;
using wclCommon;
using wclBluetooth;
using BluetoothScanner.Model.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BluetoothScanner.Model
{
    /// <summary>
    /// Encapsulate functionality for BTLE device discovering.
    /// Delegates functionality for scanning GATT services to <see cref="ServicesScanner"/>
    /// </summary>
    public class DevicesScanner
    {
        #region Private fields
        private readonly wclBluetoothManager manager;
        #endregion

        #region Public events
        /// <summary>
        /// Fires on every device found
        /// </summary>
        public event Action<Device> DeviceFound;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="manager">Bluetooth Manager <see cref="https://docs.btframework.com/bluetooth/.net/html/T_wclBluetooth_wclBluetoothManager.htm"/></param>
        public DevicesScanner(wclBluetoothManager manager)
        {
            this.manager = manager;
            this.manager.OnDeviceFound += new wclBluetoothDeviceEvent(OnDeviceFound);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Starts scanning for the devices.
        /// Calls <see cref="DeviceFound"/> handler for every device found.
        /// </summary>
        public void StartScan()
        {
            wclBluetoothRadio radio = GetRadio();
            radio?.Discover(10, wclBluetoothDiscoverKind.dkBle);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Handler for wclBluetoothManagerOnDeviceFound Event 
        /// <see cref="https://docs.btframework.com/bluetooth/.net/html/E_wclBluetooth_wclBluetoothManager_OnDeviceFound.htm"/>
        /// </summary>
        /// <param name="sender">The object initiates the event</param>
        /// <param name="radio">A wclBluetoothRadio object represents a Bluetooth driver that fired the event <see cref="https://docs.btframework.com/bluetooth/.net/html/T_wclBluetooth_wclBluetoothRadio.htm"/></param>
        /// <param name="address">A remote device's MAC address</param>
        private async void OnDeviceFound(object sender, wclBluetoothRadio radio, long address)
        {
            Int32 res = radio.GetRemoteDeviceType(address, out wclBluetoothDeviceType devType);

            // Filter out non-LE devices.
            // It looks like filtering on radio.Discover don't work properly...
            if (devType != wclBluetoothDeviceType.dtBle) return;

            radio.GetRemoteRssi(address, out sbyte rssi);
            Int32 resName = radio.GetRemoteName(address, out string devName);

            Device device = new Device(
                address: address,
                name: resName == wclErrors.WCL_E_SUCCESS ? devName : "Error: 0x" + res.ToString("X8"),
                rssi: rssi,
                radio: radio,
                services: await GetServicesAsync(address, radio)
            );

            DeviceFound(device);
        }

        /// <summary>
        /// Gets GATT services asynchronously by
        /// converting callback pattern into Task.
        /// </summary>
        /// <param name="address">A remote device's MAC address</param>
        /// <param name="radio">Local Bluetooth radio module on which this device was found <see cref="https://docs.btframework.com/bluetooth/.net/html/T_wclBluetooth_wclBluetoothRadio.htm"/></param>
        /// <returns>List of GATT <see cref="Service"/> avaliable on the device</returns>
        public Task<List<Service>> GetServicesAsync(long address, wclBluetoothRadio radio)
        {
            var t = new TaskCompletionSource<List<Service>>();
            var servicesScanner = new ServicesScanner();
            servicesScanner.StartScan(address, radio, s => t.TrySetResult(s));
            return t.Task;            
        }

        /// <summary>
        /// Looks for the first available radio.
        /// </summary>
        /// <returns>A wclBluetoothRadio object represents a Bluetooth driver <see cref="https://docs.btframework.com/bluetooth/.net/html/T_wclBluetooth_wclBluetoothRadio.htm"/></returns>
        private wclBluetoothRadio GetRadio()
        {
            for (Int32 i = 0; i < manager.Count; i++)
            {
                if (!manager[i].Available) continue;
                return manager[i];                
            }
            return null;
        }
        #endregion
    }
}
