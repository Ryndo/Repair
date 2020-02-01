using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartUI : MonoBehaviour
{

    public PartsManager.PARTS part;
    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        gameObject.GetComponent<TextMeshPro>().text = PartsManager.instance.playersPartsDic[playerID][part].ToString();
    }
}
