
#[View the video tutorial playlist](https://www.youtube.com/watch?v=cT1kuoDLScY&list=PL7SY0P6poZnDPtJqm1ta_PWC4e1ofU7q_)

## [**DOWNLOAD THE WAIFU SDK HERE**](https://github.com/Waifuverse/WaifuSDK/archive/refs/heads/main.zip)


# Get the app if you havent already! 

https://waifuverse.itch.io/aigf

https://www.oculus.com/experiences/quest/8549351031801854/


 [Our website for ai waifus.](https://www.waifuverse.org)

[The Waifuverse mod.IO](https://mod.io/g/waifuverse)



Guide to Creating a Waifu Model using Unity and Blender

This guide provides step-by-step instructions on how to create a Waifu model using Unity and Blender. It is broken down into sections for easy navigation.

# **Setup**

1. Download and Install **Unity Hub** from the Unity website: [https://unity.com/download](https://unity.com/download)
2. Using **Unity Hub** Install Unity **2021.3.21f1**
![Unity Install](/DocImg/InstallEditor.png)
3. Install the packages for **Android, Windows, Mac, and iOS.**![unityInstallOptions](/DocImg/addmodules.png)
![unityInstallSelection](/DocImg/AddModulesPackages.png)
4. Download and Install **Blender 2.9.1** from the Blender website: [https://download.blender.org/release/Blender2.91/](https://download.blender.org/release/Blender2.91/)
5. Download the **Cats Blender** plugin from the Github repository: [https://github.com/absolute-quantum/cats-blender-plugin](https://github.com/absolute-quantum/cats-blender-plugin)
6. Download or clone the **WaifuSDK** from the Github repository: [https://github.com/Waifuverse/WaifuSDK](https://github.com/Waifuverse/WaifuSDK) YOU ARE HERE

# **Formatting a MMD Model for the Waifuverse**
BE SURE TO USE BLENDER VERSION 2.9.1
1. Install the Cats plugin in Blender by going to **Edit -\> Preferences -\> Addons -\> Install Addons** and find the Cats plugin.
2. **Checkbox** the Cats plugin to turn on
![CatsPlugin](/DocImg/InstallCats.png)
3. Open the Cats plugin from the right sidebar.
4. Import the **MMD model** by selecting the model file. For this guide, we will be using a classic MMD model: [https://www.deviantart.com/thenikamiku/art/Miku1052C-Re0710-MMD-dl-680363108](https://www.deviantart.com/thenikamiku/art/Miku1052C-Re0710-MMD-dl-680363108)
5. In the Cats plugin, go to Settings and Updates and check "Embed Textures on Export".

# **Formatting a VRM Model in blenderfor the Waifuverse**

1. Todo: Adding VRM support and conversion script

# **Rigging unrigged models**

1. Humanid Rig https://docs.unity3d.com/560/Documentation/Manual/BlenderAndRigify.html

# **Importing the Model into WaifuSDK/Unity**

1. Unzip the WaifuSDK folder.
2. Open Unity Hub and select the folder you just unzipped. Open the Unity project

![OpenProject](/DocImg/OpenProject.png)

1. Open the scene "ModdingBasic" make sure you see the height ruler
Make a folder for your custom work. Ideally call the folder "Custom Waifus"
Make a sub folder for your Waifu.
Drag a .fbx file into your new subfolder.
Click on the fbx. And look at the model details in the inspector
5. Activate read write

![ModelReadWrite](/DocImg/ModelRW.png)

6. Click on the .fbx file and go to the Rig tab. Change it to **humanoid** and click apply. Make sure that avatar is created.
 ![Rig](/DocImg/ModelRig.png)
7. Go to the Materials tab and change the location to "Use External Materials (Legacy)".

![]Mat(/DocImg/ModelMat.png)

8. Add your FBX to the scene
9. Add the "Copy Components Object" by hitting hte add components button, and typing in "copy" or dragging it form the waifuSDK folder.
10. Drag the TemplateWaifu into the object, the template waifu is in the waifuSDK or in the hierarchy.
11. Hit the Copy components button
12. Press play and test

 Press space to play the test audio.
 The pre generated audio should play. THIS IS NOT WHAT YOUR WAIFU WILL SOUND LIKE.
13. Change the **Waifu Info** to what you want.
14. todo: Adjust the default voice instrucitons
14. Do any other changes you want, materials, dynamic bones, springbones, dps, ect

Make any adjustments you want, such as changing shaders or adding dynamic bones, magica cloth, or DPS. Please see the list of software that is able to be loaded. You can also suggest new software for the next build in doscrd

Todo: VRM Support from within unity.

# **Building and Exporting**

1. Drag your model you working on form the Hirarchy window on the left to your CustomWaifu folder
2. Open Toolbar Waifuverse -\> Waifu Export toolbar.

![](/DocImg/WaifuverseToolbar.png)

2. Click **Open Addressable Build Grops Window**
![](/DocImg/OpenAdd.png)
3. Drag Everything form your folder into the **default** build group.
![](/DocImg/DragIntoAddress.png)
4. Find your .prefab and label it Waifu

![](/DocImg/AddressableAssets.png)

1. Build for each platform, all of the red buttons need to be green
2. Click Screenshot button to take a screenshot of the Scene window
3. Once all buttons are green, go to the build folder, and zip all of them.

# **Mod.io Upload and Testing**

[The Waifuverse mod.IO](https://mod.io/g/waifuverse)

1. Go to MOD.IO and upload.
2. Zip all of the builds.
3. Log into Mod.IO or create a new account.
4. Go to the Waifuverse Mod.IO.
5. Make a mod for your creation.
6. Fill out the page. Summary, Screenshot Ect
7. Summary should be hte source for the model, what features it has, ect.
7. Subscribe to your mod.
Public testing 
8. Open the desktop app
9. On PC, hit Tab(capslock in older versions) to bring up the Mod.IO browser.
   todo:imrpove the UI 
10. Login to the mod.io, put in code form your email
11. Click collections to see your downloads.
12. Once all are downloaded hit close mod.
13. Press Enter to bring up the mod selection screen.


# **Making a Local/Private Mod or Loading and Testing**

1. To make a local/private mod or quickload for testing, drag the .zip file into the persistent data path below/

C:\Users\**{your username here}**\AppData\LocalLow\Waifuverse\AI_Waifu\mod.io\04680\data\mods
![](/DocImg/screenShotPersistant.png)

Todo: make a button that shows the persistent path.