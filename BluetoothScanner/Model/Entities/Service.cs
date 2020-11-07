namespace BluetoothScanner.Model.Entities
{
    /// <summary>
    /// The entity of GATT service model
    /// <see cref="https://www.bluetooth.com/specifications/gatt/services/"/>
    /// </summary>
    public class Service
    {
        #region Public properties
        public string Uuid { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="uuid">Universally unique identifier</param>
        public Service(string uuid)
        {
            Uuid = uuid;
        }
        #endregion
    }
}
