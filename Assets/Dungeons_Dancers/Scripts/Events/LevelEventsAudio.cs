using UnityEngine;
using SonicBloom.Koreo;

[RequireComponent(typeof(AudioSource))]
public class LevelEventsAudio : MonoBehaviour {
    private AudioSource audioSource;
    private GameManager gameManager;

    [SerializeField]
    LevelStates levelStates;
    [Header("ONLY WORKS ON LEVEL STATE = PLAY!")]
    [SerializeField]
    PlayerStates playerStates;
    [SerializeField]
    SatisfactionStates satisfactionStates;

    [SerializeField]
    LevelEvents[] levelEvents;

    [Space]
    [Header("ONLY WORKS ON LEVEL EVENT = BEAT BEHAVIOUR!")]
    [EventID]
    public string beatBhv;

    [SerializeField]
    private AudioClip[] auClip;

	// Use this for initialization
	void Start () 
    {
        if (this.gameObject.GetComponent<AudioSource>() != null) audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gameManager.levelEventsAudios.Add(this);
	}
	
    void EventContainer(LevelStates lstate, PlayerStates pstate, SatisfactionStates sstates)
    {
        
    }
}
