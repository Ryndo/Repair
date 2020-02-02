using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public enum CDSECONDS{
        THREE,
        TWO,
        ONE,
        GO
    }

    public GameObject texture;

    public CDSECONDS cdSecond;
    public float offset = 20f;
    public Vector3 basePos;
    public bool threep = false;
    public bool twop = false;
    public bool onep = false;
    public bool gop = false;

    public GameObject[] vitals;

    // Start is called before the first frame update
    void Start(){
        basePos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update(){
        switch(cdSecond){
            case CDSECONDS.THREE :
                if(!threep){
                    threep = true;
                    gameObject.GetComponent<TextMeshProUGUI>().text = "3";
                    Sequence s3 = DOTween.Sequence();
                    s3.Append(gameObject.transform.DOMoveY(gameObject.transform.position.y + offset, 1))
                    .Join(gameObject.GetComponent<TextMeshProUGUI>().DOFade(0,1f)).OnComplete(() =>
                        {
                            Reset();
                            cdSecond = CDSECONDS.TWO;
                        });
                }
            break;
            case CDSECONDS.TWO :
                if(!twop){
                    twop = true;
                    gameObject.GetComponent<TextMeshProUGUI>().text = "2";
                    Sequence s2 = DOTween.Sequence();
                    s2.Append(gameObject.transform.DOMoveY(gameObject.transform.position.y + offset, 1))
                    .Join(gameObject.GetComponent<TextMeshProUGUI>().DOFade(0,1f)).OnComplete(() =>
                        {
                            Reset();
                            cdSecond = CDSECONDS.ONE;
                        });
                }
            break;
            case CDSECONDS.ONE :
            if(!onep){
                onep = true;
                gameObject.GetComponent<TextMeshProUGUI>().text = "1";
                Sequence s1 = DOTween.Sequence();
                s1.Append(gameObject.transform.DOMoveY(gameObject.transform.position.y + offset, 1))
                .Join(gameObject.GetComponent<TextMeshProUGUI>().DOFade(0,1f)).OnComplete(() =>
                    {
                        Reset();
                        cdSecond = CDSECONDS.GO;
                    });
            }
            break;
            case CDSECONDS.GO :
            if(!gop){
                gop = true;
                gameObject.GetComponent<TextMeshProUGUI>().text = "GO!";
                texture.GetComponent<RawImage>().DOFade(0,1f);
                ShowVitals();
                GameManager.instance.gameState = GameManager.GAME_STATES.IN_GAME;
                Sequence s = DOTween.Sequence();
                s.Append(gameObject.transform.DOMoveY(gameObject.transform.position.y + offset, 1))
                .Join(gameObject.GetComponent<TextMeshProUGUI>().DOFade(0,1f)).OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                    });
            }
            break;
        }
    }

    public void Reset(){
        gameObject.transform.position = basePos;
        gameObject.GetComponent<TextMeshProUGUI>().color = new Color(255,255,255,1);
    }

    public void ShowVitals(){
        foreach (GameObject g in vitals){
            foreach (Transform t in g.transform){
                t.Find("Health").gameObject.SetActive(true);
            }
        }
    }
}
