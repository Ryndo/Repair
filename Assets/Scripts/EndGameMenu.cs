using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndGameMenu : MonoBehaviour
{
    void OnReplay(){
        Destroy(GameManager.instance.gameObject);
        MenuManager.instance.players[0].GetComponent<SpaceShipControls>().combo = 0;
        MenuManager.instance.players[1].GetComponent<SpaceShipControls>().combo = 0;
        MenuManager.instance.players[0].GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        MenuManager.instance.players[1].GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        BlackFade.instance.FadeToScene("main");
    }
    void OnMenu(){
        
        Destroy(MenuManager.instance.gameObject);
        Destroy(MenuManager.instance.players[0]);
        Destroy(MenuManager.instance.players[1]);
        BlackFade.instance.FadeToScene("Title Screen");
    }
}
