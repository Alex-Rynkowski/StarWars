using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    public void PlusYAxis()
    {
        Camera.main.transform.position = new Vector3(0, 10, 0);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
    }
    public void MinusYAxis()
    {
        Camera.main.transform.position = new Vector3(0, -10, 0);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(-90,0,0));
    }
    public void PlusXAxis()
    {
        Camera.main.transform.position = new Vector3(10, 0, 0);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0,-90,0));
    }
    public void MinusXAxis()
    {
        Camera.main.transform.position = new Vector3(-10, 0, 0);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
    }
    
    public void PlusZAxis()
    {
        Camera.main.transform.position = new Vector3(0, 0, 10);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
    
    public void MinusZAxis()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }
}
