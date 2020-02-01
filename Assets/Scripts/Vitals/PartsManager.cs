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

    public static PartsManager instance;
    public Dictionary<int,Dictionary<PARTS,int>> playersPartsDic = new Dictionary<int, Dictionary<PARTS, int>>();

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
        int damagesToApply = damages;

        //Critical chance
        float critNumber = Random.Range(0,100);
        if(critNumber <= ShipsStats.instance.critPercent){
            print("crit");
            damagesToApply += ShipsStats.instance.criticalBonusDamages;
            //ShowCrit();
        }

        float hitNumber = Random.Range(0,100);
        //Hit chance
        if(hitNumber >= ShipsStats.instance.hitPercent){
            print("miss");
            damagesToApply = 0;
            //ShowMiss();
        }

        print(damagesToApply + " damages");

        playersPartsDic[playerID][part] -= damagesToApply;
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
    }

}
