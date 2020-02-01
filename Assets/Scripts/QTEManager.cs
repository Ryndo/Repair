﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    SpaceShipControls spaceShipControls;
    public float QTE_Length = 6;
    public GameObject[] actionsDataBase;
    public List<GameObject> QTE = new List<GameObject>();
    int combo;

    void Awake(){
        spaceShipControls = GetComponent<SpaceShipControls>();
    }

    public void GenerateQTE(){
        List<GameObject> newQTE = new List<GameObject>();
        List<GameObject> actionsPool = new List<GameObject>();
        foreach(GameObject action in actionsDataBase){
            for(int j = 0 ; j < QTE_Length ; j++){
                actionsPool.Add(action);
            }
        }
        for(int i = 0 ; i < QTE_Length ; i++){
            int randomInt = UnityEngine.Random.Range(0,actionsPool.Count);
            newQTE.Add(actionsPool[randomInt]);
            actionsPool.Remove(actionsPool[randomInt]);
        }
        QTE = newQTE;
        DisplayQTE();
    }

    void DisplayQTE(){
        Transform parent = GameManager.instance.QTEZones[gameObject.GetComponent<Player>().id].transform;
        List<Transform> slots = new List<Transform>();
        foreach(Transform slot in parent){
            slots.Add(slot);        
        }
        slots.Reverse();
        List<GameObject> QTE_Reversed = new List<GameObject>(QTE);
        QTE_Reversed.Reverse();
        for(int i = 0; i < QTE.Count ; i++){
            if(slots[i].childCount == 0){
                GameObject touche = Instantiate(QTE_Reversed[i],slots[i].transform.position,slots[i].transform.rotation,slots[i]);
            }
        }       
        for(int i = QTE.Count; i < slots.Count ; i++){
            if(slots[i].transform.childCount > 0){
                Debug.Log(QTE.Count);
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }  
        }
    }

    void OnA(){
        UpdateQte(Action.inputs.A);
    }
    void OnB(){
        UpdateQte(Action.inputs.B);
    }
    void OnY(){
        UpdateQte(Action.inputs.Y);
    }
    void OnX(){
        UpdateQte(Action.inputs.X);
    }
    void OnAimEngine(){
        UpdateTargetedpart(PartsManager.PARTS.ENGINE);    
    }
    void OnAimCockpit(){
        UpdateTargetedpart(PartsManager.PARTS.COCKPIT);    
    }
    void OnAimCannon(){
        UpdateTargetedpart(PartsManager.PARTS.CANNON);    
    }
    void OnAimRepairModule(){
        UpdateTargetedpart(PartsManager.PARTS.REPAIR_MODULE);    
    }
    void UpdateTargetedpart(PartsManager.PARTS part){
        if(QTE.Count == 0){
            spaceShipControls.QuitQTE(part,combo);
            combo = 0;
        }
    }
    void UpdateQte(Action.inputs action){
        if(QTE.Count > 0){
            if(QTE[0].GetComponent<Action>().input == action){
                combo++;
                QTE.RemoveAt(0);
                DisplayQTE();
            }
            else{
                combo = 0;
            }
        }
    }
}

