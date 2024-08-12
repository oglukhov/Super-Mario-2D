using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Animator _anim;
    private int _health = 10; 
    private SpriteRenderer _spriteRenderer;

    private bool _canDamage;
    private Text _coinTextScore;
    private BossSoundManager _bossSoundManager;
    public static bool _isDead;
    public static bool _isInvincible;
    private BackSoundManager _backSoundManager;
    private Coroutine blinkingCoroutine; // Store reference to the blinking coroutine
    private Color originalColor; // Store the original color of the sprite

    void Awake(){
        _backSoundManager = GameObject.Find("Main Camera").GetComponent<BackSoundManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color; // Initialize the original color
        _bossSoundManager = GetComponent<BossSoundManager>();
        _anim = GetComponent<Animator>();
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
        _canDamage = true;
    }

    IEnumerator WaitForDamage(){
        yield return new WaitForSeconds(2f);
        _canDamage = true;
    }
    
    void OnTriggerEnter2D(Collider2D target){
        if(_canDamage){
            if(target.tag == MyTags.BULLETTAG && !_isInvincible && !_isDead){
                if(_health >= 1){
                    if (blinkingCoroutine != null)
                    {
                        StopCoroutine(blinkingCoroutine); // Stop the blinking coroutine if it's running
                    }
                    blinkingCoroutine = StartCoroutine(BlinkingEffect());
                }
                _bossSoundManager.PlayHurt();
                _health--;
                ScoreScript._score++;
                _coinTextScore.text = "x" + ScoreScript._score.ToString();

                _canDamage = false;
                if(_health == 0){
                    if (blinkingCoroutine != null)
                    {
                        StopCoroutine(blinkingCoroutine); // Stop the blinking coroutine if it's running
                    }
                    _backSoundManager.PlayBackSound();
                    _spriteRenderer.color = originalColor; // Reset color to original
                    _isDead = true;
                    _bossSoundManager.PlayDead();
                    _anim.Play("BossDead");
                    gameObject.GetComponent<BossScript>().DeactivateBossScript();
                    Destroy(gameObject, 3f);
                }
                StartCoroutine(WaitForDamage());
            }
        }
    }

    private IEnumerator BlinkingEffect()
    {
        _isInvincible = true;

        float blinkDuration = 2.0f;
        float blinkInterval = 0.1f;
        Color transparentColor = originalColor;
        transparentColor.a = 0.2f;

        for (float i = 0; i < blinkDuration; i += blinkInterval * 2)
        {
            _spriteRenderer.color = transparentColor;
            yield return new WaitForSeconds(blinkInterval);
            _spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
        }

        _spriteRenderer.color = originalColor;
        _isInvincible = false;
    }
}