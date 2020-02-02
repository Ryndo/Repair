using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MusicJuice : MonoBehaviour
{

    public float offset;
    public float duration;

    // Start is called before the first frame update
    void Start(){
        Sequence s = DOTween.Sequence();
        s.Append(gameObject.GetComponent<Image>().transform.DOLocalMoveX(gameObject.transform.position.x - offset, duration).SetEase(Ease.OutQuint))
        .Append(gameObject.GetComponent<Image>().transform.DOLocalMoveX(gameObject.transform.localPosition.x, duration).SetEase(Ease.OutQuint));
    }
}
