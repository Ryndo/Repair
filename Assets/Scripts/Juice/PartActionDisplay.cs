using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PartActionDisplay : MonoBehaviour
{


    public PartsManager.DAMAGETYPE damageType;

    public string textToDisplay;
    public float yOffset;
    public float duration;
    public Color damageColor;
    public Color healColor;
    public Vector3 basePosition;

    // Start is called before the first frame update
    void OnEnable(){
        basePosition = transform.GetComponent<RectTransform>().position;
        gameObject.GetComponent<TextMeshPro>().text = textToDisplay;
        switch(damageType){
            case PartsManager.DAMAGETYPE.HEAL : 
                gameObject.GetComponent<TextMeshPro>().color = healColor;
            break;
            case PartsManager.DAMAGETYPE.DAMAGE : 
                gameObject.GetComponent<TextMeshPro>().color = damageColor;
            break;
        }
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMoveY(transform.GetComponent<RectTransform>().position.y + yOffset, duration))
        .Join(GetComponent<TextMeshPro>().DOFade(0,duration)).OnComplete(() =>
            {
                AutoDeactivate();
            });
    }

    public void AutoDeactivate(){
        gameObject.GetComponent<RectTransform>().position = basePosition;
        gameObject.GetComponent<TextMeshPro>().color = new Color(255,255,255,1);
        gameObject.SetActive(false);
    }

}
