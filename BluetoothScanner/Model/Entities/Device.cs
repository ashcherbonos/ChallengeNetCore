using System.Collections.Generic;
using wclBluetooth;

namespace BluetoothScanner.Model.Entities
{
    /// <summary>
    /// The entity of BTLE device model
    /// </summary>
    public class Device: IDevice
    {
        #region Public properties
        public long Address { get; }
        public string Name { get; }
        public sbyte Rssi { get; }
        public List<Service> Services { get; }
        #endregion

        #region Internal properties
        internal wclBluetoothRadio Radio { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="address">A remote device's MAC address</param>
        /// <param name="name">A remote device's name</param>
        /// <param name="rssi">Received Signal Strength Indicator in dB</param>
        /// <param name="radio">Local Bluetooth radio module on which this device was found <see cref="https://docs.btframework.com/bluetooth/.net/html/T_wclBluetooth_wclBluetoothRadio.htm"/></param>
        public Device(long address, string name, sbyte rssi, wclBluetoothRadio radio, List<Service> services)
        {
            Address = address;
            Name = name;
            Rssi = rssi;
            Radio = radio;
            Services = services;
        }
        #endregion
    }
}
