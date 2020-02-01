using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipControls : MonoBehaviour
{
    QTEManager qte_Manager;
    PlayerInput playerInput;

    void Awake(){
        qte_Manager = GetComponent<QTEManager>();
        playerInput = GetComponent<PlayerInput>();
    }
    void OnShot(){
        playerInput.SwitchCurrentActionMap("QTE");
        qte_Manager.GenerateQTE();
    }

    public void QuitQTE(){
        playerInput.SwitchCurrentActionMap("Player");
    }
}
