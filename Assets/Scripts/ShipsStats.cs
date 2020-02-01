using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsStats : MonoBehaviour
{
    public static ShipsStats instance;

    public float hitPercent = 90f;
    public float critPercent = 10f;
    public int criticalBonusDamages = 5;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
