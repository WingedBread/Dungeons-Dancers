using UnityEngine;

public class SparkleController : MonoBehaviour
{

    [SerializeField]
    private int sparklesValue = 10;

    [HideInInspector]
    public static int totalSparkles = 0;

    private GameObject sparkleParticleSystem;

    private void Start()
    {
        sparkleParticleSystem = transform.parent.GetChild(1).gameObject;
    }

    public void AddSparkles()
    {
        totalSparkles = totalSparkles + sparklesValue;
        transform.parent.GetChild(0).gameObject.SetActive(false);
        sparkleParticleSystem.GetComponent<ParticleSystem>().Play();
        if (sparkleParticleSystem != null)
        {
            for (int i = 0; i < sparkleParticleSystem.transform.childCount; i++)
            {
                sparkleParticleSystem.transform.GetChild(i).GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void OnParticleSystemStopped()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    public void RemoveSparkles(int ncoins)
    {
        totalSparkles = sparklesValue - ncoins;
    }

    public void Restart()
    {
        transform.parent.GetChild(0).gameObject.SetActive(true);
        this.transform.parent.gameObject.SetActive(true);
        totalSparkles = 0;
        PlayerPrefs.SetInt("TotalScore", totalSparkles);
    }

}
