Get the app if you havent already! 
https://waifuverse.itch.io/aigf
https://www.oculus.com/experiences/quest/8549351031801854/
Our website for ai waifus is at [https://www.waifuverse.org(https://www.waifuverse.org)

[DOWNLOAD HERE(https://github.com/Waifuverse/WaifuSDK/archive/refs/heads/main.zip)]

Guide to Creating a Waifu Model using Unity and Blender

This guide provides step-by-step instructions on how to create a Waifu model using Unity and Blender. It is broken down into sections for easy navigation.

**Setup**

1. Download and Install **Unity Hub** from the Unity website: [https://unity.com/download](https://unity.com/download)
2. Using **Unity Hub** Install Unity **2021.3.21f1**
![Unity Install](/DocImg/InstallEditor.png)
3. Install the packages for **Android, Windows, Mac, and iOS.**![unityInstallOptions](/DocImg/addmodules.png)
![unityInstallSelection](/DocImg/AddModulesPackages.png)
4. Download and Install **Blender** from the Blender website: [https://www.blender.org/download/](https://www.blender.org/download/)
5. Download the **Cats Blender** plugin from the Github repository: [https://github.com/absolute-quantum/cats-blender-plugin](https://github.com/absolute-quantum/cats-blender-plugin)
6. Download or clone the **WaifuSDK** from the Github repository: [https://github.com/Waifuverse/WaifuSDK](https://github.com/Waifuverse/WaifuSDK)

**Formatting a MMD Model for the Waifuverse**

1. Install the Cats plugin in Blender by going to **Edit -\> Preferences -\> Addons -\> Install Addons** and finding the Cats plugin.
2. **Checkbox** the Cats plugin to turn on
[CatsPlugin](/DocImg/CatsCheckbox.png)
3. Open the Cats plugin from the right sidebar.
4. Import the **MMD model** by selecting the model file. For this guide, we will be using a classic MMD model: [https://www.deviantart.com/thenikamiku/art/Miku1052C-Re0710-MMD-dl-680363108](https://www.deviantart.com/thenikamiku/art/Miku1052C-Re0710-MMD-dl-680363108)
5. In the Cats plugin, go to Settings and Updates and check "Embed Textures on Export".

**Formatting a VRM Model in blenderfor the Waifuverse**

1. Todo: its basically the same as a MMD model. However there are some issues with textures that arnt square. I believe this can be easily resolved in unity with a script. Further testing required

**Importing the Model into WaifuSDK/Unity**

1. Unzip the WaifuSDK folder.
2. Open Unity Hub and select the folder you just unzipped. Open the Unity project

![OpenProject](/DocImg/OpenProject.png)

1. Go to the folder "Custom Waifu"
2. Make a folder for your Waifu.
3. Drag a .fbx file in
4. Activate read write

![ModelReadWrite](/DocImg/ModelRW.png)

1. Click on the .fbx file and go to the Rig tab. Change it to **humanoid** and click apply. Make sure that avatar is created.
 ![](/DocImg/ModelRig.png)
2. Go to the Materials tab and change the location to "Use extracted materials (Legacy)".

![](/DocImg/ModelMat.png)

1. Add your FBX to the scene
2. Add the "Copy Components Object".
3. Drag the TemplateWaifu into the object.
4. Copy components.
5. Press play and test
 Press space to play the test audio.
6. Change the **Waifu Info** to what you want.
7. Do any other changes you want, materials, springbones, dps, ect

Make any adjustments you want, such as changing shaders or adding dynamic bones, magica cloth, or DPS. Please see the list of software that is able to be loaded. You can also suggest new software for the next build in doscrd

Todo: VRM -\> Blender -\> fbx

**Building and Exporting**

1. Drag your model you working on form the toolbar on the left to your waifu folder
2. Open Toolbar Waifuverse -\> Waifu Export toolbar.

![](/DocImg/WaifuverseToolbar.png)

1. Click **Open Addressable Build Grops Window**
2. Drag Everything form your folder into the **default** build group.
3. Find your .prefab and label it Waifu

![](/DocImg/AddressableAssets.png)

1. Build for each platform
2. Click Screenshot button
   Note, you can change the screenshot image, eg take one yourself. But it must be named the exact same.
3. Once all buttons are green, go to the build folder, and zip all of them.

**Mod.io Upload and Testing**

1. To test, go to MOD.IO and upload.
2. Zip all three of our assets.
3. Log into Mod.IO or create a new account.
4. Go to the Waifuverse Mod.IO.
5. Make a mod for your creation.
6. Fill out the page and take a quick screenshot.
7. Subscribe to your mod.
8. Open the app and sign in if you haven't already.

**Making a Local/Private Mod or Quickload for Testing**

1. To make a local/private mod or quickload for testing, drag the .zip file into the persistent data path below/

C:\Users\*{your username here}*\AppData\LocalLow\Waifuverse\AI_Waifu\mod.io\04680\data\mods
![](/DocImg/screenShotPersistant.png)

Todo: make a button that shows the persistent path.