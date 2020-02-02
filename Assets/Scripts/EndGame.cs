using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
public class EndGame : MonoBehaviour
{
    public GameObject winner;
    public GameObject instructions;

    void Start(){
        winner.transform.DOMoveY(winner.transform.position.y + 2.5f,1.5f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        instructions.transform.DOMoveY(instructions.transform.position.y + 4,2f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo);
        winner.GetComponentInChildren<TextMeshProUGUI>().text = "Player " + (GameManager.instance.winner + 1) + " win";
        //MenuManager.instance.enabled = true;
        foreach(PlayerInput playerInput in MenuManager.instance.players.Select(x => x.GetComponent<PlayerInput>())){
            playerInput.SwitchCurrentActionMap("EndMenu");
        }
    }
}
