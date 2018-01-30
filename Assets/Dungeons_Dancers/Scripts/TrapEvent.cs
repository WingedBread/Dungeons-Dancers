using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class TrapEvent : MonoBehaviour {

    private Material mat;
    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;

	// Use this for initialization
	void Start () 
    {
        switch (trapBehaviour)
        {
            case 1:
                Koreographer.Instance.RegisterForEvents("Trap1Event", ChangeColor);
                break;
            case 2:
                Koreographer.Instance.RegisterForEvents("Trap2Event", ChangeColor);
                break;
            case 3:
                Koreographer.Instance.RegisterForEvents("Trap3Event", ChangeColor);
                break;
        }
        mat = this.GetComponent<MeshRenderer>().material;
	}
	
    void ChangeColor(KoreographyEvent kTrapEvent)
    {
        Debug.Log("trapworking");

        if (mat.color == Color.black) mat.color = Color.white;
        else mat.color = Color.black;
    }
}
