using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target){
        if(target.gameObject.tag == MyTags.PLAYERTAG){
            Destroy(gameObject, 0.1f);
        }
    }
}
