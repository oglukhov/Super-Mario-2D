using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnim : MonoBehaviour
{
    private Animator _anim;
    private bool _animStarted;
    private bool _animFinished;
    private int _jumpTimes;
    private bool _jumpLeft = true;
    private string coroutineName = "FrogJump";

    void Awake(){
        _anim = GetComponentInChildren<Animator>();
    }
    void Start(){
        StartCoroutine(coroutineName);
    }

    void Update(){
        if(Frog.isDead){
            StopCoroutine(coroutineName);
        }
    }

    void LateUpdate(){
        if(_animFinished && _animStarted){
            _animStarted = false;

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump(){
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        
        _animStarted = true;
        _animFinished = false;

        _jumpTimes++;
        
        if(_jumpLeft){
            _anim.Play ("FrogJumpLeft");
        }else {
            _anim.Play ("FrogJumpRight");
        }
        StartCoroutine(coroutineName);
    }
    void AnimationFinished(){
        _animFinished = true;

        if(_jumpLeft){
            _anim.Play("FrogIdleLeft");
        }else {
            _anim.Play("FrogIdleRight");
        }


        if(_jumpTimes == 3){
            _jumpTimes = 0;
            
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            _jumpLeft = !_jumpLeft;
        }
    }  
    
}
