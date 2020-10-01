using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateScripts : MonoBehaviour
{
    [Tooltip("Insert scripts to activate / deactivate on game over")] [SerializeField]
    private GameObject[] scripts;

    public void Deactivate(IDeactivateScripts deactivateScripts)
    {
        if (deactivateScripts.DeactivateScripts())
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                foreach (MonoBehaviour script in scripts[i].GetComponents<MonoBehaviour>())
                {
                    script.enabled = false;
                }
            }    
        }
        else
        {
            for (int i = 0; i < scripts.Length; i++)
            {
                foreach (MonoBehaviour script in scripts[i].GetComponents<MonoBehaviour>())
                {
                    script.enabled = true;
                }
            }
        }
        
    }
}