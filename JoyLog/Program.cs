using NerfDX.DirectInput;
using NerfDX.Events;
using SharpDX;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JoyLog
{
    /// <summary>
    /// Example of NerfDX logger plug-in usage.
    /// </summary>
    /// 
    /// <description>
    /// To use logging plug-in feature, write a NerfDX.Logging.ILogger implementation 
    /// wrapping your favorite logging framework (in this case, JoyLog.Logger4net) and 
    /// supply it when constructing a NerfDX.DirectInput.DirectInputManager.
    /// </description>
    class Program
    {
        private static readonly log4net.ILog LOGGER = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            ExitOnKeypress();

            using (DirectInputManager directInputManager = new DirectInputManager(
                // Supply ILogger for DirectInputManager to use 
                new Logger4net(typeof(DirectInputManager).ToString())))
            {
                try
                {
                    directInputManager.Initialize();
                }
                catch (SharpDXException e)
                {
                    LOGGER.Error("Exception initializing NerfDX.DirectInputManager", e);
                }

                EventBus<EventController>.Instance.EventRecieved += OnEventController;
                EventBus<EventControllersChanged>.Instance.EventRecieved += OnEventControllersChanged;

                // Runs until application exits via keypress
                Thread.Sleep(System.Threading.Timeout.Infinite);
            }
        }

        private static void OnEventControllersChanged(object sender, BusEventArgs<EventControllersChanged> e)
        {
            LOGGER.Info(e.BusEvent.ToString());
        }

        private static void OnEventController(object sender, BusEventArgs<EventController> e)
        {
            if (e.BusEvent.Type != EventController.EventType.Axis)
            {
                LOGGER.Info(e.BusEvent.ToString());
            }
        }

        private static void ExitOnKeypress()
        {
            LOGGER.Info(" ");
            LOGGER.Info("Press any key to exit ...");
            LOGGER.Info(" ");

            Task.Factory.StartNew(() =>
            {
                Console.ReadKey();
                Environment.Exit(0);
            });
        }
    }
}
