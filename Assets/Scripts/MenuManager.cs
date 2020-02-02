using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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
    public TextMeshProUGUI joinMessage;
    public TextMeshProUGUI holdMessage;
    public Image logo;


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
        joinMessage.transform.DOMoveY(joinMessage.transform.position.y + 1,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        logo.transform.DOMoveY(logo.transform.position.y + 1f,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
    }

    void OnPlayerJoined(PlayerInput p){
       p.gameObject.GetComponent<Player>().id = idToGive;
    StartCoroutine(FadeController());
       PlayersJoinedIcons[idToGive].transform.gameObject.SetActive(true);
       PlayersJoinedIcons[idToGive].transform.DOMoveY(PlayersJoinedIcons[idToGive].transform.position.y + 3,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
       PlayersReadyIcons[idToGive].transform.gameObject.SetActive(true);
       PlayersReadyIcons[idToGive].transform.DOMoveY(PlayersReadyIcons[idToGive].transform.position.y + 3,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
       idToGive++;
       if(idToGive ==2){
           StartCoroutine(FadeMessages());
       }
       
    }

    void Update(){
        if(readyCount == 2 && !alreadyStarting){
            alreadyStarting = true;
            SwapPlayerControls();
            StartCoroutine(FadeCanvas());
        }
        else if(idToGive == 2){
            FillIcons();
        }
        
        
    }
    IEnumerator FadeCanvas(){
        for(int i = 0 ; i <2 ; i++){
            PlayersJoinedIcons[i].gameObject.SetActive(false);
            PlayersReadyIcons[i].DOFade(0,.4f).SetEase(Ease.InOutSine);
            PlayersReadyIcons[i].transform.DOScale(joinMessage.transform.localScale * 2,.4f).SetEase(Ease.InOutSine);
            //PlayersReadyIcons[i].transform.gameObject.SetActive(false);
        }
        holdMessage.transform.DOScale(joinMessage.transform.localScale * 2f,.4f).SetEase(Ease.InOutSine);
        logo.transform.DOScale(joinMessage.transform.localScale * 2f,.4f).SetEase(Ease.InOutSine);
        logo.DOFade(0,.4f).SetEase(Ease.InOutSine).WaitForCompletion();
        holdMessage.DOFade(0,.4f).SetEase(Ease.InOutSine);
        yield return Camera.main.DOFieldOfView(180,1f);
        BlackFade.instance.FadeToScene("main");
        this.enabled = false;
        yield return null;
        
    }
    IEnumerator FadeController(){
        int id = idToGive;
        PlayersAwaitingIcons[id].DOFade(0,.6f).SetEase(Ease.InOutSine);
        yield return PlayersAwaitingIcons[id].transform.DOScale(joinMessage.transform.localScale * 1.4f,.6f).SetEase(Ease.InOutSine).WaitForCompletion();
        PlayersAwaitingIcons[id].transform.gameObject.SetActive(false);
    }
    IEnumerator FadeMessages(){
        joinMessage.transform.DOScale(joinMessage.transform.localScale * 1.4f,.6f).SetEase(Ease.InOutSine);
        yield return joinMessage.DOFade(0,.6f).SetEase(Ease.InOutSine).WaitForCompletion();
        holdMessage.transform.DOMoveY(joinMessage.transform.position.y + 1,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        holdMessage.DOFade(1,.6f).SetEase(Ease.InOutSine);
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
