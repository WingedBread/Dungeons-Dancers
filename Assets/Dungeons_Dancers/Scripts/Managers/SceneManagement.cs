using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnEnable() { SceneManager.sceneUnloaded += SceneUnloadedMethod; }
    void OnDisable() { SceneManager.sceneUnloaded -= SceneUnloadedMethod; }

    int lastSceneIndex = -1;

    void SceneUnloadedMethod(Scene sceneNumber)
    {
        int sceneIndex = sceneNumber.buildIndex;

        if (lastSceneIndex != sceneIndex)
        {
            lastSceneIndex = sceneIndex;
            Debug.Log("unloaded scene is : " + lastSceneIndex);
        }
    }
    public int GetLastSceneNumber()
    {
        return lastSceneIndex;
    }
}
