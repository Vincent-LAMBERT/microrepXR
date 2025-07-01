# microrepXR

A toolkit to manage representations of microgestures in Augmented Reality

Create a Unity Project with Universal 3D Sample template (or install Universal render pipeline)
Quit Unity
Download the MixedRealityFeatureTool (url: )
Select your project folder in the feature tool
Install :
- Platform Support → Mixed Reality OpenXR Plugin
- MRTK3 → MRTK Input 
- MRTK3 → MRTK UX Components
- MRTK3 → MRTK Core Definitions
- MRTK3 → MRTK UX Core Scripts
- MRTK3 → MRTK Spatial Manipulation 
Hit Validate and ensure that "No validation issue were detected"
You can then confirm the import and exit the feature tool
Re-open the Unity project

Now we need to setup MRTK3 and the XR PLug-in management. 

The targetted environments for our case are the Standalone (Windows, Max, Linux) and the Universal Windows Platform (UWP), used by the Hololens 2 for example.
Open the Project Settings (Edit > Project Settings). Select the MRTK3 option in the left panel and assign the default MRTK profile. Ensure that the MRTK Hands Aggregator Subsystem, the Subsystem for OpenXR Hands API and the Subsytem for Hand Synthesis are all activated for your targetted environments (Standalone and UWP).
Select the XR PLug-in Management option in the left panel and activate the OpenXR Plug-in for your targetted environments (Standalone and UWP). Make sure that the Windows Mixed Reality feature group under Standalone and the Microsoft HoloLens feature group under UWP are enabled.
Navigate to the OpenXR option  in the left panel (children to the XR PLug-in Management option) and add 3 interaction profiles in the dedicated list for your targetted environments (Standalone and UWP): 
    Eye Gaze Interaction Profile
    Microsoft Hand Interaction Profile
    Microsoft Motion Controller Profile


Select All Prefabs in the Project tab and search for "MRTK"
In the available prefabs, drag and drop "MRTKInputSimulator" and "MRTK XR Rig" then delete the Main Camera which is not needed because already included in the MRTK XR Rig prefab
Open the Package Manager (Window > Package Manager)
Hit the top-left "+" button and select "Add a package from disk" then browse to the folder containing the µRepXR package. Select the "package.json" to install the package

In the Project tab, you can see that a µRepXR folder has appeared.
Check that the Material file in µRepXR > PrefabResources > Materials is correctly imported. The shader should be "Universal Redner Pipeline/Complex Lit". If the material appears pink with an "InternalShaderError", please repair your installation of the Universal Render Pipeline (com.unity.render-pipelines.universal)
Otherwise, you can create an empty object in you scene, and drag and drop the MicroRepToolkit script (in the µRepXR > Scripts folder) to the component part of the inspector for you new object
This script is a simple Representation manager that will be usefull to quickly test that the representations are correctly imported.
In the Representations property of your object in the Inspector, you can add a new representation using the '+' button. Then, you can drag and drop the prefab of the representation (in the µRepXR > Prefabs > Representations folder) you want to display instead of the 'None (Game Object)' message. For testing purposes, you can select the AandB_Tap representation for example.
Hit the play button to run the simulation. When it is running, hold the spacebar to see a synthetic hand with the representation tied to it. The representation may appear blue for a few dozens of seconds. It is only the time for the Universal Render Pipeline to load the shader correctly the first time.

Congratulations ! You can now play with this package as you please.

Feature tool:

Unity: 
Universal Render Pipeline (com.unity.render-pipelines.universal) tested with @14.0.12