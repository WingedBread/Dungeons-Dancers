using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class PlayerEvent : MonoBehaviour
{
    private Material mat;

    private bool correct;

    // Use this for initialization
    void Start()
    {
        correct = false;
        Koreographer.Instance.RegisterForEventsWithTime("PlayerEvent", PlayerBehaviour);

        mat = this.GetComponent<MeshRenderer>().material;
    }

    //public delegate void KoreographyEventCallbackWithTime(KoreographyEvent kEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice);

    void PlayerBehaviour(KoreographyEvent kEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        //Debug.Log("playerworking");
        if (sampleTime < kEvent.EndSample)
        {
            Debug.Log("correct");
            CorrectInput();
            //correct = true;
        }
        else{
            Debug.Log("incorrect");
            IncorrectInput();
            //correct = false;
        } 

    }

    void CorrectInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("perfect! :D");
            mat.color = Color.green;
        }
    }

    void IncorrectInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("incorrect! :(");
            mat.color = Color.red;
        }
    }

}