using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class PlayerEvent : MonoBehaviour
{
    private Material mat;
    private bool activePlayerEvent;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    // Use this for initialization
    void Start()
    {
        //correct = false;
        Koreographer.Instance.RegisterForEventsWithTime("PlayerEvent", PlayerBehaviour);

        mat = this.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        PlayerInput();
    }


    void PlayerBehaviour(KoreographyEvent kEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {                  
        //Debug.Log("playerworking");
        if (sampleTime < kEvent.EndSample)
        {
            activePlayerEvent = true;
        }
        else
        {
            activePlayerEvent = false;
        } 

    }

    void PlayerInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (activePlayerEvent)
            {
                Debug.Log("perfect!");
                mat.color = Color.green;
                StartCoroutine(ReturnIdle(timeIdle));

            }
            else
            {
                Debug.Log("incorrect!");
                mat.color = Color.red;
                StartCoroutine(ReturnIdle(timeIdle));
            }
        }
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("idle player!");
        mat.color = Color.white;
        StopCoroutine("ReturnIdle");
    }
}