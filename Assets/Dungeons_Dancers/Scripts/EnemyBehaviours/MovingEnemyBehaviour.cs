using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyBehaviour : MonoBehaviour {
    
    public Transform[] points;
    private int destPoint = 0;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private bool activeTrapEvent;

    void Start()
    {
        
    }

    void Update()
    {
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
            //GotoNextPoint();
    }

    public void ActiveTrap()
    {
        activeTrapEvent = true;
    }
    public void DisableTrap()
    {
        activeTrapEvent = false;
    }

    void GotoNextPoint()
    {
        //agent.destination = points[destPoint].position;
        //destPoint = (destPoint + 1) % points.Length;
    }

    private IEnumerator ReturnIdle(float time)
    {
        yield return new WaitForSeconds(time);
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


    private void OnTriggerEnter(Collider other)
    {
        //Collide with directional platform
    }
}
