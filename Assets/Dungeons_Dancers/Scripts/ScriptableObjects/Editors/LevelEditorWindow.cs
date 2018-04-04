#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;
//using SonicBloom.Koreo;

[CustomEditor(typeof(LevelSetup))]
public class LevelEditorWindow : EditorWindow
{

    string status;
    bool showGameObject;

    LevelSetup levelsetup;

    LevelStates levelStates;
    PlayerStates playerStates;
    LevelEvents levelEvents;

    UnityEditor.Animations.AnimatorController animator;
    AudioClip auClip;
    GameObject particles;
    Ease easingListIn;
    Ease easingListOut;

    //[EventID]
    //public string eventID;

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
            LevelEventsClass temp = new LevelEventsClass();
            levelsetup.AddEvent(temp);
        }

        levelStates = (LevelStates)EditorGUILayout.EnumFlagsField("Level States", levelStates);
        levelEvents = (LevelEvents)EditorGUILayout.EnumFlagsField("Level States", levelEvents);

        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
        if (showGameObject)
        {
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;

                if (Selection.activeGameObject.tag == "Player") playerStates = (PlayerStates)EditorGUILayout.EnumFlagsField("Player States", playerStates);

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

                easingListIn = (Ease)EditorGUILayout.EnumFlagsField("Easing List IN", easingListIn);
                easingListOut = (Ease)EditorGUILayout.EnumFlagsField("Easing List OUT", easingListOut);
                /*Position -- Scale -- Rotation -- Color -- Alpha-Fade
                 * Select Tween In--
                 * From
                 * To
                 * Duration
                 * Select Tween Out--
                 * From
                 * To
                 * Duration
                 */


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
            
        }
    }
}
#endif
