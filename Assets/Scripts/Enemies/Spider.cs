using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spider : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector3 _moveDirection = Vector3.down;
    private string coroutineName = "ChangeMovement";
    private Text _coinTextScore;
    private bool _isDead;
    private EnemySoundManager _enemySoundManager;

    void Awake(){
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
        _enemySoundManager = GetComponent<EnemySoundManager>();
    }
    void Start(){
        StartCoroutine(coroutineName);
    }

    void Update(){
        MoveSpider();
    }

    void MoveSpider(){
        transform.Translate(_moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement(){
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if(_moveDirection == Vector3.down){
            _moveDirection = Vector3.up;
        }else{
            _moveDirection = Vector3.down;
        }
        StartCoroutine (coroutineName);
    }

    IEnumerator SpiderDead(){
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == MyTags.BULLETTAG || target.tag == MyTags.SNAILTAG){
            _enemySoundManager.PlayHurtSound();
            ScoreScript._score++;
            _coinTextScore.text = "x" + ScoreScript._score.ToString();
            _anim.Play("SpiderDead");
            _isDead = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(SpiderDead());
            StopCoroutine(coroutineName);
        }
        if(target.tag == MyTags.PLAYERTAG && !_isDead){
            target.GetComponent<Blinking>().DealDamage();
        }
    }
}
