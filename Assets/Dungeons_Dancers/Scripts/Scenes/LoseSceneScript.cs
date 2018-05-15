using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseSceneScript : MonoBehaviour {

	public void SelectScene(int i){
		SceneManager.LoadScene(i);
	}
}
