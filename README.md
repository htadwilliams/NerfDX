# NerfDX Facade for SharpDX DirectInput. 

A C# library that to make usage of SharpDX DirectInput easier.

SharpDX is a great thin C# wrapper around Microsoft's DirectX library. To use it correctly still takes a lot of work though, and requires the following:

* Setup to create the necessary objects, most of which are IDisposable wrappers around the underlying COM objects and require teardown and management to avoid leaking resources.
* Threading to poll for joystick input.
* Optional threading to wait for joystick input events. DirectInput 8 added the ability to wait on events instead and reduce the polling overhead for some types of applications. Most games with a tight graphical rendering or executive loop probably won't benefit much, and some devices don't generate events and still must be polled.
* Handling scenarios where controller devices are plugged / unplugged during operation. This very much complicates optional use of a waiting thread. Even if a waiting thread isn't used, device code objects must be cleaned up correctly in order to re-connect to them if they're plugged in again.

NerfDX does all this work and provides a simpler high-level interface that aims to be easy to use without sacrificing performance or features.

## Usage

Here is an example of the minimal code required to use NerfDX.DirectInput. There are other events published and other features that can be seen in the example applications.

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

## NuGet

The library is available as a NuGet package at https://www.nuget.org/packages/NerfDX/. If searching from Visual Studio, search for "NerfDX" and tick "Include prelease" checkbox.

## Dependencies

NerfDX only depends on SharpDX libraries in order to reduce the chance of diamond dependency or library hell scenarios. 

Loggers may be supplied at runtime to allow the underlying components to use a logging framework such as Log4net or NLog. The constructor of DirectInputManager optionally allows a logger wrapper to be supplied.

## Features

* Minimal code to setup / teardown.
* Event notification for controller button, POV hat, and axis inputs.
* Event notification when controllers are un/plugged.
* Only polls controllers if absolutely necessary. Event notification introduced with DirectInput 8 is used if a device doesn't require polling.
* Listening and waiting threads are only created as needed and discarded if not in use.
* Logs to console output by default, but logging facility may be plugged-in and more or less verbose logging configured.
* Includes EventBus, a generic pub/sub implementation.
* Includes ReturningEventBus which is the same as EventBus, but can be used to aggregate results from event listeners.
* EventBus and ReturningEventBus include unit tests.

## Examples

Included example application projects attempt to show the simplest possible usage and dependencies for the given scenario:

Example      | Scenario
------------ | -------------
JoyConsole   | Console application only with logging to stdout.
JoyLog       | Console application plugs in Log4net wrapper (NerfDX knows nothing about Log4net).
JoyForm      | Windows forms application displays list of connected controllers and log of controller input.
JoyWPF       | Identical to JoyForm but using WPF instead of forms.
