#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSetup))]
public class LevelEditorWindow : EditorWindow
{

    string status;
    bool showGameObject;

    LevelSetup levelsetup;

    LevelStates levelStates;
    PlayerStates playerStates;

    UnityEditor.Animations.AnimatorController animator;
    AudioClip auClip;
    GameObject particles;

    [MenuItem("Curial Tools/Level Editor", false, 0)]
    static void Init()
    {
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    private void OnGUI()
    {
        levelsetup = (LevelSetup)EditorGUILayout.ObjectField("Level Setup", levelsetup, typeof(LevelSetup), true);

        if (GUILayout.Button("Add Event"))
        {
            LevelEvents temp = new LevelEvents();
            levelsetup.AddEvent(temp);
        }

        levelStates = (LevelStates)EditorGUILayout.EnumFlagsField("Level Events", levelStates);

        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
        if (showGameObject)
        {
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;

                if (Selection.activeGameObject.tag == "Player") playerStates = (PlayerStates)EditorGUILayout.EnumFlagsField("Player Events", playerStates);

                animator = (UnityEditor.Animations.AnimatorController)EditorGUILayout.ObjectField("Animator", animator, typeof(UnityEditor.Animations.AnimatorController), true);
                if (animator != null)
                {
                    if (Selection.activeGameObject.GetComponent<Animator>() != null)
                    {
                        Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
                    }
                    else Debug.Log("Does not have Animator");
                }

                auClip = (AudioClip)EditorGUILayout.ObjectField("AudioClip", auClip, typeof(AudioClip), true);
                if (auClip != null)
                {
                    if (Selection.activeGameObject.GetComponent<AudioSource>() != null)
                    {
                        Selection.activeGameObject.GetComponent<AudioSource>().clip = auClip;
                    }
                    else Debug.Log("Does not have Audio Source");
                }

                //Maybe change to ParticleSystem(?)
                particles = (GameObject)EditorGUILayout.ObjectField("Partciles", particles, typeof(GameObject), true);
                //if (animator != null)
                //{
                //    if (Selection.activeGameObject.GetComponent<Animator>() != null)
                //    {
                //        Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
                //    }
                //    else Debug.Log("Does not have Animator");
                //}
            }
        }
        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            showGameObject = false;
        }

        if (GUILayout.Button("Remove Event"))
        {
            levelsetup.DeleteEvent();
        }

        if (GUILayout.Button("Save Data"))
        {
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(Selection.activeObject);
        }
    }
}
#endif
