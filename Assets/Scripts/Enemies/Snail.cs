using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Snail : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D _rb;
    private Animator _anim;

    public LayerMask playerLayer, groundLayer;
    private bool _moveLeft, _canMove, _stunned;
    public Transform downCollision, leftCollision, rightCollision, topCollision;
    private Vector3 leftCollisionLocalPosition, rightCollisionLocalPosition;
    private Text _coinTextScore;
    private bool _isDead;
    private EnemySoundManager _enemySoundManager;
    void Awake(){
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
        leftCollisionLocalPosition = leftCollision.localPosition;
        rightCollisionLocalPosition = rightCollision.localPosition;
        _enemySoundManager = GetComponent<EnemySoundManager>();
    }
    void Start(){
        _moveLeft = true;
        _canMove = true;
    }

    void Update(){
        if(_canMove){
            if(_moveLeft){
            _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
            }else
            {
                _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);
            }
        }
        CheckCollision();
    }

    void CheckCollision(){
        RaycastHit2D leftHit = Physics2D.Raycast (leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast (rightCollision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if(topHit != null){
            if(topHit.gameObject.tag == MyTags.PLAYERTAG && !Blinking.isInvincible){
                if(!_stunned){
                    _enemySoundManager.PlayHurtSound();
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    _canMove = false;
                    _rb.velocity = new Vector2(0, 0);
                    _anim.Play ("Stunned"); 
                    _stunned = true;
                        if(tag == MyTags.SNAILTAG){
                            ScoreScript._score++;
                            _coinTextScore.text = "x" + ScoreScript._score.ToString();
                        }

                    if(tag == MyTags.BEETLETAG){
                        _enemySoundManager.PlayHurtSound();
                        Destroy(gameObject, 0.5f);
                        ScoreScript._score++;
                        _coinTextScore.text = "x" + ScoreScript._score.ToString();
                    }
                }
            }
        }   

        if(leftHit){
            if(leftHit.collider.gameObject.tag == MyTags.PLAYERTAG && !Blinking.isInvincible){
                if(!_stunned && !_isDead){
                    //Damage player
                    leftHit.collider.gameObject.GetComponent<Blinking>().DealDamage();
                }else if(tag != MyTags.BEETLETAG) {
                    _rb.velocity = new Vector2 (15f, _rb.velocity.y);
                    Destroy(gameObject, 4f);
                }

            }
        }

        if(rightHit){
            if(rightHit.collider.gameObject.tag == MyTags.PLAYERTAG && !Blinking.isInvincible){
                if(!_stunned && !_isDead){
                    //Damage player
                    rightHit.collider.gameObject.GetComponent<Blinking>().DealDamage();
                }else {
                    if(tag != MyTags.BEETLETAG){
                        _rb.velocity = new Vector2 (-15f, _rb.velocity.y);
                        Destroy(gameObject, 1f);
                    }
                }

            }
        }

        if(!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f, groundLayer)){
            ChangeDirection();
        }
    }

    
    void ChangeDirection(){
        _moveLeft = !_moveLeft;

        Vector3 tempScale = transform.localScale;
        if(_moveLeft){
            tempScale.x = Mathf.Abs(tempScale.x);
        }else {
            tempScale.x = -Math.Abs(tempScale.x);
        }

        leftCollision.localPosition = _moveLeft ? leftCollisionLocalPosition : rightCollisionLocalPosition;
        rightCollision.localPosition = _moveLeft ? rightCollisionLocalPosition : leftCollisionLocalPosition;

        transform.localScale = tempScale;
    }    

    void OnTriggerEnter2D (Collider2D target){
        if(target.gameObject.tag == MyTags.BULLETTAG){
            if(tag == MyTags.BEETLETAG){
                _enemySoundManager.PlayHurtSound();
                ScoreScript._score++;
                _coinTextScore.text = "x" + ScoreScript._score.ToString();
                _anim.Play("Stunned");
                _canMove = false;
                _isDead = true;
                _rb.velocity = Vector3.zero;
                Destroy(gameObject, 0.5f);
            }
            else if(tag == MyTags.SNAILTAG){
                if(!_stunned){
                    _enemySoundManager.PlayHurtSound();
                    ScoreScript._score++;
                    _coinTextScore.text = "x" + ScoreScript._score.ToString();
                    _anim.Play("Stunned");
                    _stunned = true;
                    _canMove = false;
                    _rb.velocity = Vector3.zero;
                }else {
                    Destroy(gameObject, 0.5f);
                }
                
            }
        }
    }
}
