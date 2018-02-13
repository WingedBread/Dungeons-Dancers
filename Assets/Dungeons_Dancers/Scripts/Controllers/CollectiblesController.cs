using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesController : MonoBehaviour {

    private int coins = 0;
    private List<int> keys = new List<int>();

    public void AddCoin(){
        coins++;
    }

    public void AddKey(int key)
    {
        keys.Add(key);
    }

    public void RemoveCoin(int ncoins){
        coins = coins - ncoins;
    }

    public void RemoveKey(int doorInt, Collider doorCol){
        for (int i = 0; i< keys.Count; i++)
        {
            if (doorInt == keys[i]){
                keys.Remove(i);
                doorCol.enabled = false;
            }
        } 
    }

    public int GetTotalKeys(){
        return keys.Count;
    }

    public int GetCoins(){
        return coins;
    }
}
