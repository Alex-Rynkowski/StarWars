using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour, IPointerClickHandler
{
    public string loadScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<SceneToLoad>().LoadScene(loadScene);
    }
}
