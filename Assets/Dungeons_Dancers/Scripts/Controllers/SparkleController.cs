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
        Destroy(transform.parent.GetChild(0).gameObject);
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
        Destroy(this.transform.parent);
    }

    public void RemoveSparkles(int ncoins)
    {
        totalSparkles = sparklesValue - ncoins;
    }

}
