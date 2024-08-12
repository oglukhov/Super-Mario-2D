using UnityEngine;
using UnityEngine.UI;

public class BonusBlok : MonoBehaviour
{
    public Transform bottomCollision;

    private Animator _anim;
    public LayerMask playerLayer;
    private Vector3 _moveDir = Vector3.up;
    private Vector3 _originPos;
    private Vector3 _animPos;
    private bool _startAnim;
    private bool _canAnimate = true;
    private Text _coinTextScore;
    private PlayerSoundManager _soundManager;

    void Awake(){
        _soundManager = GameObject.Find("Player").GetComponent<PlayerSoundManager>();
        _anim = GetComponent<Animator>();
    }
    void Start(){
        _originPos = transform.position;
        _animPos = transform.position;
        _animPos.y += 0.15f;
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    void Update(){
        CheckForCollision();
        AnimateUpDown();

    }
    void CheckForCollision(){
        RaycastHit2D hit = Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, playerLayer);
        if(_canAnimate){
            if(hit){
                if(hit.collider.gameObject.tag == MyTags.PLAYERTAG){
                    //increase score
                    _soundManager.PlayCoinSound();
                    ScoreScript._score++;
                    _coinTextScore.text = "x" + ScoreScript._score.ToString();
                    _anim.Play("IdleBonus");
                    _startAnim = true;
                    _canAnimate = false;
                    }
                }
        }
    }

    void AnimateUpDown(){
        if(_startAnim){
            transform.Translate(_moveDir * Time.smoothDeltaTime);
            if(transform.position.y >= _animPos.y){
                _moveDir = Vector3.down;
            }else if(transform.position.y <= _originPos.y){
                _startAnim = false;
            }
        }
    }
}

