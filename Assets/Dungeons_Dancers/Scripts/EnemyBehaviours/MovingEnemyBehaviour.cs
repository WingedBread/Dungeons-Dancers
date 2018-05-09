using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyBehaviour : MonoBehaviour {

    [Header("Moves Horizontally?")]
    [SerializeField]
    private bool xAxis;
    [Header("Initial Direction")]
    [SerializeField]
    private int direction = -1;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    private bool activeTrapEvent;

    private void Start()
    {
        if (xAxis)
        {
            if(direction == 1)
            {
                SetRotation(1);
            }
            else if(direction == -1)
            {
                SetRotation(0);
            }
        }
        else
        {
            if (direction == 1)
            {
                SetRotation(3);
            }
            else if (direction == -1)
            {
                SetRotation(2);
            }
        }
    }
    public void ActiveTrap()
    {
        if (xAxis)
        {
            this.transform.position = new Vector3(this.transform.position.x + (direction), this.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (direction));
        }

        activeTrapEvent = true;
    }
    public void DisableTrap()
    {
        activeTrapEvent = false;
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

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "EnemyDirection")
        {
            if (col.gameObject.transform.GetChild(0).name == "Up")
            {
                direction = 1;
                SetRotation(3);
                xAxis = false;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Down")
            {
                direction = -1;
                SetRotation(2);
                xAxis = false;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Right")
            {
                direction = 1;
                SetRotation(1);
                xAxis = true;
            }
            else if (col.gameObject.transform.GetChild(0).name == "Left")
            {
                direction = -1;
                SetRotation(0);
                xAxis = true;
            }
        }
    }

    void SetRotation(int direction)
    {
        switch (direction)
        {
            case 0: //LEFT
                transform.GetChild(0).rotation = Quaternion.Euler(0, 90, 45);
                break;
            case 1: //RIGHT
                transform.GetChild(0).rotation = Quaternion.Euler(0, -90, -45);
                break;
            case 2: //DOWN
                transform.GetChild(0).rotation = Quaternion.Euler(45, 0, 0);
                break;
            case 3: //UP
                transform.GetChild(0).rotation = Quaternion.Euler(-45, 180, 0);
                break;
        }
    }
}
