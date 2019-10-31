using NerfDX.DirectInput;
using System.Collections.ObjectModel;

namespace NerfDX.Events
{
    public class EventControllersChanged
    {
        public ReadOnlyCollection<ConnectedDeviceInfo> ConnectedDeviceInfos { get; }

        public EventControllersChanged(ReadOnlyCollection<ConnectedDeviceInfo> connectedDeviceInfos)
        {
            ConnectedDeviceInfos = connectedDeviceInfos;
        }
    }
}
