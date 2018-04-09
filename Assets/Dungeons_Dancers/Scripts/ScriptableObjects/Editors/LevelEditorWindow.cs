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

    enum LevelOptions { LevelStates, LevelEvents, PlayerStates };
    LevelOptions optionSelection;

    //List<LevelEventsClass> currentEvents = new List<LevelEventsClass>();

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
            LevelEventsClass temp = ScriptableObject.CreateInstance<LevelEventsClass>();
            //currentEvents.Add(temp);
            levelsetup.AddEvent(temp);
        }

        optionSelection = (LevelOptions)EditorGUILayout.EnumPopup("Choose Event/State", optionSelection);


        if (levelsetup.eventsLevel.Count > 0)
        {
            if (optionSelection == LevelOptions.LevelStates) levelStates = (LevelStates)EditorGUILayout.EnumPopup("Level States", levelStates);
            else if (optionSelection == LevelOptions.LevelEvents) levelEvents = (LevelEvents)EditorGUILayout.EnumPopup("Level Events", levelEvents);
            else if (optionSelection == LevelOptions.PlayerStates) playerStates = (PlayerStates)EditorGUILayout.EnumPopup("Player States", playerStates);

            showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
            if (showGameObject)
            {
                if (Selection.activeTransform)
                {
                    status = Selection.activeTransform.name;
                    animator = (UnityEditor.Animations.AnimatorController)EditorGUILayout.ObjectField("Animator", animator, typeof(UnityEditor.Animations.AnimatorController), true);
                    if (animator != null)
                    {
                        if (Selection.activeGameObject.GetComponent<Animator>() != null)
                        {
                            Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController = animator;
                        }
                        else
                        {
                            Debug.Log("Does not have Animator");
                            if (GUILayout.Button("Add Animator"))
                            {
                                Selection.activeGameObject.AddComponent<Animator>();
                            }
                        }
                    }

                    auClip = (AudioClip)EditorGUILayout.ObjectField("AudioClip", auClip, typeof(AudioClip), true);
                    if (auClip != null)
                    {
                        if (Selection.activeGameObject.GetComponent<AudioSource>() != null)
                        {
                            levelsetup.eventsLevel[0].audioSource = Selection.activeGameObject.GetComponent<AudioSource>();
                            //currentEvents[0].audioSource = Selection.activeGameObject.GetComponent<AudioSource>();
                            Selection.activeGameObject.GetComponent<AudioSource>().clip = auClip;
                        }
                        else
                        {
                            Debug.Log("Does not have Audio Source");
                            if (GUILayout.Button("Add AudioSource"))
                            {
                                Selection.activeGameObject.AddComponent<AudioSource>();
                                Selection.activeGameObject.GetComponent<AudioSource>().playOnAwake = false;
                            }
                        }
                    }

                    //Maybe change to ParticleSystem(?)
                    particles = (GameObject)EditorGUILayout.ObjectField("Partciles", particles, typeof(GameObject), true);

                    easingListIn = (Ease)EditorGUILayout.EnumPopup("Easing List IN", easingListIn);
                    easingListOut = (Ease)EditorGUILayout.EnumPopup("Easing List OUT", easingListOut);
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
                //currentEvents.RemoveAt(currentEvents.Count - 1);
                if (auClip != null)
                {
                    auClip = null;
                    Selection.activeGameObject.GetComponent<AudioSource>().clip = null;
                }
                if (animator != null)
                {
                    animator = null;
                    Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController = null;
                }
            }

            if (GUILayout.Button("Save Event -- State"))
            {
                //currentEvents[0].selectedEventsEnum = levelEvents;
                //currentEvents[0].selectedStatesEnum = levelStates;
                //currentEvents[0].CheckActiveEventsAndStates();

                if (optionSelection == LevelOptions.LevelStates) levelsetup.eventsLevel[0].selectedStatesEnum = levelStates;
                else if (optionSelection == LevelOptions.LevelEvents) levelsetup.eventsLevel[0].selectedEventsEnum = levelEvents;
                else if (optionSelection == LevelOptions.PlayerStates) levelsetup.eventsLevel[0].selectedPlayerStateEnum = playerStates;

                levelsetup.eventsLevel[0].CheckActiveEventsAndStates();
                //AssetDatabase.SaveAssets();
            }
        }

    }
}
#endif
