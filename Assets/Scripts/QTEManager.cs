using System.Collections;
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

    void UpdateQte(Action.inputs action){
        if(QTE[0].GetComponent<Action>().input == action){
            QTE.RemoveAt(0);
            if(QTE.Count == 0){
                spaceShipControls.QuitQTE();
            }
        }
        
    }
}

