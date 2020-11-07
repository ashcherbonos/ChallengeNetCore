using BluetoothScanner.Model.Entities;
using System;

namespace BluetoothScanner.Model
{
    /// <summary>
    /// The interface for the Scanner model
    /// </summary>
    public interface IScanner
    {
        public enum State { Idle, Scanning }
        public event Action<IDevice> DeviceFound;
        public event Action<State> StateChanged;
        public void Start();
        public void Stop();
        public void GetDevices();
    }
}
