using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrapBehaviour : MonoBehaviour {
    
    private Material mat;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    [Header("Choose Instantiate Prefab")]
    [SerializeField]
    private GameObject projectile;
    private GameObject activeProjectile;

    [Header("Choose Max Position")]
    [SerializeField]
    private float maxPosition;
    private float minPosition;
    [Header("Set Direction")]
    [SerializeField]
    private int direction = 1;
    [Header("Moves Horizontally?")]
    [SerializeField]
    private bool xAxis;

    private bool activeTrapEvent;

    void Start()
    {
        InstantiateProjectile();
        mat = this.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    public void ActiveTrap()
    {
        activeTrapEvent = true;
        mat.color = Color.white;

        if (Mathf.Approximately(activeProjectile.transform.position.x, maxPosition))
        {
            Destroy(activeProjectile);
            InstantiateProjectile();
        }

        if (xAxis)
        {
            activeProjectile.transform.position = new Vector3(activeProjectile.transform.position.x + (direction), activeProjectile.transform.position.y, activeProjectile.transform.position.z);
        }
        else
        {
            activeProjectile.transform.position = new Vector3(activeProjectile.transform.position.x, activeProjectile.transform.position.y, activeProjectile.transform.position.z + (direction));
        }


        StartCoroutine(ReturnIdle(timeIdle));
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        mat.color = Color.black;
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        mat.color = Color.black;
        StopCoroutine("ReturnIdle");
    }

    void InstantiateProjectile()
    {
        activeProjectile = Instantiate(projectile,this.transform, true);
    }

    public int GetTrapBehaviour()
    {
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent()
    {
        return activeTrapEvent;
    }
}
