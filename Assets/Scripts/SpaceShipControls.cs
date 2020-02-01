using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipControls : MonoBehaviour
{
    QTEManager qte_Manager;
    PlayerInput playerInput;
    public enum Stance { Idle, Attack , Repair};
    public Stance stance;
    PartsManager partsManager;
    Player player;
    int enemyId;

    void Awake(){
        qte_Manager = GetComponent<QTEManager>();
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        enemyId = player.id == 0 ? 1 : 0;
    }

    void Start(){
        partsManager = PartsManager.instance;
    }

    void StartQTE(){
        playerInput.SwitchCurrentActionMap("QTE");
        qte_Manager.GenerateQTE();
    }

    public void QuitQTE(PartsManager.PARTS targetedPart,int combo){
        playerInput.SwitchCurrentActionMap("Player");
        ExecuteAction(targetedPart,combo);

    }
    void OnAttack(){
        stance = Stance.Attack;
        StartQTE();
    }
    void OnRepair(){
        stance = Stance.Repair;
        StartQTE();
    }

    void ExecuteAction(PartsManager.PARTS targetedPart,int combo){
        int amount = player.actionAmount + combo;
        if(stance == Stance.Attack){
            PartsManager.instance.DamagePart(enemyId,targetedPart,amount);
            //partsManager.DamagePart(enemyId,targetedPart,amount);
        }
        else{
            partsManager.RepairPart(player.id,targetedPart,amount);
        }
    }
}
