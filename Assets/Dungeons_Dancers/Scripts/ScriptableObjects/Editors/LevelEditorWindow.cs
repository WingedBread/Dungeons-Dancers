﻿#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using DG.Tweening;
//using SonicBloom.Koreo;

public class LevelEditorWindow : EditorWindow
{

    string status;
    bool showGameObject;

    LevelStates levelStates;
    PlayerStates playerStates;
    LevelEvents levelEvents;

    LevelSetup levelSetup;

    UnityEditor.Animations.AnimatorController animator;
    AudioClip auClip;
    GameObject particles;
    Ease easingListIn;
    Ease easingListOut;

    enum LevelOptions { LevelStates, LevelEvents, PlayerStates };
    LevelOptions optionSelection;

    [MenuItem("Curial Tools/Level Editor", false, 0)]
    static void Init()
    {
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    void OnEnable()
    {
        GameObject go = GameObject.FindWithTag("GameManager");
        levelSetup = (LevelSetup)go.GetComponent(typeof(LevelSetup));
        if(levelSetup == null )Debug.Log("Add LevelSetup to GameManager GameObject!");
    }

    private void OnGUI()
    {
        if (levelSetup != null)
        {
            EditorGUILayout.Space();
            optionSelection = (LevelOptions)EditorGUILayout.EnumPopup("Choose Event/State", optionSelection);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            switch (optionSelection)
            {
                case LevelOptions.LevelStates:
                    if (GUILayout.Button("Add  ''LevelState''  Event"))
                    {
                        if (Selection.activeGameObject.GetComponent<LevelStateEvents>() == null)
                        {
                            Selection.activeGameObject.AddComponent<LevelStateEvents>();
                            levelSetup.levelStatesEvt.Add(Selection.activeGameObject.GetComponent<LevelStateEvents>());
                        }
                        else Debug.Log("This GameObject already has a LevelState Event");
                    }
                    EditorGUILayout.Space();
                    levelStates = (LevelStates)EditorGUILayout.EnumPopup("Level States", levelStates);

                    if (levelSetup.levelStatesEvt.Count > 0)
                    {
                        EditorGUILayout.Space();
                        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
                        if (showGameObject)
                        {
                            if (Selection.activeTransform)
                            {
                                status = Selection.activeTransform.name;
                                if (Selection.activeGameObject.GetComponent<LevelStateEvents>() != null)
                                {
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
                        }

                        if (GUILayout.Button("Remove Level State"))
                        {
                            if(Selection.activeGameObject.GetComponent<LevelStateEvents>().GetActiveEventsCount() <= 1)
                            {
                                Selection.activeGameObject.GetComponent<LevelStateEvents>().UncheckAllEvents();
                                levelSetup.levelStatesEvt.RemoveAt(levelSetup.levelStatesEvt.Count - 1);
                                DestroyImmediate(Selection.activeGameObject.GetComponent<LevelStateEvents>());

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

                            else
                            {
                                Selection.activeGameObject.GetComponent<LevelStateEvents>().UncheckLastEvent();
                            }

                        }

                        if (GUILayout.Button("Save Level State"))
                        {
                            Selection.activeGameObject.GetComponent<LevelStateEvents>().SetLevelState(levelStates);
                            Selection.activeGameObject.GetComponent<LevelStateEvents>().CheckActiveEvents();
                        }
                    }

                    break;
                case LevelOptions.LevelEvents:
                    if (GUILayout.Button("Add  ''LevelEvent''  Event"))
                    {
                        if (Selection.activeGameObject.GetComponent<LevelEventEvents>() == null)
                        {
                            Selection.activeGameObject.AddComponent<LevelEventEvents>();
                            levelSetup.levelEventsEvt.Add(Selection.activeGameObject.GetComponent<LevelEventEvents>());
                        }
                        else Debug.Log("This GameObject already has a LevelEvent Event");
                    }
                    EditorGUILayout.Space();
                    levelEvents = (LevelEvents)EditorGUILayout.EnumPopup("Level Events", levelEvents);

                    if (levelSetup.levelEventsEvt.Count > 0)
                    {
                        EditorGUILayout.Space();
                        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
                        if (showGameObject)
                        {
                            if (Selection.activeTransform)
                            {
                                status = Selection.activeTransform.name;
                                if (Selection.activeGameObject.GetComponent<LevelEventEvents>() != null)
                                {

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
                        }

                        if (GUILayout.Button("Remove Level Event"))
                        {
                            if (Selection.activeGameObject.GetComponent<LevelEventEvents>().GetActiveEventsCount() <= 1)
                            {
                                Selection.activeGameObject.GetComponent<LevelEventEvents>().UncheckAllEvents();
                                levelSetup.levelEventsEvt.RemoveAt(levelSetup.levelEventsEvt.Count - 1);
                                DestroyImmediate(Selection.activeGameObject.GetComponent<LevelEventEvents>());

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

                            else
                            {
                                Selection.activeGameObject.GetComponent<LevelEventEvents>().UncheckLastEvent();
                            }

                        }


                        if (GUILayout.Button("Save Level Events"))
                        {
                            Selection.activeGameObject.GetComponent<LevelEventEvents>().SetLevelEvents(levelEvents);
                            Selection.activeGameObject.GetComponent<LevelEventEvents>().CheckActiveEvents();
                        }
                    }

                    break;
                case LevelOptions.PlayerStates:
                    if (GUILayout.Button("Add  ''PlayerState''  Event"))
                    {
                        if (Selection.activeGameObject.GetComponent<PlayerStatesEvents>() == null)
                        {
                            Selection.activeGameObject.AddComponent<PlayerStatesEvents>();
                            levelSetup.playerStatesEvt.Add(Selection.activeGameObject.GetComponent<PlayerStatesEvents>());
                        }
                        else Debug.Log("This GameObject already has a PlayerState Event");
                    }
                    EditorGUILayout.Space();
                    playerStates = (PlayerStates)EditorGUILayout.EnumPopup("Player States", playerStates);

                    if (levelSetup.playerStatesEvt.Count > 0)
                    {
                        EditorGUILayout.Space();
                        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
                        if (showGameObject)
                        {
                            if (Selection.activeTransform)
                            {
                                status = Selection.activeTransform.name;
                                if (Selection.activeGameObject.GetComponent<PlayerStatesEvents>() != null)
                                {
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
                        }

                        if (GUILayout.Button("Remove Player State"))
                        {
                            if (Selection.activeGameObject.GetComponent<PlayerStatesEvents>().GetActiveEventsCount() <= 1)
                            {
                                Selection.activeGameObject.GetComponent<PlayerStatesEvents>().UncheckAllEvents();
                                levelSetup.playerStatesEvt.RemoveAt(levelSetup.playerStatesEvt.Count - 1);
                                DestroyImmediate(Selection.activeGameObject.GetComponent<PlayerStatesEvents>());

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

                            else
                            {
                                Selection.activeGameObject.GetComponent<PlayerStatesEvents>().UncheckLastEvent();
                            }

                        }

                        if (GUILayout.Button("Save Player State"))
                        {
                            Selection.activeGameObject.GetComponent<PlayerStatesEvents>().SetPlayerState(playerStates);
                            Selection.activeGameObject.GetComponent<PlayerStatesEvents>().CheckActiveEvents();
                        }
                    }

                    break;
            }

            if (!Selection.activeTransform)
            {
                status = "Select a GameObject";
                showGameObject = false;
            }
        }
        else
        {
            if (GUILayout.Button("Find Level Setup in Game Manager"))
            {
                GameObject go = GameObject.FindWithTag("GameManager");
                levelSetup = (LevelSetup)go.GetComponent(typeof(LevelSetup));
            }
        }
    }
}
#endif
