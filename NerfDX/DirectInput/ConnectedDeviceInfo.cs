using SharpDX.DirectInput;

namespace NerfDX.DirectInput
{
    public class ConnectedDeviceInfo
    {
        public DeviceInstance Information { get; }
        public Capabilities Capabilities { get; }
        public DeviceProperties Properties { get; }

        public ConnectedDeviceInfo(WaitableJoystick joystick)
        {
            Information = joystick.Information;
            Capabilities = joystick.Capabilities;
            Properties = joystick.Properties;
        }
    }
}
