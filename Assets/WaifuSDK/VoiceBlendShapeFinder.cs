using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.VisualScripting;

public class VoiceBlendShapeFinder : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    private List<string> visimeList = new List<string>{"SIL", "PP", "FF", "TH", "DD", "KK", "CH", "SS", "NN", "RR", "AA", "E", "IH", "OH", "OU"};
    private List<string> MMDvisemes = new List<string>
    {
        "休止",  // Kyūshi (Neutral)
        "あい",  // AI (Blink)
        "あ",    // A (Ahh)
        "い",    // I (Ee)
        "う",    // U (Ooh)
        "え",    // E (Ay)
        "お",    // O (Oh)
        "ん",    // M (Mm) or N (Un)
    };
    private OVRLipSyncContextMorphTarget OVRls;
    private bool JapaneseMmdVisimes = false;
    public bool SetToOvrlipsync = true;
    
    //you will need to set your model to have read/write enabled (the .fbx)
    private bool addSilence = false;

    private void Start()
    {
        //get the OVRls
        OVRls = GetComponent<OVRLipSyncContextMorphTarget>();
        
        
    }
    private void OnValidate()
    {
        //get the audio source adn set the play on awake to false
        //GetComponent<AudioSource>().playOnAwake = false;
        
        if (skinnedMeshRenderer == null)
        {
            // Find the SkinnedMeshRenderer component in the children of this GameObject
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
            
        }
        //OVRls = GetComponent<OVRLipSyncContextMorphTarget>();
        if(OVRls == null)
        {
            OVRls = GetComponent<OVRLipSyncContextMorphTarget>();
            //set the skinned mesh renderer to the OVRls
            OVRls.skinnedMeshRenderer = skinnedMeshRenderer;
            OVRls.enableVisemeTestKeys = false;
            /*OVRls.smoothAmount = 51;*/
        }
        // if neither null then FindMatchingBlendShapes
        if (skinnedMeshRenderer != null && OVRls != null)
        {
            FindMatchingBlendShapes();
            /*FindMatchingBlendShapes();
            Destroy(this);*/
        }
        
    }
    //on gui find the skin meshrender in children if public SkinnedMeshRenderer skinnedMeshRenderer is null

    
    //

    public void FindMatchingBlendShapes()
    {
        OVRls = GetComponent<OVRLipSyncContextMorphTarget>();
        OVRls.skinnedMeshRenderer = skinnedMeshRenderer;
        if (addSilence)
        {
            //make
            MakeEmptyBlendShape();
        }
        
        List<int> matchingIndices = new List<int>();
        // if JapaneseMmdVisimes = false, use the visimeList else use mmd
        List<string> visimeListSearch = JapaneseMmdVisimes ? MMDvisemes : visimeList;
        
        foreach (string viseme in visimeListSearch)
        {
            int blendShapeIndex = -1;
            for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
            {
                string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
                if (blendShapeName.ToLower().Split('_', '.').Contains(viseme.ToLower()))
                {
                    blendShapeIndex = i;
                    matchingIndices.Add(blendShapeIndex);
                    break;
                }
            }
            if (blendShapeIndex >= 0)
            {
                Debug.Log($"Found blend shape {viseme} ({skinnedMeshRenderer.sharedMesh.GetBlendShapeName(blendShapeIndex)}) at index {blendShapeIndex}");

                // Do something with the blend shape, e.g. animate it based on audio input
            }
            else
            {
                Debug.Log($"No blend shape found for {viseme}");
                // if the blend shape is sil then run the make empty blendshape function and add it to the top of list
                
            }
        }
        //rpint hte list
        Debug.Log(string.Join(", ", matchingIndices.Select(x => x.ToString()).ToArray()));
        if (SetToOvrlipsync)
        {
            OVRls.visemeToBlendTargets = matchingIndices.ToArray();
        }
        
        // if
        
    }
    void MakeEmptyBlendShape()
    {
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        Debug.Log("Making empty blendshape");

        // Check if the blendshape already exists
        int blendShapeIndex = -1;
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            string blendShapeName = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            if (blendShapeName.ToLower().Split('_', '.').Contains("script_SIL".ToLower()))
            {
                blendShapeIndex = i;
                break;
            }
        }

        // If the blendshape doesn't exist, create it with no vertices
        if (blendShapeIndex == -1)
        {
            skinnedMeshRenderer.sharedMesh.AddBlendShapeFrame("script_SIL", 100f, new Vector3[0], new Vector3[0], new Vector3[0]);
            blendShapeIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("script_SIL");
        }

        // Set the blendshape weight to zero
        skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, 0f);
        
    }
    
    

    

    public void FindMatchingBlendShapesButton()
    {
        FindMatchingBlendShapes();
        DestroyImmediate(this);
        
        
    }
    //make empty blendshape
    
}
