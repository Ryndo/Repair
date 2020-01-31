using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDDOL : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(gameObject);
    }
}
