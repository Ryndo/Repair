using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GAME_STATES{
        BEFORE_GAME,
        IN_GAME,
        AFTER_GAME
    }
    public int winner;
    public GAME_STATES gameState;

    public GameObject[] QTEZones;

    [Space]
    [Header("Ui Hide / Show")]  
    public GameObject[] PlayersUIs;
    public Color[] activePlayersColors;

    public static GameManager instance;
    bool ended;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        switch(gameState){
            case GAME_STATES.BEFORE_GAME :
                //MoveCamera();
                //Countdown();
            break;
            case GAME_STATES.IN_GAME :

            break;
            case GAME_STATES.AFTER_GAME :
                endGame();
            break;

        }
    }
    void endGame(){
        if(!ended){
            ended = true;
            BlackFade.instance.FadeToScene("End");
            
        }
        
    }
}
