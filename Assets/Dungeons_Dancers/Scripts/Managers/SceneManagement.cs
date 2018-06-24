using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // we can use the SceneUnloaded delegate of scenemanager to listen for scenes that have been unloaded
    void OnEnable() { SceneManager.sceneUnloaded += SceneUnloadedMethod; }
    void OnDisable() { SceneManager.sceneUnloaded -= SceneUnloadedMethod; }

    int lastSceneIndex = -1;

    // looks a bit funky but the method signature must match the scenemanager delegate signature
    void SceneUnloadedMethod(Scene sceneNumber)
    {
        int sceneIndex = sceneNumber.buildIndex;
        // only want to update last scene unloaded if were not just reloading the current scene
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

    #region method2
    //static string lastScene;
    //static string currentScene;

    //public static void ChangeScene(string sceneName)
    //{
    //    lastScene = currentScene;
    //    currentScene = sceneName;
    //    SceneManager.LoadScene(currentScene);
    //}


    //public static void LoadLastScene()
    //{
    //    string last = lastScene;
    //    lastScene = currentScene;
    //    currentScene = last;
    //    SceneManager.LoadScene(currentScene);
    //}
    #endregion

}
