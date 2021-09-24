# Three Year Degree Thesis Prototype
With mixed reality, it is possible to create experiences where users can interact with each other and with the environment in new ways.
These new interactions can be introduced by digitizing real entities and information present in the environment.
Having a digital representation of a device can allow you to observe its status and control it without having to interact directly with that device. As for the information, it is possible to have representations that highlight certain aspects, useful to the type of user who is exceeding that information.

This is a prototype developed for the case study of the thesis, in particular to demonstrate in a concrete way the potential of mixed reality and HoloLens 2.
The prototype consists of a client application and a service.

## Used technologies
### Client
- Unity 2020.3.14f1 LTS with OpenXR
- MRTK 2.7
- Vuforia 10.1.4
- Visual Studio 2019 was used to develop the C# scripts in the application.
### Service
- Node.js

## How it works

At runtime, the prototype connects to the service on the LAN and downloads the Json file that is responsible for associating the name of the prefab to the marker. Subsequently, the relative marker images are also downloaded and reported in the Json file.
In this case the Json file contains an entry like this: "marker1": "heart".

At this point the marker object is instantiated, so when you look at it it can be detected correctly.
When the marker is recognized by the prototype, a button is created that allows you to instantiate the associated prefab.
Once the holograms have been instantiated, the user is free to position the holograms within the environment as he prefers.

![ECG Hologram](/images/holograms.jpg)