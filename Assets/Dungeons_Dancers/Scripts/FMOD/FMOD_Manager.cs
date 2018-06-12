using System;
using System.Runtime.InteropServices;
using UnityEngine;
using SonicBloom.Koreo.Players;
using FMODUnity;


class FMOD_Manager : MonoBehaviour
{
    // Variables that are modified in the callback need to be part of a seperate class.
    // This class needs to be ‘blittable’ otherwise it can’t be pinned in memory.
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    public TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;

    [SerializeField]
    private SatisfactionController satisController;

    public MultiMusicPlayer musicPlayer;
    [HideInInspector]
    public bool koreoWithFmod = false;

    string instanceEvent = "event:/Songs/World_01/Song_01";
    string lastMarker = "Click";
    string satisfactionParameter = "Satisfaction";

    [Header("Emitters")]
    [SerializeField]
    private StudioEventEmitter[] emitterTrap = new StudioEventEmitter[5];
    [SerializeField]
    private StudioEventEmitter[] emitterSkeleton = new StudioEventEmitter[5];

	void Awake()
    {
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(instanceEvent);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.start();
    }

    void OnDestroy()
    {
        UnloadFMOD();
    }

    public void UnloadFMOD()
    {
        for (int w = 0; w < emitterSkeleton.Length; w++) emitterSkeleton[w].Stop();
        for(int i = 0; i < emitterTrap.Length; i++) emitterTrap[i].Stop();
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free(); 
    }

    void OnApplicationQuit()
    {
        UnloadFMOD();
    }

    private void Update()
    {
        musicInstance.setParameterValue(satisfactionParameter, satisController.GetSatisfactionPoints(0, 1, 0));
		//musicInstance.setParameterValue(satisfactionParameter, 0);
    }
    private void FixedUpdate()
    {
        if ((string)timelineInfo.lastMarker == lastMarker && koreoWithFmod){
            musicPlayer.Play();
            koreoWithFmod = false;
        }
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        // Retrieve the user data
        IntPtr timelineInfoPtr;
        instance.getUserData(out timelineInfoPtr);

        // Get the object to store beat and marker details
        GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
        TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

        switch (type)
        {
            case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                {
                    var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                    timelineInfo.currentMusicBar = parameter.bar;
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                {
                    var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                    timelineInfo.lastMarker = parameter.name;
                }
                break;
        }
        return FMOD.RESULT.OK;
    }
}
