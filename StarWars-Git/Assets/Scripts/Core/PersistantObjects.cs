using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjects : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < FindObjectsOfType<PersistantObjects>().Length; i++)
        {
            if (i > 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
