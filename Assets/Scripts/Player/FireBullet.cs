using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float _speed = 10f;
    private Animator _anim;
    private bool _canMove;
    private EnemySoundManager _enemySoundManager;

    void Awake(){
        _anim = GetComponent<Animator>();
        _enemySoundManager = GetComponent<EnemySoundManager>();
}
    void Start () {
        StartCoroutine(DisableBullet ());
        _canMove = true;
    }

    void Update(){
        Move();
    }

    void Move(){
        if(_canMove) {
            Vector3 temp = transform.position;
            temp.x += _speed * Time.deltaTime;
            transform.position = temp;}
    }
    public float Speed {
        get { return _speed; }
        set { _speed = value; }
    }

    IEnumerator DisableBullet(){
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.gameObject.tag == MyTags.BEETLETAG || target.gameObject.tag ==  MyTags.SNAILTAG || target.gameObject.tag == MyTags.BIRDTAG || target.gameObject.tag == MyTags.SPIDERTAG || target.gameObject.tag == MyTags.FROGTAG || target.gameObject.tag == MyTags.GAMEOBJ){
            _anim.Play("BulletAnim");
            _canMove = false;
            Destroy(gameObject, 0.1f);
        }
        if(target.gameObject.tag == MyTags.BOSSTAG && BossHealth._isInvincible == false){
            _anim.Play("BulletAnim");
            _canMove = false;
            Destroy(gameObject, 0.1f);
        }
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == MyTags.GAMEOBJ)
            {
            _anim.Play("BulletAnim");
            _canMove = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
