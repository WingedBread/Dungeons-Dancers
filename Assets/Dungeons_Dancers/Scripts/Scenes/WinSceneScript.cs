using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class WinSceneScript : MonoBehaviour {
    
    [Header("Score Text")]
    [SerializeField]
    private TextMeshProUGUI highscore;
    [Header("Dungeon")]
    [SerializeField]
    private TextMeshProUGUI dungeonName;

    [Header("Rating Texts")]
    [SerializeField]
    private TextMeshProUGUI ratingText;
    [SerializeField]
    private TextMeshProUGUI ratingLetter;
    [SerializeField]
    private TextMeshProUGUI ratingCaption;

    [Header("Move Texts")]
    [SerializeField]
    private TextMeshProUGUI badText;
    [SerializeField]
    private TextMeshProUGUI goodText;
    [SerializeField]
    private TextMeshProUGUI greatText;
    [SerializeField]
    private TextMeshProUGUI perfectText;
    [SerializeField]
    private TextMeshProUGUI climaxText;

    [Header("Sparkle Anim")]
    [SerializeField]
    private GameObject sparkles;
    [SerializeField]
    private Ease initSparkleEase;

    private Vector3[] ogSparklePos = new Vector3[9];

    [SerializeField]
    private float sparkleAnimuration = 1;

    private float ratingPercentage;
    private int totalMoves;

    private SceneManagement sceneManagement;

    private string[] ratingLetterString = { "S+", "S", "A", "B", "C", "D", "E" };
    private string[] ratingLetterCaption = { "Dancing God", "Glorious Steps", "Fred Astaire", "Advanced", "Rookie", "Not bad", "Terrible Dancer" };

    // Use this for initialization
    private void Awake()
    {
        sceneManagement = GameObject.FindWithTag("SceneManagement").GetComponent<SceneManagement>();
    }
    void Start () {
        highscore.gameObject.SetActive(false);
        highscore.text = PlayerPrefs.GetInt("TotalScore").ToString();
        for (int i = 0; i < sparkles.transform.childCount; i++)
        {
            ogSparklePos[i] = sparkles.transform.GetChild(i).localPosition;
            sparkles.transform.GetChild(i).position = new Vector3(Random.Range(-200, 200), Random.Range(-200, 200), 0);
        }
        dungeonName.text = "Dungeon " + sceneManagement.GetLastSceneNumber().ToString();
        SparkleAnim();
        RatingBehaviour();

    }
	
	// Update is called once per frame
	void Update () {
        if(!DOTween.IsTweening("TweenSparkle"))
        {
            highscore.gameObject.SetActive(true);
        }
	}


    void SparkleAnim(){
        for (int i = 0; i < sparkles.transform.childCount; i++){
            
            sparkles.transform.GetChild(i).DOLocalMove(ogSparklePos[i], sparkleAnimuration).SetId("TweenSparkle").SetEase(initSparkleEase);
        }
    }
    public void SelectScene()
    {
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("NumGoodMoves", 0);
        PlayerPrefs.SetInt("NumGreatMoves", 0);
        PlayerPrefs.SetInt("NumPerfectMoves", 0);
        PlayerPrefs.SetInt("NumBadMoves", 0);
        PlayerPrefs.SetInt("MovesInClimax", 0);
        PlayerPrefs.SetInt("MovesAfterClimax", 0);
        ratingPercentage = 0;

        if (sceneManagement.GetLastSceneNumber() == SceneManager.GetActiveScene().buildIndex - 1) SceneManager.LoadScene(0);
        else SceneManager.LoadScene(sceneManagement.GetLastSceneNumber()+1);
    }

    void RatingBehaviour()
    {
        badText.text = PlayerPrefs.GetInt("NumBadMoves").ToString();
        goodText.text = PlayerPrefs.GetInt("NumGoodMoves").ToString();
        greatText.text = PlayerPrefs.GetInt("NumGreatMoves").ToString();
        perfectText.text = PlayerPrefs.GetInt("NumPerfectMoves").ToString();
        climaxText.text = PlayerPrefs.GetInt("MovesInClimax").ToString();
        totalMoves = PlayerPrefs.GetInt("NumGoodMoves") + PlayerPrefs.GetInt("NumGreatMoves") + PlayerPrefs.GetInt("NumPerfectMoves") + PlayerPrefs.GetInt("NumBadMoves");
        //Debug.Log("TotalMoves " + totalMoves);
        //Debug.Log("MovesAfterClimax " + PlayerPrefs.GetInt("MovesAfterClimax"));

        //Para asegurarse que nunca se divide entre 0 (dando error)
        if (PlayerPrefs.GetInt("MovesAfterClimax") == 0) PlayerPrefs.SetInt("MovesAfterClimax", 1);
        ratingPercentage = (((((PlayerPrefs.GetInt("NumGoodMoves") * 0.7f) + (PlayerPrefs.GetInt("NumGreatMoves") * 0.9f) + (PlayerPrefs.GetInt("NumPerfectMoves")) - (PlayerPrefs.GetInt("NumBadMoves") * 0.2f)) / totalMoves) * 100) * 0.85f) + (((PlayerPrefs.GetInt("MovesInClimax") / PlayerPrefs.GetInt("MovesAfterClimax")) * 100) * 0.15f);
        ratingText.text = ratingPercentage.ToString("0.00") + "%";


        if(ratingPercentage >= 0 && ratingPercentage < 21)
        {
            ratingLetter.text = ratingLetterString[6];
            ratingCaption.text = ratingLetterCaption[6];
        }
        else if (ratingPercentage >= 21 && ratingPercentage < 41)
        {
            ratingLetter.text = ratingLetterString[5];
            ratingCaption.text = ratingLetterCaption[5];
        }
        else if (ratingPercentage >= 41 && ratingPercentage < 61)
        {
            ratingLetter.text = ratingLetterString[4];
            ratingCaption.text = ratingLetterCaption[4];
        }
        else if (ratingPercentage >= 61 && ratingPercentage < 81)
        {
            ratingLetter.text = ratingLetterString[3];
            ratingCaption.text = ratingLetterCaption[3];
        }
        else if (ratingPercentage >= 81 && ratingPercentage < 91)
        {
            ratingLetter.text = ratingLetterString[2];
            ratingCaption.text = ratingLetterCaption[2];
        }
        else if (ratingPercentage >= 91 && ratingPercentage < 100)
        {
            ratingLetter.text = ratingLetterString[1];
            ratingCaption.text = ratingLetterCaption[1];
        }
        else if (ratingPercentage >= 100)
        {
            ratingLetter.text = ratingLetterString[0];
            ratingCaption.text = ratingLetterCaption[0];
        }
    }
}
