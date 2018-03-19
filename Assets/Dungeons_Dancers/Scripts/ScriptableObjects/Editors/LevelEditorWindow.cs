using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(LevelSetup))]
public class LevelEditorWindow : EditorWindow {

    string status;

    bool showGameObject;

    LevelSetup levelsetup;

    LevelStates levelStates;
    PlayerStates playerStates;

    Animator animator;
    AudioClip auClip;
    GameObject particles;

    [MenuItem("Curial Tools/Level Editor", false, 0)]
    static void Init(){
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

	private void Awake()
	{
        levelsetup = new LevelSetup();
	}

    private void OnGUI()
    {
        if (GUILayout.Button("Add Event"))
        {
            levelsetup.AddEvent();
        }


        levelStates = (LevelStates)EditorGUILayout.EnumFlagsField("Level Events", levelStates);
        playerStates = (PlayerStates)EditorGUILayout.EnumFlagsField("Player Events", playerStates);

        showGameObject = EditorGUILayout.Foldout(showGameObject, status, true);
        if (showGameObject)
        {
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;
            }
        }
        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            showGameObject = false;
        }

        animator = (Animator)EditorGUILayout.ObjectField("Animator", animator, typeof(Animator), true);
        //if (animator != null) for (int i = 0; i < levelsetup.eventsLevel.Count - 1; i++)levelsetup.eventsLevel[i].SetAnimator(animator);

        auClip = (AudioClip)EditorGUILayout.ObjectField("AudioClip", auClip, typeof(AudioClip), true);
        //if (auClip != null) levelsetup.SetAudioClip(auClip);

        particles = (GameObject)EditorGUILayout.ObjectField("Partciles", particles, typeof(GameObject), true);
        //if (particles != null) levelsetup.SetParticles(particles);

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
