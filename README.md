# What is it?

Unity MVC+C stands for Model, View, Controller + Component.

It is an architecture, project manager and code generator that aims to implement a modular MVC concept on Unity project, making sure the project is clean, organized and no unwanted dependency is ever added to any feature of your project.

# Philosophy

Unity MVC+C is build aroung a few core philosophies:

- Every feature is a module that MUST work on its own or be explicit and clear about its dependencies.

- Every module has its own namespace.

- Every module has its own assembly.

- MonoBehaviour is to be used ONLY when it has to be a Monobehaviour. They can really mess up your project if overused.

- Events are the proper way to trigger changes on your game most of the times, as they keep your dependencies clean.

- Models are NOT MonoBehaviour.

- Controllers are NOT MonoBehaviour.

- Controllers NEVER access directly any class from another module. It calls the other module's controller and subscribe to its events.

- Every MonoBehaviour belong to View side of MVC.

- MVC Components take care of local data of the GameObject itself and provide data for the View.

# Installation

If you want to use the MVC+C Code Generator, for now please add this git link to your Unity Package Manager:

https://github.com/AlbertoVosgerau/UnityMVC-C.git#releases/0.0.3

Please, keep in ming that it is still on an alpha state and it might have a few changes and bug fixes in the future.
In special, there is no upgrade feture ow workflow for now, it is planed for the future, so feel free to use it, change it in any way you want, just keep in mind the current state of the project.

![Installation screenshot](https://github.com/AlbertoVosgerau/Unity_MVC/blob/develop/Screenshots/06.png)


### Architecture
The MVC+C architecture has the followin components:

### Controller

 #### Controller
 
 Controllers are not Monobehaviours. They are provided by MVCApplication and acessed by a locator trough MVCApplication.Controllers.Get<>();

 Controllers are responsible for business logic and hold the module's events. If something happens on the module, is the Controller that will inform other modules.
 If other Module's events are raised, it is the Controller that will inform the View, which will then raise internal events will be raised and cast to the module MVCComponents.

 Controllers are also allowed to call methods on the View.

### View

#### View

Views are Monobehaviours. They are the Controller counterpart that takes care of the unity scene's module classes and components.
It owns MVCComponents, that may or may not be part of its own hierarchy.

If any event happen on a MVCComponent object, it will raise events that should inform the View, which should inform Controller, that may then take actions or inform other modules.

### Model

#### Container

Containers are just that.
They hold data to be used by Controllers.
If they need to get any data from anywhere, they might call a Loader, than store its responde and provide it to the game.

#### Loader

Loader is responsible to fetch data from disk or web.
If data is not in the proper format, it can call a Solver, that will be responsible to get data in a format and output it in some other require data type.

#### Solver

Solver is responsible for get data in a format and output it in some other require data type

### Component

#### MVCComponents

##### MVCComponent

MVCComponents are essencially the owners of a Unity GameObject on the architecture.
If a View needs to talk to the GameObject or it needs any data or component, it will ask the MVCComponent, and it is MVCComponent's responsability to locally look for it and provide it.
It will also apply any internal logic that responds to what the object does.
It never calls any method on its owner View, it will raise events to pass data to it.

##### MVCComponentGroup

MVCComponentGroups are an exception to the system.
Let's say you cave a module that is important and nests other module's features. This component will be responsible to make the whole object hierarchy work, by calling other MVCComponents's methods.

##### UnityComponent

Simply put, it is the MVC+C version of the MonoBehaviour. It inherits from MonoBehaviour and organizes the structure to make sure it is consistent with the architecture code standards.
Their job is simple do the smaller piece of GameObject logic and can't access any external dependency.

# Ok, but how does it work?

## How to start a project

Using the MVC+C Code Generator is pretty straightfoward.
After importing Unity MVC+C package on your brand new Unity project, go to Unity MVC+C > Open Creation Window.
This will open the MVC+C Window, place it wherever you want on your Unity interface.

Now you need to initialize your project by creating your projects MVCApplication file.
A MVCApplication is the static class that will provide Controllers and is the place you can add your global application data if needed.
It will create a ~YourProjectName~MVCApplication.cs file at _Project/Application/Scripts

If you want to fill your folder structure, you can select which folders you want to create at this time too.

Then just create your project MVC+C Project.

Great, you now have created a whole MVC+C project.
From now on the MVC+C window will be where you start all the code for any module.

## Modules? What is it?

I've created my project, now what?

Well, now you are gona start your very first module.

A module is essencially a feature your project might have. Every feature should be a module, meaning it is designed to keep its dependencies contained on its own, making sure deleting, disabling or moving your feature around won't brake your project in any way, and if it does, it will be a very easy to spot problem. From now on, your project can grow as a collection of many pretty managable small modules, instead of a land of pain and dispair that many of us have witnessed in your life as dame developers.

Creating your first module is pretty simple, for your first module the MVC+C Code Generator will ask you the name and namespace of the module.
Just go ahead and create your very first module.

That's all! Your module is ready.
You can inspect your Modules folder and see how the structure of a module looks like.

Note: There's a known issue for now where for the first module, it won't recognize _Project/Modules folder as the module folder, it that is your case, please select this path manually. This will be fixed soon.
