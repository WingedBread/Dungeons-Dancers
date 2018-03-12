using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "StatisfactionBarSetupValues", menuName = "Tools/SatisfactionBar")]
public class SatisfactionBarSetup : ScriptableObject
{
    [Header("Bar Values")]
    public int initPoints;
    public int minPoints;
    public int maxPoints;

    [Range(0,89)]
    public float rafa;

    [Header("Input Points")]
    public int soonPoints;
    public int perfectPoints;
    public int latePoints;
    public int failPoints; //MEjor que sea un valor negativo directamente

    [Header("")]
    public int amountOfFailInputsWhenFever;

    public List<Rafa> cosas;



    public void RAFA()
    {

        Debug.Log("hell yeah");


        Rafa temp = new Rafa();
        Debug.Log(JsonUtility.ToJson(temp));

    }
}

[Serializable]
public class Rafa
{
    public int cosa1=3;
    public string cosa2="pepe";

}
