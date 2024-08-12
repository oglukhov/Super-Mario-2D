using UnityEngine;

public class FireballStar : MonoBehaviour
{
    public static bool canfire;
    private PlayerSoundManager _soundManager;

    void Awake(){
        _soundManager = GameObject.Find("Player").GetComponent<PlayerSoundManager>();
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.gameObject.tag == MyTags.PLAYERTAG){
            _soundManager.PlayBonusSound();
            Destroy(gameObject, 0.1f);
            GameObject.Find("Player").GetComponent<PlayerShoot>().enabled = true;
            canfire = true;
        }
    }
}
