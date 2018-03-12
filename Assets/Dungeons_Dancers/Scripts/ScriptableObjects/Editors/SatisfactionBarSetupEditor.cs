using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SatisfactionBarSetup))]
public class SatisfactionBarSetupEditor : Editor {


    float rafa = 99;

    float pointsmin = 0;
    float pointsmax = 100;
    float pointsInitial = 20;

    bool showCosas = true;
    bool canJump = true;

	public override void OnInspectorGUI()
	{
        //base.OnInspectorGUI();

        SatisfactionBarSetup so = target as SatisfactionBarSetup;

        //EditorGUILayout.BeginHorizontal();
        so.minPoints = EditorGUILayout.IntField("Min: ",so.minPoints/*, GUILayout.Width(150)*/);
        so.initPoints = EditorGUILayout.IntField("Init: ", so.initPoints/*, GUILayout.Width(150)*/);
        so.maxPoints = EditorGUILayout.IntField("Max: ",so.maxPoints/*, GUILayout.Width(150)*/);
        //EditorGUILayout.EndHorizontal();

        /*
        EditorGUILayout.MinMaxSlider("pepe",ref pointsmin, ref pointsmax, 0, 100);

        rafa = EditorGUILayout.FloatField(rafa);

        EditorGUILayout.ColorField(Color.red);

        EditorGUILayout.FloatField(rafa+90);
*/
        showCosas = EditorGUILayout.Foldout(showCosas,"COSAS");
        if (showCosas)
        {

            for (int i = 0; i < so.cosas.Count; i++)
            {
                EditorGUILayout.LabelField("COSA " + i + ":");
                so.cosas[i].cosa1 = EditorGUILayout.IntField("Cosa 1", so.cosas[i].cosa1);
                so.cosas[i].cosa2 = EditorGUILayout.TextField("Cosa 2", so.cosas[i].cosa2);

            }




            canJump = EditorGUILayout.Toggle("Can Jump", canJump);

            // Disable the jumping height control if canJump is false:
            EditorGUI.BeginDisabledGroup(!canJump);
            pointsmax = EditorGUILayout.FloatField("Jump Height", pointsmax);
            EditorGUI.EndDisabledGroup();




            if (GUILayout.Button("RAFA"))
            {
                so.RAFA();
            }



            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {

                so.cosas.Add(new Rafa());
            }

            if (GUILayout.Button("Delete"))
            {

                so.cosas.RemoveAt(so.cosas.Count - 1);
            }
            EditorGUILayout.EndHorizontal();
        }
    }

}



