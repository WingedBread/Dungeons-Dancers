using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class WinSceneScript : MonoBehaviour {
    [SerializeField]
    private Text highscore;

    [SerializeField]
    private GameObject sparkles;

    [SerializeField]
    private Ease initSparkleEase;

    private Vector3[] ogSparklePos = new Vector3[9];

    [SerializeField]
    private float duration = 1;
	// Use this for initialization
	void Start () {
        highscore.gameObject.SetActive(false);
        highscore.text = PlayerPrefs.GetInt("HighScoreLVL1").ToString();
        for (int i = 0; i < sparkles.transform.childCount; i++)
        {
            ogSparklePos[i] = sparkles.transform.GetChild(i).localPosition;
            sparkles.transform.GetChild(i).position = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), 0);
        }
        SparkleAnim();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        if(!DOTween.IsTweening("TweenSparkle")){
            highscore.gameObject.SetActive(true);

        }
	}


    void SparkleAnim(){
        for (int i = 0; i < sparkles.transform.childCount; i++){
            
            sparkles.transform.GetChild(i).DOLocalMove(ogSparklePos[i], duration).SetId("TweenSparkle");
        }
    }
}
