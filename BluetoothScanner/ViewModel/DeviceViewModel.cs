using BluetoothScanner.Model.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BluetoothScanner.ViewModel
{
    /// <summary>
    /// ViewModel for the <see cref="IDevice"/>.
    /// Presents GATT services in a human-readable format.
    /// </summary>
    class DeviceViewModel
    {
        #region Public properies
        public string Name { get; }
        public sbyte Rssi { get; }
        public List<ServiceViewModel> Services { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="device">Entity of <see cref="IDevice"/> model</param>
        public DeviceViewModel(IDevice device)
        {
            Name = device.Name;
            Rssi = device.Rssi;
            Services = device.Services.Select(s => new ServiceViewModel(s)).ToList();
        }
        #endregion
    }
}
