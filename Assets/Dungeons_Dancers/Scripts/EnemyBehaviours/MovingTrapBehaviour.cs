using System.Collections;
using UnityEngine;

public class MovingTrapBehaviour : MonoBehaviour {

    private Material mat;

    [Header("Choose Trap Behaviour from 1-3")]
    [SerializeField]
    private int trapBehaviour;
    [Header("Choose Idle Return Time")]
    [SerializeField]
    private float timeIdle = 0.25f;

    [Header ("Choose Max-Min Position")]
    [SerializeField]
    private float maxPosition;
    [SerializeField]
    private float minPosition;
    [Header("Moves Horizontally?")]
    [SerializeField]
    private bool xAxis;

    private int direction = 1;
    private bool activeTrapEvent;

    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    public void ActiveTrap()
    {
        activeTrapEvent = true;
        mat.color = Color.white;
        if (Mathf.Approximately(this.transform.position.x, maxPosition))
        {
            direction *= -1;
            if(xAxis)this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0, 90);
            else this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 90, 90);
        } 
        else if (Mathf.Approximately(this.transform.position.x, minPosition))
        {
            direction *= -1;
            if(xAxis)this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 180, 90);
            else this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, -90, 90);
        }

        if (xAxis)
        {
            this.transform.position = new Vector3(this.transform.position.x + (direction), this.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (direction));
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

    public int GetTrapBehaviour()
    {
        return trapBehaviour;
    }

    public bool GetActiveTrapEvent()
    {
        return activeTrapEvent;
    }
}

