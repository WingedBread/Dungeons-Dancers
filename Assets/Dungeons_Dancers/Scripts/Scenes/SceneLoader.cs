using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void SelectScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
