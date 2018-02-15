using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastCollisions))]
public class MovingEnemyBehaviour : MonoBehaviour {
    
    private RaycastCollisions ray;
    public Transform[] points;
    private int destPoint = 0;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private Vector3 upv3, downv3, leftv3, rightv3;

    private bool activeTrapEvent;

    void Start()
    {
        upv3 = new Vector3(1, 0, 0);
        downv3 = new Vector3(-1, 0, 0);
        leftv3 = new Vector3(0, 0, 1);
        rightv3 = new Vector3(0, 0, -1);

        ray.GetComponent<RaycastCollisions>();
    }

    void Update()
    {
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
            //GotoNextPoint();
    }

    public void ActiveTrap(){

    }

    public void DisableTrap(){
        
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
}
