using System;
using wclCommon;
using wclBluetooth;
using System.Collections.Generic;
using BluetoothScanner.Model.Entities;

namespace BluetoothScanner.Model
{
    /// <summary>
    /// Encapsulate functionality for GATT services discovering.
    /// </summary>
    public class ServicesScanner
    {
        #region Private fields
        private readonly wclGattClient сlient;
        #endregion
              
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public ServicesScanner()
        {
            сlient = new wclGattClient();            
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Starts scanning for services available on the device
        /// </summary>
        /// <param name="device">The device where scanning happens</param>
        /// <param name="extractor">on completion callback</param>
        public void StartScan(long address, wclBluetoothRadio radio, Action<List<Service>> extractor)
        {
            DissconnectFromTheDevice();
            сlient.OnConnect += (s,e) => GetServices(extractor);
            сlient.Address = address;            
            сlient.Connect(radio);
        }
        #endregion

        #region Private methods     
        /// <summary>
        /// Gets services from the connected device.
        /// Calls <see cref="ScanCompleted"/> handler.
        /// <param name="extractor">on completion callback</param>
        /// </summary>
        private void GetServices(Action<List<Service>> extractor)
        {
            var items = new List<Service>();

            Int32 Res = сlient.ReadServices(wclGattOperationFlag.goNone, out wclGattService[] services);
            if (Res != wclErrors.WCL_E_SUCCESS)
            {
                extractor.Invoke(items);
                DissconnectFromTheDevice();
                return;
            }

            if (services == null)
            {
                extractor.Invoke(items);
                DissconnectFromTheDevice();
                return;
            }

            foreach (wclGattService service in services)
            {
                items.Add(new Service(
                    uuid: service.Uuid.IsShortUuid ? service.Uuid.ShortUuid.ToString("X4") : service.Uuid.LongUuid.ToString()
                ));
            }

            extractor.Invoke(items);
            DissconnectFromTheDevice();
        }

        /// <summary>
        /// Disconnects from the currently connected device
        /// </summary>
        private void DissconnectFromTheDevice()
        {
            сlient.Disconnect();
        }
        #endregion
    }
}
