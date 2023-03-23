#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyFromTemplate : MonoBehaviour
{
    public GameObject template;
    //on validate get the object form the secene called "TemplateWaifu" and set to the template variable
    /*private void OnValidate()
    {
        template = GameObject.Find("TemplateWaifu");
    }*/

    public void CopyComponentsFromTemplate()
    {
        //set the animator to the template
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = template.GetComponent<Animator>().runtimeAnimatorController;
        
        
        // Get all components on the template object
        Component[] components = template.GetComponents<Component>();

        // Loop through all components and add them to the current object if they don't already exist
        foreach (Component component in components)
        {
            // Skip Transform component since it is already present on the current object
            if (component is Transform)
            {
                continue;
            }

            // Check if the component already exists on the current object
            Component existingComponent = GetComponent(component.GetType());
            if (existingComponent == null)
            {
                // Add the component to the current object
                Component newComponent = gameObject.AddComponent(component.GetType());
                UnityEditorInternal.ComponentUtility.CopyComponent(component);
                UnityEditorInternal.ComponentUtility.PasteComponentValues(newComponent);
            }
        }
        //remove this component
        DestroyImmediate(this);
    }


}
#endif  
