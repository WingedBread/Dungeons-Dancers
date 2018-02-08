using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrapBehaviour : MonoBehaviour {
    
    [Header("Enemy Manager")]
    private EnemyManager enemiesManager;
    private Material mat;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    public int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private bool activeTrapEvent;

    void Start()
    {
        enemiesManager = this.transform.parent.GetComponent<EnemyManager>();
        mat = this.GetComponent<MeshRenderer>().material;
    }

    public void ActiveTrap()
    {
        activeTrapEvent = true;
        mat.color = Color.white;
        this.gameObject.GetComponent<Collider>().enabled = true;
        StartCoroutine(ReturnIdle(timeIdle));
    }

    public void DisableTrap()
    {
        activeTrapEvent = false;
        mat.color = Color.black;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Collider>().enabled = false;
        mat.color = Color.black;
        StopCoroutine("ReturnIdle");
    }

    public int GetTrapBehaviour()
    {
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent()
    {
        return activeTrapEvent;
    }

    public int GetTrapType()
    {
        return 1;
    }
}
