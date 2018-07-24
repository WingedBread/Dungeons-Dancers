namespace UnityEngine.PostProcessing
{
    public class PostProcessingSettings : MonoBehaviour
    {
        [Header("Every Array Number is equal to it's Quality Setting")]
        [Header("0 - Very Low...6 - Ultra")]
        [SerializeField]
        private PostProcessingProfile[] avaiablePostProcessings = new PostProcessingProfile[7];

        // Use this for initialization
        void Awake()
        {
            GetComponent<PostProcessingBehaviour>().profile = avaiablePostProcessings[QualitySettings.GetQualityLevel()];
        }
    }
}
