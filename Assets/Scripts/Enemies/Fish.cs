using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    private Vector3 _moveDirection = Vector3.down;
    private float _speed;
    private Rigidbody2D _rb;
    private Text _coinTextScore;
    private Animator _anim;
    private bool _isDead;

    private EnemySoundManager _enemySoundManager;

    void Awake(){
        _enemySoundManager = GetComponent<EnemySoundManager>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    void Update(){
        MoveSpider();
    }

    void MoveSpider(){
        transform.Translate(_moveDirection * Time.smoothDeltaTime * _speed);
    }


    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == MyTags.PLAYERTAG && !_isDead){
            target.GetComponent<Blinking>().DealDamage();
        }
        if(target.tag == "FishUp" && !_isDead){
            _moveDirection = Vector3.down;
            _speed = 2f;
            }
        if (target.tag == "FishDown" && !_isDead){
            _moveDirection = Vector3.up;
            _speed = 4f;
        }
        if(target.tag == MyTags.BULLETTAG){
            _enemySoundManager.PlayHurtSound();
            ScoreScript._score++;
            _coinTextScore.text = "x" + ScoreScript._score.ToString();
            _anim.Play("Death");
            _isDead = true;
            
            _rb.bodyType = RigidbodyType2D.Dynamic;
            Destroy(gameObject, 2f);
        }
    }
}
