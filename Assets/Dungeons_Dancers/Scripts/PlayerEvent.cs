using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class PlayerEvent : MonoBehaviour
{
    private Vector3 playerTransform;

    // Use this for initialization
    void Start()
    {
        Koreographer.Instance.RegisterForEvents("PlayerEvent", ChangeSize);

        playerTransform = new Vector3(1, 1, 1);
    }

    void ChangeSize(KoreographyEvent kPlayerEvent)
    {
        Debug.Log("playerworking");

        if (this.transform.localScale == playerTransform)
        {
            this.transform.localScale = new Vector3(1.2f, 1, 1);
        }
        else this.transform.localScale = playerTransform;

    }
}