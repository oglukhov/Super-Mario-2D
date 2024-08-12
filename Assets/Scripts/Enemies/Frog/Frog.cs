using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    private Animator _anim;
    public GameObject _player;
    public LayerMask playerLayer;
    private BoxCollider2D _collider;
    public static bool isDead;
    private Text _coinTextScore;

    void Awake(){
        _player = GameObject.FindGameObjectWithTag(MyTags.PLAYERTAG);
        _collider = GetComponentInChildren<BoxCollider2D>();
        _anim = GetComponentInChildren<Animator>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == MyTags.BULLETTAG){
            _anim.Play("FrogDead");
            ScoreScript._score++;
            _coinTextScore.text = "x" + ScoreScript._score.ToString();
            _collider.isTrigger = true;
            StartCoroutine(FrogDead());
            isDead = true;
        }
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer)){
            if(!isDead){
                _player.GetComponent<Blinking>().DealDamage();
                }
        }
    }
    IEnumerator FrogDead(){
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
