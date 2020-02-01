using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    bool isReady;
    Player player;

    void Awake(){
        MenuManager.instance.players.Add(gameObject);
        player = GetComponent<Player>();
    }
    void Start(){
        
    }
    public void OnReady(){
        MenuManager.instance.playersPressingReady[player.id] = !MenuManager.instance.playersPressingReady[player.id];
    }
}
