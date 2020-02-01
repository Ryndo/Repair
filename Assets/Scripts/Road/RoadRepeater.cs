using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRepeater : MonoBehaviour
{

    public int currentRoadIndex = 1;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        gameObject.transform.Translate(new Vector3(0,0,-1) * speed * Time.deltaTime);
        if(gameObject.transform.position.z <= -90){
            gameObject.transform.localPosition = new Vector3(0,0,90);
        }
    }
}
