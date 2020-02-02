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
    public float rotationOffsetY = 5f;
    public float rotationDurationY = 2f;
    public float rotationOffsetZ = 5f;
    public GameObject sonde;

    // Start is called before the first frame update
    void Start(){
        gameObject.transform.DOMoveY(transform.position.y + floatOffset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        gameObject.transform.DOMoveZ(transform.position.z + speedOffset, accelerationDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        gameObject.transform.DORotate(new Vector3(transform.rotation.x,transform.rotation.y + rotationOffsetY,transform.rotation.z + rotationOffsetZ),rotationDurationY).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        sonde.transform.DOMoveY(transform.position.y + floatOffset, duration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        sonde.transform.DOMoveZ(transform.position.z + speedOffset, accelerationDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        sonde.transform.DORotate(new Vector3(transform.rotation.x,transform.rotation.y + rotationOffsetY,transform.rotation.z + rotationOffsetZ),rotationDurationY).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
