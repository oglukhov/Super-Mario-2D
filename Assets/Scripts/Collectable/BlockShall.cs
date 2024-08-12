using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockShall : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D target){
        if(target.gameObject.tag == MyTags.SNAILTAG){
            Destroy(gameObject, 0.1f);
            Destroy(target);
        }
    }
}
