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
        playersPartsDic[playerID][part] -= damages;
        if(playersPartsDic[playerID][part] <= 0){
            //GameOver();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
