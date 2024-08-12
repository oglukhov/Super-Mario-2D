using System.Data;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;
    private Animator _animator;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private PlayerSoundManager _soundManager;
    

    private bool _isGrounded, _jumped;
    public float _jumpPower = 13f;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _soundManager = GetComponent<PlayerSoundManager>();
    }
    void FixedUpdate(){
        PlayerWalk();
    }
    
    void Update (){
        CheckIfGrounded();
        PlayerJump();
    }

    void PlayerWalk(){
        float h = Input.GetAxisRaw("Horizontal");
        if(h > 0){
            _rb.velocity = new Vector2(speed, _rb.velocity.y);
            ChangeDirection(1);
        } else if (h < 0){
            _rb.velocity = new Vector2(-speed, _rb.velocity.y);
            ChangeDirection(-1);
        } else {
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }

        _animator.SetInteger("Speed", Mathf.Abs((int)_rb.velocity.x));
    }

    void ChangeDirection(int direction){
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded(){
        _isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
        if(_isGrounded){
            if(_jumped){
                _jumped  = false;
                _animator.SetBool("Jump", false);
            }
        }
    
    }
    
    void PlayerJump(){
        if(_isGrounded){
            if(Input.GetKey(KeyCode.Space)){
                _soundManager.PlayJumpSound();
                _jumped = true;
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
                _animator.SetBool("Jump", true);
            }
        }
    }
}
