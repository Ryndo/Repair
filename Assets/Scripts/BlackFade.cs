using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BlackFade : MonoBehaviour
{
    Animator animator;
    string sceneToLoad;
    public static BlackFade instance;

    void Awake(){
        if(instance == null && instance != this){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
        animator = GetComponent<Animator>();
    }

    public void FadeToScene(string scene){
        sceneToLoad = scene;
        animator.SetTrigger("Transition");
    }
    void LoadSceneOnFadeOutComplete(){
        SceneManager.LoadScene(sceneToLoad);
    }
}
