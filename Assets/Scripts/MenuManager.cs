using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public float readyCount;
    bool alreadyStarting;
    public List<GameObject> players = new List<GameObject>();
    public int idToGive = 0;
    public Image[] PlayersAwaitingIcons;
    public Image[] PlayersJoinedIcons;
    public Image[] PlayersReadyIcons;
    public bool[] playersPressingReady;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        playersPressingReady = new bool[2];
    }
    void Start(){
        foreach(Image image in PlayersAwaitingIcons){
            image.transform.DOMoveY(image.transform.position.y + 3,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
            image.DOFade(.2f,1f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        }
    }

    void OnPlayerJoined(PlayerInput p){
       p.gameObject.GetComponent<Player>().id = idToGive;
       PlayersAwaitingIcons[idToGive].transform.gameObject.SetActive(false);
       PlayersJoinedIcons[idToGive].transform.gameObject.SetActive(true);
       PlayersJoinedIcons[idToGive].transform.DOMoveY(PlayersJoinedIcons[idToGive].transform.position.y + 3,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
       PlayersReadyIcons[idToGive].transform.gameObject.SetActive(true);
       PlayersReadyIcons[idToGive].transform.DOMoveY(PlayersReadyIcons[idToGive].transform.position.y + 3,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
       idToGive++;
       
    }

    void Update(){
        if(readyCount == 2 && !alreadyStarting){
            alreadyStarting = true;
            SwapPlayerControls();
            SceneManager.LoadScene("Main");
        }
            FillIcons();
        
    }

    public void ToggleReady(bool readyState){
        readyCount += readyState ?  1 : - 1;
    }

    void SwapPlayerControls(){
        foreach(GameObject player in players){
            Destroy(player.GetComponent<MenuNavigation>());
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
    }
    public void FillIcons(){
        int tempReadyCount = 0;
        for(int i = 0; i < 2 ; i++){
            if(playersPressingReady[i]){
                PlayersReadyIcons[i].fillAmount += Time.deltaTime / 2;
            }
            else{
                PlayersReadyIcons[i].fillAmount -= Time.deltaTime / 2;
            }
            
            if(PlayersReadyIcons[i].fillAmount == 1){
                tempReadyCount++;
            } 
        }
        readyCount = tempReadyCount;
    }
}
