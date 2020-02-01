using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start(){
        transform.LookAt(mainCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
