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

    private string[] tagsText = {"Exit", "Trap", "Sparkle", "Key", "Spawn", "Door", "Obstacle"};

	private void Start()
	{
        playerManager = transform.parent.GetChild(0).GetComponent<PlayerManager>();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == tagsText[0])
        {
            playerManager.ExitBehaviour();
        }
        else if (col.gameObject.tag == tagsText[1])
        {
            playerManager.TrapBehaviour();
        }
        else if (col.gameObject.tag == tagsText[2])
        {
            playerManager.SparkleBehaviour(col);
        }
        else if (col.gameObject.tag == tagsText[3])
        {
            playerManager.KeyBehaviour(col);
        }
        else if (col.gameObject.tag == tagsText[4])
        {
            playerManager.SpawnBehaviour(col);
        }
        else if (col.gameObject.tag == tagsText[5])
        {
            playerManager.DoorBehaviour();
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
            if (rayHit.collider.gameObject.tag == tagsText[6]) return true;

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
            if (rayHit.collider.gameObject.tag == tagsText[6]) return true;
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
            if (rayHit.collider.gameObject.tag == tagsText[6]) return true;
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
            if (rayHit.collider.gameObject.tag == tagsText[6]) return true;
        }
        return false;
    }
    #endregion
}
