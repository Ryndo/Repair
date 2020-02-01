using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShipMovement : MonoBehaviour
{

    public float floatOffset = 1f;
    public float duration = 3f;
    public float speedOffset = 1f;
    public float accelerationDuration = 3f;

    // Start is called before the first frame update
    void Start(){
        gameObject.transform.DOLocalMoveY(transform.position.y + floatOffset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        gameObject.transform.DOLocalMoveX(transform.position.x + speedOffset, accelerationDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
