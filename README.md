# What is it?

Unity MVC+C stands for Model, View, Controller + Component.

It is an architecture, project manager and code generator that aims to implement a modular MVC concept on Unity project, making sure the project is clean, organized and no unwanted dependency is ever added to any feature of your project.

# Philosophy

Unity MVC+C is built around a few core philosophies:

- Every feature is a module that MUST work on its own or be explicit and clear about its dependencies.

- Every module has its own independent folder that can be easily detached or deleted from the project.

- Every module has its own namespace.

- Every module has its own assembly.

- MonoBehaviour is to be used ONLY when it has to be a Monobehaviour. They can really mess up your project if overused.

- Events are the proper way to trigger changes on your game most of the times, as they keep your dependencies clean.

- Models are NOT MonoBehaviour.

- Controllers are NOT MonoBehaviour.

- Controllers NEVER access directly any class from another module. It calls the other module's controller and subscribe to its events.

- Views are MonoBehaviours.

- MVC Components take care of local data of the GameObject itself and provide data for the View.

# Installation

If you want to use the MVC+C Code Generator, for now please add this git link to your Unity Package Manager:

https://github.com/AlbertoVosgerau/UnityMVC-C.git#releases/0.0.3

Please, keep in ming that it is still on an alpha state and it might have a few changes and bug fixes in the future.
In special, there is no upgrade feture ow workflow for now, it is planed for the future, so feel free to use it, change it in any way you want, just keep in mind the current state of the project.

![Installation screenshot](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/06.png)

NOTE: If your project doesn't support assemby definitions or rely on code that doesn't use it, you may want to import the no-assembly-definition version available on the releases section of this repository. If you can work with assembly definitions, however, it is strongly recommended that you use the package manager version instead.

https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/releases/tag/no-assembly-definition


### Architecture

The MVC+C architecture has the following components:


![Unity MVC+C Hierarchy](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/Hierarchy.png)

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

##### Assembly Definitions

On top of the architecture structure, MVC+C makes use of Assembly Definitions.
That means every module will live inside of its own assembly. This is done by placing an AssemblyDefinition file inside the Module's Script folder, which will be done automatically for you.

![Assembly Definition Screenshot](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/15.png)

If you want to use any class from outside your module, you will now have to explicitly add its reference to the Assembly Definition file, like that:

![Adding Assembly Definition reference](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/16.png)

Assembly Definitions are an amazing tool to prevent bad habits with code, making sure no reference is added to the module without explicit knowledge of the developer, it also won't allow you to create cyclic dependencies, meaning if two modules depend on each other, you probably want to create a third module that will interface their common behaviour, or passing data trough events, delegates or wathever your team agree upon.

Also, Assembly Definitions allow us to implement unit tests and yes, we got you covered on that one too! MVC+C will automatically generate Tests folders with a basic setup for every feature and module.


# Ok, but how does it work?

## How to start a project

Using the MVC+C Code Generator is pretty straightfoward.
After importing Unity MVC+C package on your brand new Unity project, go to Unity MVC+C > Open Creation Window.

![Unity MVC+C Menu](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/01.png)

This will open the MVC+C Window, place it wherever you want on your Unity interface.

![Unity MVC+C Window](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/02.png)

Now you need to initialize your project by creating your projects MVCApplication file.
A MVCApplication is the static class that will provide Controllers and is the place you can add your global application data if needed.
It will create a ~YourProjectName~MVCApplication.cs file at _Project/Application/Scripts

If you want to fill your folder structure, you can select which folders you want to create at this time too.

Then just create your project MVC+C Project.

Great, you now have created a whole MVC+C project.

![Proejct folder structure](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/03.png)

From now on the MVC+C window will be where you start all the code for any module.


## Modules? What is it?

I've created my project, now what?

Well, now you are gona start your very first module.

A module is essencially a feature your project might have. Every feature should be a module, meaning it is designed to keep its dependencies contained on its own, making sure deleting, disabling or moving your feature around won't brake your project in any way, and if it does, it will be a very easy to spot problem. From now on, your project can grow as a collection of many pretty managable small modules, instead of a land of pain and dispair that many of us have witnessed in your life as dame developers.

Creating your first module is pretty simple, for your first module the MVC+C Code Generator will ask you the name and namespace of the module.
Just go ahead and create your very first module.

![Module creation window](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/04.png)

That's all! Your module is ready.
You can inspect your Modules folder and see how the structure of a module looks like.

Note: There's a known issue for now where for the first module, it won't recognize _Project/Modules folder as the module folder, it that is your case, please select this path manually. This will be fixed soon.

## MVC+C Window

After you have a project initialized with the proper files and structure and at least one module, whenever you come back to the MVC+C window, this is how it is gonna look like:

![Unity MVC+C Window](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/05.png)

It is VERY important to note, this is where you create your classes from now on, since the code generator will take care to link all dependencies internally and generate the proper code. If you have a specific type of class you want to ask to be included, please feel free to do so.

Let's take a look at that the tabs in there are for.

### Module Wizard

This is the tab in which you will create new modules.

### Code Generator

The main part of the MVC+C system, here you are going to create every file inside a module.

If you take a look at it, you will notice that you can select the module you are working on in a dropdown. No need to worry about folders or anything like that, the MVC+C has a standar folder structure and your file will be created inside your module on the righr place for you. Soon you will have more details about it in the wiki of this project as well.

![Unity MVC+C Modules dropdown](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/08.png)

Right below it, you will find the Base File Name input field.
There you can type the name of your feature, that will then be automatically composed with the type of file you will be creating, like, if you file is Player, then you are creating a MVCComponent, you will have the button Create PlayerMVCComponent that will create a file called PlayerMVCComponent.cs inside the module's Scripts/Components folder and so on.

![Unity MVC+C Inspector](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/09.png)

Here are the tabs inside this section and what they will contain. Keep in mind that every class in this section will be found inside your module's Script folder

#### Controllers/Views

- Create View/Controller
- Create View
- Create Controller

![Unity MVC+C Creation tab](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/10.png)

#### MVC Components

- Create MVCComponent
- Create MVCComponentGroup

![Unity MVC+C Creation tab](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/11.png)

#### Models

- Create Loader/Solver/Container
- Create Container
- Create Loader
- Create Solver

![Unity MVC+C Creation tab](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/12.png)

#### UnityComponent

- Create UnityComponent

![Unity MVC+C Creation tab](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/13.png)

#### Other

- Create Interface
- Create Enum
- Create ScriptableObject

![Unity MVC+C Creation tab](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/14.png)

### Inspector

The Inspector is a helper tool that will analyse the code's health.
It inspects the MVC+C classes to find possible wrong declarations or improper dependencies on classes fields.
If any problem is detected, it will show it as a warning, this may or may not be what you intented, it is not up to the MVC+C to say that, but it is going to try to help you to find blindspots.

![Unity MVC+C Inspector](https://github.com/AlbertoVosgerau/UnityMVC-C_UnityProject/blob/main/Screenshots/07.png)

### Help

This section is probably gonna become a separate window, it will just contain guides and help, probably, if needed and helpful.

# Roadmap

- [ ] Add assets module (modules that dedicated to assets and prefabs, no code involved)
- [ ] Add help window
- [ ] Add the module's location in the project in the MVC+C Generator window
- [ ] Highlight the created folder or file on Unity's Project tab after creation
- [ ] Refactor EditorWindow code (divide in smaller classes)
- [ ] Refactor Code Generator (Right now it works with no known issue, but it can be generic and more elegant)
- [ ] Develop upgrade feature and/or workflow (for upgrading MVC+C versions on running projects)
- [ ] Create a inheritance Inspector to help visualize hierarchy structure of the MVC+C modules
- [ ] Add more types of Unity Classes to the code generator if needed
- [ ] Make a sample and template project to showcade use cases
- [ ] Add settings window if needed
- [X] Create a version of the system that runs without AssemblyDefinition, since some projects might rely on code that is not ready for it
- [ ] Finish documentation
- [ ] Create tutorials
- [ ] Fill Wiki with infos and documentation
