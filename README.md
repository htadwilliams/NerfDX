# NerfDX

High-level Facade for SharpDX DirectInput

Makes usage of SharpDX easy by encapsulating all of the setup, teardown, and threading required. 

## Dependencies

NerfDX only depends on SharpDX libraries in order to reduce the chance of diamond dependency or library hell scenarios.

## Usage

Here is an example of the minimal code required to use NerfDX.DirectInput.

```c#
// Constructs wrapped SharpDX.DirectInput instance 
directInput = new DirectInputManager();

// Starts threads that either poll or wait for joystick input 
directInput.Initialize();

// Register for controller button, POV, and axis events 
EventBus<EventController>.Instance.EventRecieved += OnEventController;
```

```c#
private static void OnEventController(object sender, BusEventArgs<EventController> e)
{
    // Do something with SharpDX JoystickUpdate information
    Console.WriteLine(e.JoystickUpdate.Offset.ToString(), e.JoystickUpdate.Value);
}
```

## Examples

Examples included show simplest possible usage for the scenario:

* JoyConsole Console application only with logging to stdout.
* JoyLog Console application plugs in Log4net wrapper (NerfDX knows nothing about Log4net).
* JoyForm Windows forms application displays list of connected controllers and displays controller input.
* JoyWPF WPF application displays list of connected controllers and displays controller input.
