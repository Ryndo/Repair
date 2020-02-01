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

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
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
            Debug.Log(player.GetComponent<PlayerInput>().currentActionMap);
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
            Debug.Log(player.GetComponent<PlayerInput>().currentActionMap);
        }
    }
}
