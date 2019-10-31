using NerfDX.DirectInput;
using NerfDX.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JoyConsole
{
    class Program
    {
        public static EventController LastAxisEvent { get; private set; }

        static void Main()
        {
            DirectInputManager directInput = null;

            ExitOnKeypress();

            try
            {
                //
                // Creation of DirectInputManager and registration for events via EventBus
                // may be done in any order.
                //

                // Constructs wrapped SharpDX.DirectInput instance but nothing more
                directInput = new DirectInputManager();

                // Starts threads that either poll or wait for joystick input 
                directInput.Initialize();

                // Register for controller button, POV, and axis events 
                EventBus<EventController>.Instance.EventRecieved += OnEventController;

                // Register for event when connected controller list changes
                // Occurs when a controller is un/plugged (or otherwise en/disabled)
                EventBus<EventControllersChanged>.Instance.EventRecieved += OnEventControllersChanged;

                // Runs until application exits via keypress
                Thread.Sleep(System.Threading.Timeout.Infinite);
            }
            catch(SharpDX.SharpDXException e)
            {
                Console.WriteLine("SharpDX Exception while initializing DirectInputManager: " + e);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                directInput.Dispose();
            }
        }

        private static void OnEventControllersChanged(object sender, BusEventArgs<EventControllersChanged> e)
        {
            Console.WriteLine("Controller un/plugged. New count: " + e.BusEvent.ConnectedDeviceInfos.Count);
        }

        private static void OnEventController(object sender, BusEventArgs<EventController> e)
        {
            EventController controllerEvent = e.BusEvent;

            // Prevents spamming every time an axis is moved by writing that
            // information only when buttons are pressed
            if (controllerEvent.Type == EventController.EventType.Axis)
            {
                LastAxisEvent = e.BusEvent;
            }
            else
            {
                Console.WriteLine(controllerEvent.ToString());

                if (LastAxisEvent != null)
                {
                    Console.WriteLine(LastAxisEvent.ToString());
                }
            }
        }

        private static void ExitOnKeypress()
        {
            Console.WriteLine();
            Console.WriteLine("Press controller buttons or move hats (last axis movement will be reported).");
            Console.WriteLine("Press any key to exit ...");
            Console.WriteLine();

            Task.Factory.StartNew(() =>
            {
                Console.ReadKey();
                Environment.Exit(0);
            });
        }
    }
}
