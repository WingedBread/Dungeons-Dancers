using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SatisfactionBarSetupValues", menuName = "Tools/SatisfactionBarSetup")]
public class SatisfactionBarSetup : ScriptableObject
{
    [Header("Bar Values")]
    [HideInInspector]
    public int initPoints;
    [HideInInspector]
    public int minPoints;
    [HideInInspector]
    public int maxPoints;

    //[Range(0, 89)]
    //[HideInInspector]
    //public float rafa;

    [Header("Input Points")]
    [HideInInspector]
    public int soonPoints;
    [HideInInspector]
    public int perfectPoints;
    [HideInInspector]
    public int latePoints;
    [HideInInspector]
    public int failPoints; //MEjor que sea un valor negativo directamente

    [Header("")]
    [HideInInspector]
    public int amountOfFailInputsWhenFever;
    //[HideInInspector]
    //public List<Rafa> cosas;

    //INSERT INSPECTORGUI BUTTON TO OPEN HIS EDITOR WINDOW.

    //public void RAFA()
    //{
    //    Debug.Log("hell yeah");
    //    Rafa temp = new Rafa();
    //    Debug.Log(JsonUtility.ToJson(temp));
    //}
}

//[Serializable]
//public class Rafa
//{
//    public int cosa1 = 3;
//    public string cosa2 = "pepe";

//}