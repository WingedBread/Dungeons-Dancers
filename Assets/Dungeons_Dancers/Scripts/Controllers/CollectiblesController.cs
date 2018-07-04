using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour {
    
    private List<int> keys = new List<int>();

    [Header("Drag Doors")]
    [SerializeField]
    private GameObject[] doors;
    [SerializeField]
    private Material[] doorMaterial;


    public void AddKey(int key)
    {
        keys.Add(key);
        for (int i = 0; i < doors.Length; i++)
        {
            if (key == int.Parse(doors[i].name.Substring(0, 2)))
            {
                doors[i].GetComponent<MeshRenderer>().material = doorMaterial[1];
                doors[i].transform.GetChild(0).tag = "Door";
            }
        }
    }



    public int GetTotalKeys(){
        return keys.Count;
    }



    public void OpenDoor()
    {
        for (int i = 0; i < keys.Count; i++)
        {   doors[i].transform.GetChild(0).GetComponent<Collider>().enabled = false;
            doors[i].transform.GetChild(0).tag = "Obstacle";
            doors[i].transform.rotation = Quaternion.Euler(0, 90, 0);
            keys.Remove(i);
        }
    }

    public void Reset(){
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].transform.GetChild(0).GetComponent<Collider>().enabled = true;
            doors[i].transform.GetChild(0).tag = "Obstacle";
            doors[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            doors[i].transform.GetComponent<MeshRenderer>().material = doorMaterial[0];
        }
        keys.Clear();
    }
}
