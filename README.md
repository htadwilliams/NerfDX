# NerfDX High-level Facade for SharpDX DirectInput. 

Makes usage of SharpDX DirectInput soft and easy by encapsulating all of the setup, teardown, and threading required. 

SharpDX is a great thin C# wrapper around Microsoft's DirectX library. To use it though still takes a lot of work to do correctly and requires the following:

* Setup to create the necessary objects, most of which are IDisposable wrappers around the underlying COM objects and require teardown and management to avoid leaking resources.
* Threading to poll for joystick input.
* Optional threading to wait for joystick input events. DirectInput 8 added the ability to wait on events instead and reduce the polling overhead for some types of applications. Most games with a tight graphical rendering or executive loop probably won't benefit much, and some devices don't generate events and still must be polled.
* Handling scenarios where controller devices are plugged / unplugged during operation. This very much complicates optional use of a waiting thread. Even if a waiting thread isn't used, device code objects must be cleaned up correctly in order to re-connect to them if they're plugged in again.

## Dependencies

NerfDX only depends on SharpDX libraries in order to reduce the chance of diamond dependency or library hell scenarios. 

Loggers may be supplied at runtime to allow the underlying components to use a logging framework such as Log4net or NLog. The constructor of DirectInputManager optionally allows a logger wrapper to be supplied.

## Usage

Here is an example of the minimal code required to use NerfDX.DirectInput.

Instantiate and initialize the library
```c#
// Register for controller button, POV, and axis events (may be done at any time)
EventBus<EventController>.Instance.EventRecieved += OnEventController;

// Constructs wrapped SharpDX.DirectInput instance 
directInput = new DirectInputManager();

// Starts threads that either poll or wait for joystick input 
directInput.Initialize();
```

Process events from the library
```c#
private static void OnEventController(object sender, BusEventArgs<EventController> e)
{
    // Do something with SharpDX JoystickUpdate information
    Console.WriteLine(e.JoystickUpdate.Offset.ToString(), e.JoystickUpdate.Value);
}
```

## Example projects

Included example projects show simplest possible usage and dependencies for the scenario:

Example      | Scenario
------------ | -------------
JoyConsole   | Console application only with logging to stdout.
JoyLog       | Console application plugs in Log4net wrapper (NerfDX knows nothing about Log4net).
JoyForm      | Windows forms application displays list of connected controllers and log of controller input.
JoyWPF WPF   | WPF application displays list of connected controllers and log of controller input.

