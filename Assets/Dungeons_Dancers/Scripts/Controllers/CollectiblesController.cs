using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour {

    [Header("Coins Value")]
    [SerializeField]
    private int coinsValue = 10;
    private int coins = 10;
    private List<int> keys = new List<int>();

    [Header("Drag Doors")]
    [SerializeField]
    private GameObject[] doors;

    public void AddCoin(){
        coins = coins + coinsValue;
    }

    public void AddKey(int key)
    {
        keys.Add(key);
        for (int i = 0; i < doors.Length; i++)
        {
            if (key == int.Parse(doors[i].name.Substring(0, 2)))
            {
                doors[i].GetComponent<Collider>().enabled = false;
                keys.Remove(i);
            }
        }
    }

    public void RemoveCoin(int ncoins){
        coins = coinsValue - ncoins;
    }

    public int GetTotalKeys(){
        return keys.Count;
    }

    public int GetCoins(bool fever){
        if (fever)
        {
            coins = coinsValue * 2;
            return coins;
        }
        else return coins;
    }

    public void Reset(){
        for (int i = 0; i < doors.Length; i++) doors[i].GetComponent<Collider>().enabled = true;
        keys.Clear();
    }
}
