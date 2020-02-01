﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public float readyCount;
    bool alreadyStarting;
    public List<GameObject> players = new List<GameObject>();
    public int idToGive = 0;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void OnPlayerJoined(PlayerInput p){
       p.gameObject.GetComponent<Player>().id = idToGive;
       idToGive++;
    }

    void Update(){
        if(readyCount == 2 && !alreadyStarting){
            alreadyStarting = true;
            SwapPlayerControls();
            SceneManager.LoadScene("Main");
        }
    }

    public void ToggleReady(bool readyState){
        readyCount += readyState ?  1 : - 1;
    }

    void SwapPlayerControls(){
        foreach(GameObject player in players){
            Destroy(player.GetComponent<MenuNavigation>());
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
    }
}
