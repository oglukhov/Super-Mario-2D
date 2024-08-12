using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySound : MonoBehaviour
{
    private BackSoundManager _bossSoundManager;
    void Awake(){
        _bossSoundManager = GameObject.Find("Main Camera").GetComponent<BackSoundManager>();
    }
    void OnTriggerEnter2D (Collider2D target){

        if (target.gameObject.tag == MyTags.PLAYERTAG){
            _bossSoundManager.PlayVictorySound();
        }
    }
}
