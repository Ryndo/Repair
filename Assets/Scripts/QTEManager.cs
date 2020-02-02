using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        ModifyPlayerUI();     

        for(int i = QTE.Count; i < slots.Count ; i++){
            if(slots[i].transform.childCount > 0){
                //Debug.Log(QTE.Count);
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }  
        }
    }

    public void ModifyPlayerUI(){
        Transform playerUI = GameManager.instance.PlayersUIs[gameObject.GetComponent<Player>().id].transform;

        //Show QTE OPEN
        playerUI.Find("QTEOpen").gameObject.SetActive(true);
        TextMeshProUGUI combo = playerUI.Find("Combo").GetComponent<TextMeshProUGUI>();
        combo.text = "x" + spaceShipControls.combo.ToString();
        //Gray out unused icon
        switch(gameObject.GetComponent<SpaceShipControls>().stance){
            case SpaceShipControls.Stance.Attack :
                playerUI.transform.Find("QTEOpen/IconAttack").GetComponent<Image>().color = GameManager.instance.activePlayersColors[gameObject.GetComponent<Player>().id];
                playerUI.transform.Find("QTEOpen/IconHeal").GetComponent<Image>().color = new Color(255,255,255,.15f);
            break;
            case SpaceShipControls.Stance.Repair :
                playerUI.transform.Find("QTEOpen/IconHeal").GetComponent<Image>().color = GameManager.instance.activePlayersColors[gameObject.GetComponent<Player>().id];
                playerUI.transform.Find("QTEOpen/IconAttack").GetComponent<Image>().color = new Color(255,255,255,.15f);
            break;
        }
        
    }
    void UpdateCombo(){
        GameManager.instance.PlayersUIs[gameObject.GetComponent<Player>().id]
        .transform.Find("Combo").GetComponent<TextMeshProUGUI>().text = spaceShipControls.combo.ToString();
    }

    public void ReversePlayerUi(){
        Transform playerUI = GameManager.instance.PlayersUIs[gameObject.GetComponent<Player>().id].transform;

        //Hide QTE OPEN
        playerUI.Find("QTEOpen").gameObject.SetActive(false);
    }

    void OnA(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateQte(Action.inputs.A);
        }
    }
    void OnB(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateQte(Action.inputs.B);
        }
    }
    void OnY(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateQte(Action.inputs.Y);
        }
    }
    void OnX(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateQte(Action.inputs.X);
        }
    }
    void OnAimEngine(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateTargetedpart(PartsManager.PARTS.ENGINE);
        }
    }
    void OnAimCockpit(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateTargetedpart(PartsManager.PARTS.COCKPIT);   
        }
    }
    void OnAimCannon(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateTargetedpart(PartsManager.PARTS.CANNON); 
        }
    }
    void OnAimRepairModule(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            UpdateTargetedpart(PartsManager.PARTS.REPAIR_MODULE);  
        }
    }
    void UpdateTargetedpart(PartsManager.PARTS part){
        if(QTE.Count == 0){
            HideAimMessag();
            ReversePlayerUi();
            UpdateCombo();
            spaceShipControls.QuitQTE(part);
            UpdateCombo();
        }
    }
    void UpdateQte(Action.inputs action){
        if(QTE.Count > 0){
            if(QTE[0].GetComponent<Action>().input == action){
                spaceShipControls.combo++;
                QTE.RemoveAt(0);
                DisplayQTE();
            }
            else{
                spaceShipControls.combo = 0;
            }
            if(QTE.Count == 0){
                ShowAimMessage();
            }
            
        }
        else{
            
        }
        UpdateCombo();
    }

    public void ShowAimMessage(){
        Transform playerUI = GameManager.instance.PlayersUIs[gameObject.GetComponent<Player>().id].transform;
        playerUI.Find("QTEOpen/Message").gameObject.SetActive(true);
    }

    public void HideAimMessag(){
        Transform playerUI = GameManager.instance.PlayersUIs[gameObject.GetComponent<Player>().id].transform;
        playerUI.Find("QTEOpen/Message").gameObject.SetActive(false); 
    }
}

