using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private RaycastHit rayHit;
    private Ray ray;
    [Header("Ray Lenght")]
    [SerializeField]
    private float rayLenght = 1f;

    [Header("PlayerManager")]
    private PlayerManager playerManager;

	private void Start()
	{
        playerManager = transform.parent.GetChild(0).GetComponent<PlayerManager>();
	}

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Exit":
                playerManager.ExitBehaviour();
                break;
            case "Trap":
                playerManager.TrapBehaviour();
                break;
            case "Coin":
                playerManager.CoinBehaviour(col);
                break;
            case "Key":
                playerManager.KeyBehaviour(col);
                break;
            case "Spawn":
                playerManager.SpawnBehaviour(col);
                break;
            case "Door":
                playerManager.DoorBehaviour();
                break;
        }
    }

	#region Raycast
	public bool RightCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.right);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            //Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;

        }
        return false;
    }

    public bool LeftCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.right * -1);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            //Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }

    public bool DownCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.forward * -1);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            //Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }

    public bool UpCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.forward);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            //Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }
    #endregion
}
