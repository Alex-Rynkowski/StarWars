using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToLoad : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
