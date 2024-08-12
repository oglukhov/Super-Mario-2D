using UnityEngine;

public class BossStoneScript : MonoBehaviour
{
    
    void Start()
    {
        Invoke("Deactivate", 4f);
    }

    void Deactivate(){
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == MyTags.PLAYERTAG){
            
            target.GetComponent<Blinking>().DealDamage();
            gameObject.SetActive(false);
        }
    }
}
