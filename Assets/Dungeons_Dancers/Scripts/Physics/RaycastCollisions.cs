using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisions : MonoBehaviour
{
    private RaycastHit rayHit;
    private Ray ray;
    [Header("Ray Lenght")]
    [SerializeField]
    private float rayLenght = 1f;

    #region Collisions
    public bool RightCollision()
    {
        Vector3 colliderCenter = new Vector3(this.GetComponent<Collider>().bounds.center.x, this.GetComponent<Collider>().bounds.center.y - 0.5f, this.GetComponent<Collider>().bounds.center.z);
        ray = new Ray(colliderCenter, Vector3.right);

        if (Physics.Raycast(ray, out rayHit, rayLenght))
        {
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
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
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
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
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
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
            Debug.Log("Collision with  " + rayHit.collider.gameObject.name);
            if (rayHit.collider.gameObject.tag == "Obstacle") return true;
        }
        return false;
    }
    #endregion
}
