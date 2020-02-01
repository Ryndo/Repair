using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour
{

    public enum PARTS{
        ENGINE,
        REPAIR_MODULE,
        COCKPIT,
        CANNON
    }

    public enum DAMAGETYPE{
        HEAL,
        DAMAGE
    }

    public static PartsManager instance;
    public Dictionary<int,Dictionary<PARTS,int>> playersPartsDic = new Dictionary<int, Dictionary<PARTS, int>>();
    [SerializeField]
    public SpaceShip[] spaceShips;

    [Space]
    [Header("PartsDamageUI")]
    public GameObject P1Engine;
    public GameObject P1Cannon;
    public GameObject P1Cockpit;
    public GameObject P1RE;
    [Space]
    public GameObject P2Engine;
    public GameObject P2Cannon;
    public GameObject P2Cockpit;
    public GameObject P2RE;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void InitPlayerParts(){
        for (int i = 0; i < 2; i++){
            playersPartsDic.Add(i, new Dictionary<PARTS, int>());
            playersPartsDic[i][PARTS.ENGINE] = 100;
            playersPartsDic[i][PARTS.REPAIR_MODULE] = 100;
            playersPartsDic[i][PARTS.COCKPIT] = 100;
            playersPartsDic[i][PARTS.CANNON] = 100;
        }
    }



    // Start is called before the first frame update
    void Start(){
        InitPlayerParts();
    }

    public void DamagePart(int playerID, PARTS part, int damages){
        Transform cannon = playerID == 0 ? spaceShips[1].cannon : spaceShips[1].cannon;
        if(part == PARTS.CANNON){
            cannon.LookAt(spaceShips[playerID].cannon.position);
        }
        int damagesToApply = damages;

        bool hasCritted = false;
        bool hasMissed = false;

        //Critical chance
        float critNumber = Random.Range(0,100);
        if(critNumber <= ShipsStats.instance.critPercent){
            print("crit");
            hasCritted = true;
            damagesToApply += ShipsStats.instance.criticalBonusDamages;
            //ShowCrit();
        }

        float hitNumber = Random.Range(0,100);
        //Hit chance
        if(hitNumber >= ShipsStats.instance.hitPercent){
            print("miss");
            hasCritted = false;
            hasMissed = true;
            damagesToApply = 0;
            //ShowMiss();
        }

        playersPartsDic[playerID][part] -= damagesToApply;

        //prepare display
        string damageDisplay = "-"+damagesToApply.ToString();
        if(hasCritted){damageDisplay += "!";}
        if(hasMissed){damageDisplay = "MISS!";}
        //Affect display
        ShowJuice(damageDisplay,PartsManager.DAMAGETYPE.DAMAGE,part, playerID);

        if(playersPartsDic[playerID][part] <= 0){
            GameManager.instance.gameState = GameManager.GAME_STATES.AFTER_GAME;
        }
    }

    public void RepairPart(int playerID, PARTS part, int repairAmount){
        int tmpHealth = playersPartsDic[playerID][part] + repairAmount;
        if(tmpHealth > 100){
            playersPartsDic[playerID][part] = 100;
        }else{
            playersPartsDic[playerID][part] += repairAmount;
        }
        //Affect display
        ShowJuice("+"+repairAmount.ToString(),PartsManager.DAMAGETYPE.HEAL,part, playerID);
    }

    public void ShowJuice(string _text, DAMAGETYPE _damageType, PARTS part, int playerID){
        GameObject partGO = GetPartGameObject(part,playerID);
        partGO.transform.Find("JuiceAction").GetComponent<PartActionDisplay>().damageType = _damageType;
        partGO.transform.Find("JuiceAction").GetComponent<PartActionDisplay>().textToDisplay = _text;
        partGO.transform.Find("JuiceAction").gameObject.SetActive(true);
    }

    public GameObject GetPartGameObject(PARTS part, int playerID){
        GameObject partGO = new GameObject();
        switch(part){
            case PARTS.ENGINE :
                if(playerID == 0){
                    partGO = P1Engine;
                }else if(playerID == 1){
                    partGO = P2Engine;
                }
            break;
            case PARTS.CANNON :
                if(playerID == 0){
                    partGO = P1Cannon;
                }else if(playerID == 1){
                    partGO = P2Cannon;
                }
            break;
            case PARTS.COCKPIT :
                if(playerID == 0){
                    partGO = P1Cockpit;
                }else if(playerID == 1){
                    partGO = P2Cockpit;
                }
            break;
            case PARTS.REPAIR_MODULE :
                if(playerID == 0){
                    partGO = P1RE;
                }else if(playerID == 1){
                    partGO = P2RE;
                }
            break;
        }
        return partGO;
    }

}
[System.Serializable]
public class SpaceShip 
{
    [SerializeField]
    public Transform cockpit;
    [SerializeField]
    public Transform cannon;
    [SerializeField]
    public Transform repairModule;
    [SerializeField]
    public Transform engine;

}
