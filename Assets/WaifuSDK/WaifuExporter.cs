#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEditor.AddressableAssets;

public class WaifuExporter : EditorWindow 
{
    private string exportPath;
    string WaifuName = "WAIFU NAME HERE";

    Color originalColor;
    Color greenColor = Color.green;
    Color redColor = Color.red;

    bool windowsFolderExists;
    bool androidFolderExists;
    bool iosFolderExists;
    bool macFolderExists;

    [MenuItem("Waifuverse/Waifu Exporter")]
    public static void ShowMapWindow() {
        GetWindow<WaifuExporter>("Waifu Exporter");
    }
    void OnGUI() {
        originalColor = GUI.color;
        exportPath = Application.dataPath + "/../Builds";
        windowsFolderExists = Directory.Exists(exportPath + "/StandaloneWindows");
        androidFolderExists = Directory.Exists(exportPath + "/Android");
        iosFolderExists = Directory.Exists(exportPath + "/IOS");
        //mac
        macFolderExists = Directory.Exists(exportPath + "/StandaloneOSX");

        //provide button to open the export path
        
        if (GUILayout.Button("Open Addressables Groups Window", GUILayout.Height(40)))
        {
            OpenAddressablesGroupsWindow();
        }
        

        GUILayout.Label("Waifu Exporter", EditorStyles.boldLabel);
        // set the window size
        this.minSize = new Vector2(300, 500);
        //this.maxSize = new Vector2(300, 100);
        //set anti-aliasing
        EditorGUIUtility.labelWidth = 100;
        EditorGUIUtility.fieldWidth = 100;

        // Check for waifu name and illegal characters
        bool hasWaifuName = !string.IsNullOrEmpty(WaifuName);
        bool hasIllegalCharacters = !System.Text.RegularExpressions.Regex.IsMatch(WaifuName, "^[a-zA-Z0-9 ]*$");

        // Show help box if waifu name is empty or has illegal characters
        if (!hasWaifuName || hasIllegalCharacters) {
            string message = "";
            /*if (!hasWaifuName) {
                message = "Please enter a waifu name.";
                 EditorGUILayout.HelpBox(message, MessageType.Error);
            } 
            else */
            if (hasIllegalCharacters) {
                message = "Waifu name contains illegal characters. Only letters, numbers, and spaces are allowed.";
                EditorGUILayout.HelpBox(message, MessageType.Error);
            }
           
        }

        WaifuName = EditorGUILayout.TextField("Waifu Name", WaifuName);
        //describe the waifu name
        EditorGUILayout.LabelField("Build for each of the platforms and then zip the builds. ", GUILayout.Height(40));
        //CheckWaifuLabel();
        // if !CheckWaifuLabel then return
        if (!CheckWaifuLabel())
        {
            // Open Addressables Groups Window and drag all your waifu assets into the Default group.
            //EditorGUILayout.HelpBox("Open Addressables Groups Window and drag all your waifu assets into the Default group.", MessageType.Error);
            return;
        }
        //cear all files button, if the folderis empty then disable the button
        /*if (windowsFolderExists || androidFolderExists || iosFolderExists || macFolderExists)
        {
            GUI.color = Color.yellow;
            if (GUILayout.Button("Clear All Builds", GUILayout.Height(40)))
            {
                Clear();
            }
        }
        else
        {
            GUI.enabled = false;
            GUI.color = Color.yellow;
            if (GUILayout.Button("Clear All Builds", GUILayout.Height(40)))
            {
                Clear();
            }
            GUI.enabled = true;
        }*/
       


        GUI.color = windowsFolderExists ? greenColor : redColor;
        if (GUILayout.Button("Build for Windows", GUILayout.Height(40)))
        {
            ExportWindows();
        }


        GUI.color = androidFolderExists ? greenColor : redColor;
        if (GUILayout.Button("Build for Android", GUILayout.Height(40)))
        {
            ExportAndroid();
        }
        //ios export
        GUI.color = iosFolderExists ? greenColor : redColor;
        if (GUILayout.Button("Build for IOS", GUILayout.Height(40)))
        {
            ExportIOS();
        }
        GUI.color = macFolderExists ? greenColor : redColor;
        if (GUILayout.Button("Build for Mac", GUILayout.Height(40)))
        {
            ExportMac();
        }
        //CheckForSreenShot red if no green if yes
        GUI.color = CheckForSreenShot() ? greenColor : redColor;
        if (GUILayout.Button("Take Screenshot of Scene View", GUILayout.Height(40)))
        {
            TakeScreenshot169();
            TakeScreenshot11();
        }
        static void Clear()
        {
            string exportPath = Application.dataPath + "/../Builds";
            if (EditorUtility.DisplayDialog("Clear Exported Files", "This action will clear all files in the export folder:\n" + exportPath + "\nAre you sure you want to do this?", "Yes", "No"))
            {
                string[] filePaths = Directory.GetFiles(exportPath);
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }
                Debug.Log("Cleared all exported files at " + exportPath);
            }
        }
        
        
        

        
        GUI.enabled = true;
        GUI.color = originalColor;
        /*bool allDirectoriesExist = windowsFolderExists && androidFolderExists && iosFolderExists && macFolderExists;
        GUI.enabled = allDirectoriesExist;

        GUI.color = originalColor;
        if (GUILayout.Button("Zip Builds", GUILayout.Height(40)))
        {
            // Create the zip file name using the waifu name and current time
            ZipBuilds();
        }*/
        
        if (GUILayout.Button("Open Export Path", GUILayout.Height(40)))
        {
            System.Diagnostics.Process.Start(exportPath);
        }
        // open https://mod.io/g/waifuverse
        if (GUILayout.Button("Open Waifuverse Mod.IO", GUILayout.Height(40)))
        {
            Application.OpenURL("https://mod.io/g/waifuverse");
        }
        //Screenshot button
        

        GUI.enabled = true;
        GUI.color = originalColor;
        CheckZip();
        
        //check to see if there is a .zip file in the export path if htere is say , "you may now upload to Mod.IO"
        
    }
    
    //check if a .png exists in the folder check for screenshot return ture or false
    private bool CheckForSreenShot()
    {
        string[] filePaths = Directory.GetFiles(exportPath);
        foreach (string filePath in filePaths)
        {
            if (filePath.Contains(".png"))
            {
                return true;
            }
        }
        return false;
    }
    private void TakeScreenshot169()
    {
        //if no waifu name then change to WaifuName
        
        string fileName = "Cover169.png";
        
        string filePath = exportPath+ "/" + fileName;

        // Get the current SceneView
        SceneView sceneView = SceneView.lastActiveSceneView;

        // Set the SceneView camera to orthographic mode
        sceneView.orthographic = true;

        // Get the width and height for the screenshot, based on a 16:9 aspect ratio
        float aspectRatio = 16f / 9f;
        int width = (int)sceneView.position.width;
        int height = Mathf.RoundToInt(width / aspectRatio);

        // Create a new RenderTexture with the same resolution as the SceneView
        RenderTexture rt = new RenderTexture(width, height, 24);
        sceneView.camera.targetTexture = rt;

        // Render the SceneView to the RenderTexture
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        sceneView.camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        sceneView.camera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);

        // Save the screenshot to a file
        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);
        Debug.Log("Screenshot saved to " + filePath);

        // Open the file in the default image viewer
        //System.Diagnostics.Process.Start(filePath);
    }
    private void TakeScreenshot11()
    {
        //if no waifu name then change to WaifuName
        
        string fileName = "Cover11.png";
        
        string filePath = exportPath+ "/" + fileName;

        // Get the current SceneView
        SceneView sceneView = SceneView.lastActiveSceneView;

        // Set the SceneView camera to orthographic mode
        sceneView.orthographic = true;

        // Get the width and height for the screenshot, based on a 16:9 aspect ratio
        float aspectRatio = 10f / 10f;
        int width = (int)sceneView.position.width;
        int height = Mathf.RoundToInt(width / aspectRatio);

        // Create a new RenderTexture with the same resolution as the SceneView
        RenderTexture rt = new RenderTexture(width, height, 24);
        sceneView.camera.targetTexture = rt;

        // Render the SceneView to the RenderTexture
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        sceneView.camera.Render();
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        sceneView.camera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);

        // Save the screenshot to a file
        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(filePath, bytes);
        Debug.Log("Screenshot saved to " + filePath);

        // Open the file in the default image viewer
        System.Diagnostics.Process.Start(filePath);
    }

    void ZipBuilds()
    {
        try
        {
            string zipFileName = WaifuName + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
            using (FileStream fs = new FileStream(exportPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                Zip(zipFileName, exportPath);
                Selection.activeObject = null;
                System.Diagnostics.Process.Start(exportPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error exporting waifu: " + e.Message);
            EditorUtility.DisplayDialog("Export Error", "An error occurred while exporting the waifu:\n\n" + e.Message, "OK");
        }


    }
    public void CheckZip()
    {
        string[] files = Directory.GetFiles(exportPath, "*.zip");

        bool hasUnfinishedBuild = false;
        foreach (string filePath in files)
        {
            if (Path.GetExtension(filePath) == ".zip.zip")
            {
                hasUnfinishedBuild = true;
                break; // no need to continue checking the rest of the files
            }
        }

        if (hasUnfinishedBuild)
        {
            EditorGUILayout.HelpBox("Error: Unfinished build detected (file extension '.zip.zip You may need to delete the zip.zip and restart computer')", MessageType.Error);
        }
        else if (files.Length > 0)
        {
            EditorGUILayout.HelpBox("You may now upload the zip file to Mod.IO", MessageType.None);
        }
        else
        {
            EditorGUILayout.HelpBox("No zip files found in the export path", MessageType.Info);
        }
    }


    public static void BuildAddressables(object o = null)
    {
        AddressableAssetSettings.CleanPlayerContent();
        AddressableAssetSettings.BuildPlayerContent();
        Debug.Log("Building Addressables!!! DONE");
    }

    void ExportWindows() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows64;
        BuildAddressables();
    }

    void ExportAndroid() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        BuildAddressables();
    }
    //ios export
    void ExportIOS() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
        BuildAddressables();
    }
    //mac export
    void ExportMac() {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        BuildAddressables();
    }
    
    
    
    
    

    public void Zip(string fileName, string directoryPath)
    {
        string zipPath = Path.Combine(directoryPath, $"{fileName}.zip"); // Set the name and path of the zip file to be created
        ZipFile.CreateFromDirectory(directoryPath, zipPath); // Create the zip archive from the given directory
        Debug.Log($"Zip archive created at {zipPath}");
    }
    private void OpenAddressablesGroupsWindow()
    {
        //AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        //Selection.activeObject = settings;
        //EditorGUIUtility.PingObject(settings);
        EditorApplication.ExecuteMenuItem("Window/Asset Management/Addressables/Groups");
    }
    private bool CheckWaifuLabel()
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
        AddressableAssetGroup defaultGroup = settings.FindGroup("Default Local Group");
        //print all group names
        foreach (var group in settings.groups)
        {
            Debug.Log(group.name);
        }

        if (defaultGroup == null)
        {
            EditorGUILayout.HelpBox( "No Default group found.", MessageType.Error);
            return false;
        }
        // if there are no entries in the default group then say "please add assets to the groups"
        if (defaultGroup.entries.Count == 0)
        {
            EditorGUILayout.HelpBox("Please add assets to the default group.", MessageType.Error);
            return false;
        }
        

        foreach (var entry in defaultGroup.entries)
        {
            if (entry.labels.Contains("Waifu"))
            {
                Debug.Log(entry.labels);
                //check if hte object Labeld Waifu is a .prefab
                if (entry.address.Contains(".prefab"))
                {
                    
                    return true;
                }
                else
                {
                    EditorGUILayout.HelpBox("Waifu object is not a .prefab.", MessageType.Error);
                    return false;
                }
                return true;
            }
            //if no object is labeled Waifu
            
        }
        EditorGUILayout.HelpBox("No object labeled Waifu found.", MessageType.Error);

        return false;
    }
    
    
}

#endif
