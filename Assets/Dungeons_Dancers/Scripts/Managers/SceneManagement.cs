using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour {
    
    static string lastScene;
    static string currentScene;

    public static void ChangeScene(string sceneName)
    {
        lastScene = currentScene;
        currentScene = sceneName;
        SceneManager.LoadScene(currentScene);
    }


    public static void LoadLastScene()
    {
        string last = lastScene;
        lastScene = currentScene;
        currentScene = last;
        SceneManager.LoadScene(currentScene);
    }
}
