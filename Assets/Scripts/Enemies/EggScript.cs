using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D target){
        if (target.gameObject.tag == MyTags.PLAYERTAG){
            //Damage Player
            target.gameObject.GetComponent<Blinking>().DealDamage();
            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
}
