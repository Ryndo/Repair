using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    bool isReady;
    void Awake(){
        MenuManager.instance.players.Add(gameObject);
    }
    public void OnReady(){
        isReady = isReady ? false : true;
        MenuManager.instance.ToggleReady(isReady);
        
    }
}
