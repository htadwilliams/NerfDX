using NerfDX.DirectInput;
using NerfDX.Events;
using SharpDX;
using SharpDX.DirectInput;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace JoyWPF
{
    /// <summary>
    /// Used to display controller information in DataGrid
    /// </summary>
    public class ControllerDisplayInfo
    {
        public string Name { get; }
        public DeviceType Type { get; }
        public int Buttons { get; }
        public int Axes { get; }
        public int Hats { get; }

        public ControllerDisplayInfo(ConnectedDeviceInfo deviceInfo)
        {
            Name = deviceInfo.Information.InstanceName;
            Type = deviceInfo.Information.Type;
            Buttons = deviceInfo.Capabilities.ButtonCount;
            Hats = deviceInfo.Capabilities.PovCount;
            Axes = deviceInfo.Capabilities.AxeCount;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml as simple example of NerfDX usage
    /// </summary>
    /// <description>
    /// There are a lot cleaner ways to do the WPF things but this example 
    /// attempts to do everything as minimally as possible.
    /// </description>
    public partial class MainWindow : Window
    {
        private readonly DirectInputManager directInput;

        // For cross-thread event marshalling
        private delegate void OnEventControllerInvoker(object sender, BusEventArgs<EventController> e);
        private delegate void OnEventControllersChangedInvoker(object sender, BusEventArgs<EventControllersChanged> e);

        private List<ControllerDisplayInfo> controllerDisplayInfos = new List<ControllerDisplayInfo>();

        public MainWindow()
        {
            InitializeComponent();

            // Constructs wrapped SharpDX.DirectInput instance but doesn't 
            // start DirectInputManager()
            directInput = new DirectInputManager();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                //
                // Initialization of DirectInputManager and registration for 
                // events via EventBus may be done in any order.
                //

                // Register for controller button, POV, and axis events 
                EventBus<EventController>.Instance.EventRecieved += OnEventController;

                // Register for event when connected controller list changes
                // Occurs when a controller is un/plugged (or otherwise en/disabled)
                EventBus<EventControllersChanged>.Instance.EventRecieved += OnEventControllersChanged;

                // Creates and starts threads to either poll or wait for joystick input.
                // Also synchronously enumerates and connects to all controller devices.
                // May throw SharpDXException
                directInput.Initialize();

                // This is also happens OnEventControllersChanged()
                UpdateConnectedDevices(directInput.GetConnectedDeviceInfos());
            }
            catch (SharpDXException exception)
            {
                MessageBox.Show(exception.Message, "DirectInput initialization exception");
                if (null != directInput)
                {
                    directInput.Dispose();
                }
            }
        }

        /// <summary>
        /// Tell ControllerDataGrid to update itself with new connected device 
        /// information.
        /// </summary>
        private void UpdateConnectedDevices(ReadOnlyCollection<ConnectedDeviceInfo> connectedDeviceInfos)
        {
            // Distill displayed information and feed it to DataGrid
            controllerDisplayInfos = new List<ControllerDisplayInfo>(connectedDeviceInfos.Count);
            foreach (ConnectedDeviceInfo deviceInfo in connectedDeviceInfos)
            {
                controllerDisplayInfos.Add(new ControllerDisplayInfo(deviceInfo));
            }
            ControllerDataGrid.ItemsSource = controllerDisplayInfos;
        }

        /// <summary>
        /// Handles EventControllersChanged sent by DirectInputManager via 
        /// EventBus<EventControllersChanged> when list of connected 
        /// controller devices is updated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEventControllersChanged(object sender, BusEventArgs<EventControllersChanged> e)
        {
            // Events from DirectInputManager aren't on the UI thread, so 
            // updates to window elements must be marshalled via Dispatcher.Invoke()
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new OnEventControllersChangedInvoker(OnEventControllersChanged), sender, e);
                return;
            }
            
            UpdateConnectedDevices(e.BusEvent.ConnectedDeviceInfos);
        }

        /// <summary>
        /// Handles EventController sent by DirectInputManager via EventBus<
        /// EventController> when any controller input occurs. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEventController(object sender, BusEventArgs<EventController> e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new OnEventControllerInvoker(OnEventController), sender, e);
                return;
            }
            
            EventController controllerEvent = e.BusEvent;

            switch (controllerEvent.Type)
            {
                case EventController.EventType.Button:
                    ControllerLog.Text += " " + controllerEvent.Joystick.Information.InstanceName + 
                        " (Button " + controllerEvent.Button + " " + controllerEvent.ButtonValue + ")\r\n";
                    break;

                case EventController.EventType.POV:
                    ControllerLog.Text += " " + controllerEvent.Joystick.Information.InstanceName + 
                        " (POV " + controllerEvent.POVState + ")\r\n";
                    break;

                // TODO do something with axis name and value 
                case EventController.EventType.Axis:
                    break;
            }
            ControllerLog.ScrollToEnd();
        }

        private void OnButtonClear(object sender, RoutedEventArgs e)
        {
            ControllerLog.Clear();
        }
    }
}
