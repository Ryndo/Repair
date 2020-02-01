using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public int actionAmount;
    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
}
