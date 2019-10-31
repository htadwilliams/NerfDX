using NerfDX.DirectInput;
using NerfDX.Events;
using SharpDX;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace JoyForm
{

    public partial class Form : System.Windows.Forms.Form
    {
        private readonly DirectInputManager directInput;
        private ReadOnlyCollection<ConnectedDeviceInfo> connectedDeviceInfos;

        // For cross-thread event marshalling
        private delegate void OnEventControllerInvoker(object sender, BusEventArgs<EventController> e);
        private delegate void OnEventControllersChangedInvoker(object sender, BusEventArgs<EventControllersChanged> e);

        public Form()
        {
            InitializeComponent();

            try
            {
                directInput = new DirectInputManager();

                directInput.Initialize();

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
            catch (SharpDXException e)
            {
                MessageBox.Show(e.Message, "DirectInput initialization exception");

                if (null != directInput)
                {
                    directInput.Dispose();
                }
            }
        }

        /// <summary>
        /// Cache connected device information and tell ListView to update 
        /// itself.
        /// </summary>
        /// <param name="connectedDeviceInfos"></param>
        private void UpdateConnectedDevices(ReadOnlyCollection<ConnectedDeviceInfo> connectedDeviceInfos)
        {
            this.connectedDeviceInfos = connectedDeviceInfos;
            listViewControllers.VirtualListSize = connectedDeviceInfos.Count;
            listViewControllers.Invalidate();
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
            if (InvokeRequired && !IsDisposed)
            {
                OnEventControllersChangedInvoker d = new OnEventControllersChangedInvoker(OnEventControllersChanged);
                this.Invoke(d, new object[] {sender, e});
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
            if (InvokeRequired && !IsDisposed)
            {
                OnEventControllerInvoker d = new OnEventControllerInvoker(OnEventController);
                this.Invoke(d, new object[] {sender, e});
                return;
            }
            
            EventController controllerEvent = e.BusEvent;

            switch (controllerEvent.Type)
            {
                case EventController.EventType.Button:
                    textBoxControllerLog.AppendText(" " + controllerEvent.Joystick.Information.InstanceName + 
                        " (Button " + controllerEvent.Button + " " + controllerEvent.ButtonValue + ")\r\n");
                    break;

                case EventController.EventType.POV:
                    textBoxControllerLog.AppendText(" " + controllerEvent.Joystick.Information.InstanceName + 
                        " (POV " + controllerEvent.POVState + ")\r\n");
                    break;

                // TODO do something with axis name and value 
                case EventController.EventType.Axis:
                    break;
            }
        }

        private void ListViewControllers_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ConnectedDeviceInfo connectedDeviceInfo = connectedDeviceInfos[e.ItemIndex];

            // Name
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = connectedDeviceInfo.Information.InstanceName;

            // Type
            ListViewSubItem listViewSubItem = new ListViewSubItem();
            listViewSubItem.Text = connectedDeviceInfo.Information.Type.ToString();
            listViewItem.SubItems.Add(listViewSubItem);

            // Buttons
            listViewSubItem = new ListViewSubItem();
            listViewSubItem.Text = connectedDeviceInfo.Capabilities.ButtonCount.ToString();
            listViewItem.SubItems.Add(listViewSubItem);

            // Hats
            listViewSubItem = new ListViewSubItem();
            listViewSubItem.Text = connectedDeviceInfo.Capabilities.PovCount.ToString();
            listViewItem.SubItems.Add(listViewSubItem);

            // Axes
            listViewSubItem = new ListViewSubItem();
            listViewSubItem.Text = connectedDeviceInfo.Capabilities.AxeCount.ToString();
            listViewItem.SubItems.Add(listViewSubItem);

            e.Item = listViewItem;
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            textBoxControllerLog.Clear();
        }
    }
}
