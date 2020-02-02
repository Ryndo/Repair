using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadRepeater : MonoBehaviour
{

    public float speed = 5f;
    public float limit;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void LateUpdate(){
        if(GameManager.instance.gameState == GameManager.GAME_STATES.IN_GAME){
            gameObject.transform.Translate(new Vector3(0,0,-1) * speed * Time.deltaTime);
            if(gameObject.transform.localPosition.z <= -limit){
                gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,(limit*2));
            }
        }
    }
}
