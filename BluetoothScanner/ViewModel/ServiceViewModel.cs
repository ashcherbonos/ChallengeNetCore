using BluetoothScanner.Model.Entities;
using System.Collections.Generic;

namespace BluetoothScanner.ViewModel
{
    /// <summary>
    /// ViewModel for the <see cref="Service"/>.
    /// Presents GATT services in a human-readable format.
    /// </summary>
    public class ServiceViewModel
    {
        #region Public properies
        public string Name { get; }
        #endregion

        #region Private fields
        /// <summary>
        /// Human readable GATT services names <see cref="https://www.bluetooth.com/specifications/gatt/services/"/>
        /// </summary>
        private static readonly Dictionary<string, string> HumanReadableNames = new Dictionary<string, string>
        {
            ["1800"] = "Generic Access",
            ["1811"] = "Alert Notification Service",
            ["1815"] = "Automation IO",
            ["180F"] = "Battery Service",
            ["183B"] = "Binary Sensor",
            ["1810"] = "Blood Pressure",
            ["181B"] = "Body Composition",
            ["181E"] = "Bond Management Service",
            ["181F"] = "Continuous Glucose Monitoring",
            ["1805"] = "Current Time Service",
            ["1818"] = "Cycling Power",
            ["1816"] = "Cycling Speed and Cadence",
            ["180A"] = "Device Information",
            ["183C"] = "Emergency Configuration",
            ["181A"] = "Environmental Sensing",
            ["1826"] = "Fitness Machine",
            ["1801"] = "Generic Attribute",
            ["1808"] = "Glucose",
            ["1809"] = "Health Thermometer",
            ["180D"] = "Heart Rate",
            ["1823"] = "HTTP Proxy",
            ["1812"] = "Human Interface Device",
            ["1802"] = "Immediate Alert",
            ["1821"] = "Indoor Positioning",
            ["183A"] = "Insulin Delivery",
            ["1820"] = "Internet Protocol Support Service",
            ["1803"] = "Link Loss",
            ["1819"] = "Location and Navigation",
            ["1827"] = "Mesh Provisioning Service",
            ["1828"] = "Mesh Proxy Service",
            ["1807"] = "Next DST Change Service",
            ["1825"] = "Object Transfer Service",
            ["180E"] = "Phone Alert Status Service",
            ["1822"] = "Pulse Oximeter Service",
            ["1829"] = "Reconnection Configuration",
            ["1806"] = "Reference Time Update Service",
            ["1814"] = "Running Speed and Cadence",
            ["1813"] = "Scan Parameters",
            ["1824"] = "Transport Discovery",
            ["1804"] = "Tx Power",
            ["181C"] = "User Data",
            ["181D"] = "Weight Scale"
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="service"></param>
        public ServiceViewModel(Service service)
        {
            Name = HumanReadableNames.GetValueOrDefault(service.Uuid, $"Unknown [{service.Uuid}]");
        }
        #endregion
    }
}
