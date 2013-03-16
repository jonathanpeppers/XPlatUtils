# XPlatUtils

A set of helpers for cross platform apps.  Right now has a ServiceContainer and Messenger implementation. This is provided as a portable class library.

## ServiceContainer

ServiceContainer is a simple IoC container, very similar to what you get with Game.Services in XNA or MonoGame.

Registration is explicit and accessed via a static class, all registrations are singleton.

Here are some examples of registration:

    //Our types
    interface MyInterface { }
    class MyClass : MyInterface { }
    
    //This would register MyClass with the default constructor
    ServiceContainer.Register<MyClass>();
    //So equivalent to
    ServiceContainer.Register<MyClass>(() => new MyClass());
    //Also equivalent to
    ServiceContainer.Register(typeof(MyClass), () => new MyClass());
    
    //Or if you already have an instance of an object
    ServiceContainer.Register(myClass);
    //Which is equivalent to
    ServiceContainer.Register<MyClass>(() => myClass);
    
    //You can also register interfaces
    ServiceContainer.Register<MyInterface>(() => new MyClass());
    
    //To retrieve an instance
    MyClass myClass = ServiceContainer.Resolve<MyClass>();
    MyInterface myInterface = ServiceContainer.Resolve<MyInterface>();
    
    //Also the same as
    MyClass myClass = ServiceContainer.Resolve(typeof(MyClass)) as MyClass;
    MyInterface myInterface = ServiceContainer.Resolve(typeof(MyInterface)) as MyInterface;

Other notes:
* Double registrations will just overwrite the previous
* If a type is not found, an exception will be thrown
* Cyclic dependencies will work just fine, no exceptions will be thrown -- just use sparingly for your co-worker's sake

Some nice things you can do on each platform:
* iOS
    * Register controllers created by XIBs or storyboard such as: `public MyController(IntPtr handle) : base(handle) { ServiceContainer.Register(this); }`
    * You can also hook up storyboard creation like this: `ServiceContainer.Register(() => (MyController)Storyboard.InstantiateViewController ("MyController"));`
* Android
    * Setup a custom `Android.App.Application` class and register everything in `OnCreate`
    * You can register activities in OnCreate, or even OnResume/OnPause if that makes more sense
* WinRT/Windows Phone
    * Use it for binding View to ViewModel: `DataContext = ServiceContainer.Resolve<MyViewModel>()`

## Messenger

This is a simple messenger for sending loosely coupled messages out to multiple subscribers.  I feel this is critical for the nature of cross platform apps.  Sometimes ViewModels need to send events to the View and vice versa, this is an easy way to make that happen.

Examples:

    //First make a Message class
    class MyMessage : Message { public MyMessage(object sender) : base(sender) { } }
    
    //Now any class can register such as
    var messenger = new Messenger();
    messenger.Subscribe<MyMessage>(m => {
        //Do something here
    });

    //Or use a method    
    messenger.Subscribe<MyMessage>(MyMethod);

    //For cleanliness, use in combination with ServiceContainer
    var messenger = ServiceContainer.Resolve<IMessenger>();

    //To send a message
    messenger.Publish(new MyMessage(this));

    //To unsubscribe
    messenger.Unsubscribe<MyMessage>(MyMethod);

Notes:
* Messenger uses WeakReference so if you forget to unsubscribe, there shouldn't be a memory leak
* This is great for application level events, such as backgrounding/foregrounding the app, etc.



