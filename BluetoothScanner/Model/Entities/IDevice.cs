using System.Collections.Generic;

namespace BluetoothScanner.Model.Entities
{
    /// <summary>
    /// The entity interface of BTLE device model
    /// </summary>
    public interface IDevice
    {
        #region Public properties
        public long Address { get; }
        public string Name { get; }
        public sbyte Rssi { get; }
        public List<Service> Services { get; }
        #endregion
    }
}
