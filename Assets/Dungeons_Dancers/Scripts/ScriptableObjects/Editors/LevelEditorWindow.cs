#if UNITY_EDITOR

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
                break;
        }

        if(levelSetup.levelStatesEvt.Count > 0 || levelSetup.levelEventsEvt.Count > 0 || levelSetup.playerStatesEvt.Count > 0)
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
                    else if(Selection.activeGameObject.GetComponent<LevelEventEvents>() != null){

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
                    else if (Selection.activeGameObject.GetComponent<PlayerStatesEvents>() != null)
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
                if (!Selection.activeTransform)
                {
                    status = "Select a GameObject";
                    showGameObject = false;
                }

                if (GUILayout.Button("Remove Event"))
                {
                    //levelsetup.DeleteEvent();
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

            }
        }   
    }
}
#endif
