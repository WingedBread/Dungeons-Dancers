using UnityEngine;

public class IntroController : MonoBehaviour {

    [Header("Game Manager")]
    private GameManager gameManager;

    private  int introCounter = 0;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>(); 
        introCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (introCounter < 7) IntroBeatCountdown();
	}

    private void IntroBeatCountdown()
    {
        switch (introCounter)
        {
            case 0:
                gameManager.IntroBehaviour(0);
                break;
            case 1:
                gameManager.IntroBehaviour(3);
                break;
            case 2:
                gameManager.IntroBehaviour(2);
                break;
            case 3:
                gameManager.IntroBehaviour(3);
                break;
            case 4:
                gameManager.IntroBehaviour(2);
                break;
            case 5:
                gameManager.IntroBehaviour(1);
                break;
            case 6:
                gameManager.IntroBehaviour(-1);
                introCounter++;
                break;
        }
    }

    public void SetCounter(int intro)
    {
        introCounter = intro;
    }

    public int GetCounter()
    {
        return introCounter;
    }
}
