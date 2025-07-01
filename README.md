<p align="center">
<h1 align="center">µRepXR</h1>
<h3 align="center">A Unity Package to Create Representation of Microgestures in Augmented Reality</h3>
</p>
<p align="center">
  <p align="center">
    <a href="https://vincent-lambert.eu/">Vincent Lambert</a><sup>1</sup>
    ·
    <a href="http://alixgoguey.fr/">Alix Goguey</a><sup>1</sup>
    ·
    <a href="https://malacria.com/">Sylvain Malacria</a><sup>2</sup>
    ·
    <a href="http://iihm.imag.fr/member/lnigay/">Laurence Nigay</a><sup>1</sup>
    <br>
    <sup>1</sup>Université Grenoble Alpes <sup>2</sup>Université Lille - INRIA
  </p>
</p>

---

This package allows you to attach 3D models to the user's hand to easily create representations of microgestures in Unity. This package makes it easier to create help interfaces for microgesture-based applications in Augmented Reality (AR). It is based on a few main concepts:

* **Microgestures** are quick and subtle finger movements that do not involve the wrist nor the arm. This package allows to create representations of microgestures for any type of microgesture.
* **Representations** visually explain how to perform microgestures by showing the moving finger, i.e. the *actuator*, the target, i.e. the *receiver*, and the trajectory of the microgesture. There are two types of representations: 
  * *Single-picture representations of microgestures* show one microgesture per hand shape. Multiple single-picture representations are typically used to illustrate a set of microgestures in a research paper.
  * *Simultaneous representations of microgestures* show multiple microgestures on the same hand shape.
* **Families** describe the consistent set of visual cues used to shape the representations of each microgesture. One family is made of a representation for each type of microgesture.

If you are not familiar with the words *mesh*, *prefab* or *material*, please refer to the [Unity glossary](https://docs.unity3d.com/Manual/Glossary.html).

## Installation

To install the µRepXR package, you first need to correctly setup a Unity project. In the following sections, we will present the different steps to install it, from the installation of Unity to the usage of the µRepXR package for a simple example.

#### 1. Create a Unity 2022.3.62f1 Project

For this project, we use the [Microsoft Mixed Reality Toolkit 3](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk3-overview/) (MRTK3). This project cannot run with the latest versions of Unity but only Unity 2021.3 LTS or Unity 2022.3 LTS. We chose to use Unity 2022.3.62f1, thus we recommend that you install this specific version to avoid unpleasant bugs.

Then, you can click on `New project` and select the `Universal 3D` core template as a base. This core comes with the Universal Render Pipeline (URP) which we use to render the 3D meshes of the representations. Finally, before going to the next step, close the project.

#### 2. Install MRTK features

The µRepXR package is based on MRTK3, so we need to install the MRTK3 features. To do so, download the [Mixed Reality Feature Tool](https://www.microsoft.com/en-us/download/details.aspx?id=102778) and open it. You can then select your project and install the following features:
- Platform Support → Mixed Reality OpenXR Plugin
- MRTK3 → MRTK Input 
- MRTK3 → MRTK UX Components
- MRTK3 → MRTK Core Definitions
- MRTK3 → MRTK UX Core Scripts
- MRTK3 → MRTK Spatial Manipulation 

Hit Validate and ensure that "No validation issue were detected". You can then confirm the import and exit the feature tool

#### 3. Setup MRTK3 in the Project Settings

Re-open the Unity project. As you can expect, we still need a little bit of tinkering to make everything work so open the `Project Settings` (`Edit > Project Settings`). Select the `MRTK3` option in the left panel and assign the default MRTK profile with the eponymous button. 

As you can notice, there are multiple tabs that allow you to specify a different behavior according to the targeted environment. Initially, we developped this package for the Hololens 2. For this reaseon, our targetted environments were the Standalone (Windows, Max, Linux) that we used to simulate the application in Unity and the Universal Windows Platform (UWP) used by the Hololens 2. 

Still in `MRTK3` option in the left panel, make sure that the `MRTK Hands Aggregator Subsystem`, the `Subsystem for OpenXR Hands API` and the `Subsytem for Hand Synthesis` are all activated for your targetted environments (Standalone and UWP).

#### 4. Setup OpenXR in the Project Settings

Select the `XR Plug-in Management` option in the left panel and activate the `OpenXR` plug-in for your targetted environments (Standalone and UWP). Make sure that the `Windows Mixed Reality feature group` under Standalone and the `Microsoft HoloLens feature group` under UWP are enabled.
Navigate to the `OpenXR` option  in the left panel (child to the `XR Plug-in Management` option) and add 3 interaction profiles in the dedicated list for your targetted environments (Standalone and UWP): 
- `Eye Gaze Interaction Profile`
- `Microsoft Hand Interaction Profile`
- `Microsoft Motion Controller Profile`

You can then close the `Project Settings` window.

#### 5. Setup the Unity scene

Select `All Prefabs` in the `Project` tab and search for "MRTK". In the available prefabs, drag and drop `MRTKInputSimulator` and `MRTK XR Rig` in the scene under the top-left `Hierarchy` tab. You can then delete the `Main Camera` which is not needed because already included in the `MRTK XR Rig` prefab.

#### 6. Install the µRepXR package

Open the `Package Manager` (`Window > Package Manager`). Hit the top-left `+` button and select `Add a package from disk` then browse to the folder containing the µRepXR package. Select the `package.json` file to install the package.

In the `Project` tab, you can see that a `µRepXR` folder has appeared.
Check that the `Material.mat` file in `µRepXR > PrefabResources > Materials` is correctly imported. The shader should be *Universal Render Pipeline/Complex Lit*. If the material appears pink with an *InternalShaderError*, please repair your installation of the *Universal Render Pipeline* (`com.unity.render-pipelines.universal`).

#### 7. Test the package

You can create an empty object in you scene. When you click on this object, you can see the `Inspector` tab in which you will be able to manipulate representations of microgestures using special scripts and their associated Graphical User Interfaces (GUI). Go to the `µRepXR > Scripts` folder in the `Project` tab and drag and drop the `MicroRepToolkit.cs` script in the Inspector of your new empty object.
As you can see, there is now a new component in which you can add multiple representations using the `+` button. Click on the `+` button to add a new representation. You should see a new line in the `Inspector` with a `None (Game Object)` asigned to it. 
This script is a simple Representation manager that will be usefull to quickly test that the representations are correctly imported.

Go to the `µRepXR > Prefabs > Representations` folder in the `Project` tab. As you can see, we let pre-made representations for everyone to easily manipulate and understand the project without taking too much space. Drag and drop the `AandB_Tap` prefab instead of the `None (Game Object)` box. You just selected the `AandB_Tap` representation as you base representation for the scene. 

Hit the play button to run the simulation. When it is running, hold the spacebar to see a synthetic hand with the representation tied to it. The representation may appear blue for a few tenths of a second. It is only the time for the *Universal Render Pipeline* to load the shader correctly the first time.

Congratulations ! You can now play with this package as you please.

## Creating your own representations

Even tough we included a few prefabs that you can use to create you own representations of microgestures, it is likely that you will want to create your own representations. To do so, you need 3 steps:

1. 3D modelling (in Blender)
2. Creating the visual cues prefabs
3. Creating the representations prefabs.

#### 1. 3D modelling (in Blender)

[Blender](free and open-source 3D computer graphics software tool ) is a tool that runs on basically any operating system (OS) and allows you to create 3D models that can later be imported in Unity. You can also directly create the 3D models in Unity but we recommend to use Blender because there are a lot of features that make it easier to create 3D models and the community is very active and helpful.

We used 3D modelled hands within the Blender files to accurately scale the visual cues of each family and created animations for the *dynamic* versions of each family. In Blender, you can use `modifiers` to easily create animations, e.g. growing arrows, shrinking balls, etc... Nevertheless, we **HIGHLY** recommend not to use them for animation because every modifier is **applied** when imported in Unity. Bascially, if you created a model whose mesh was modified during the animation, forget about it. This compatibility issue can create a lot of mess when importing animated models from Blender to Unity but if you only work with *static* representations this should not be a problem.

#### 2. Creating the visual cues prefabs

In Unity, select your Blender file (see `µRepXR > PrefabResources > BlenderModels` for the example file) and expand it by clicking on the right arrow in the `Project` tab. You can see all the elements in your Blender model. Drag and drop the Blender model as a prefab in your scene and separate each visual cue in independant prefabs. Ensure that the material are correctly configured and save them for later. You can delete them from the scene once you are done.

#### 3. Creating the representations prefabs

In Unity, create an empty object. Then, go to the `µRepXR > Scripts` folder in the `Project` tab and drag and drop the `Representation.cs` script in the `Inspector` of your new empty object. As you can see, there is a new GUI with two elements: a handedness and a list of `Artefacts`.

The handedness allows you to create a representation for the left OR the right hand. It is as simple as that. The list of `Artefacts` allows you to pick your previously created visual cue prefabs and associate them to a placeholder. Click on the `+` button and you will see a `None (Game Object)` box in which you can drag and drop a visual cue prefab. Then, you can decide if this visual cue should appear on a `Unique` finger, on `Joined` fingers or between two far `Away` fingers. Depending on the chosen configuration, the ChoiceBox widgets will slightly change to precise the exact location, e.g. the `Tip`of the `Index` finger. The behavior of the representation will also change. For a `Unique` finger, the representation will never disappear but for `Joined` fingers it will only be visible when the relevant fingers are joined. For the `Away` option, it will only be visible when the relevant fingers are far apart. 

With this `Representation.cs` script, you can thus create both representations for one microgestures or for multiple microgestures simultaneously.

When you are done, save your representation prefab and test it !