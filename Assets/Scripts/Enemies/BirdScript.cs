using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private Vector3 _moveDir = Vector3.left;
    private Vector3 _originPos;
    private Vector3 _movePos;
    public GameObject birdEgg, fireBall, heart;
    public LayerMask playerLayer;
    private bool _attacked, _canMove;
    private float _speed = 2.5f;
    private Text _coinTextScore;
    private EnemySoundManager _enemySoundManager;


    void Awake(){
        _enemySoundManager = GetComponent<EnemySoundManager>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }
    void Start(){
        _originPos = transform.position;
        _originPos.x += 5f;

        _movePos = transform.position;
        _movePos.x -= 5f;

        _canMove = true;
    }
    void Update(){
        MoveTheBird();
        DropTheEgg();
    }

    void MoveTheBird(){
        if(_canMove){
            transform.Translate(_moveDir * _speed * Time.smoothDeltaTime);
            if(transform.position.x >= _originPos.x){
                _moveDir = Vector3.left;

                ChangeDir(0.5f);
            }else if(transform.position.x <= _movePos.x){
                _moveDir = Vector3.right;

                ChangeDir(-0.5f);
            }

        }
    }

    void ChangeDir(float direction){
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void DropTheEgg(){
        if(!_attacked){
            if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer)){
                Instantiate(birdEgg, new Vector3 (transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                _attacked = true;
                _anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDeadStar(){  
            Instantiate(fireBall, gameObject.transform.position, Quaternion.identity);
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
            }
        IEnumerator BirdDeadHeart(){
            Instantiate(heart, gameObject.transform.position, Quaternion.identity);
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == MyTags.BULLETTAG || target.tag == MyTags.SNAILTAG){
            _enemySoundManager.PlayHurtSound();
            _anim.Play ("BirdDead");
            ScoreScript._score++;
            _coinTextScore.text = "x" + ScoreScript._score.ToString();
            GetComponent<BoxCollider2D>().isTrigger = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _canMove = false;
            if(FireballStar.canfire == false){
                StartCoroutine(BirdDeadStar()); 
            }else if(FireballStar.canfire == true){
                StartCoroutine(BirdDeadHeart());
            }
        }
        if(target.tag == MyTags.PLAYERTAG){
            GameObject.Find("Player").GetComponent<Blinking>().DealDamage();
        }
    }
}
