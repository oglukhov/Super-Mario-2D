using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Blinking : MonoBehaviour
{
   private SpriteRenderer spriteRenderer;
    public static bool isInvincible = false;
    private Text _lifeText;
    private int _lifesScore;
    private PlayerSoundManager _soundManager;
    private BackSoundManager _backManager;
    private Animator _anim;
    private Collider2D _playerColl;
    private Rigidbody2D _rb;
    private Coroutine blinkingCoroutine; // Store reference to the blinking coroutine
    private Color originalColor;
    private bool _isDead; // Store the original color of the sprite

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _playerColl = GetComponent<Collider2D>();
        _soundManager = GetComponent<PlayerSoundManager>();
        _backManager = GameObject.Find("Main Camera").GetComponent<BackSoundManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Initialize the original color
        _lifesScore = 3;
        _lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        _lifeText.text = "x" + _lifesScore;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start() 
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (!isInvincible)
        {
            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine); // Stop the blinking coroutine if it's running
            }
            blinkingCoroutine = StartCoroutine(BlinkingEffect());
            _lifesScore--;
            _soundManager.PlayerHurtSound();

            if (_lifesScore >= 0)
            {
                _lifeText.text = "x" + _lifesScore;
            }
            if (_lifesScore <= 0)
            {
                if (blinkingCoroutine != null)
                {
                    StopCoroutine(blinkingCoroutine); // Stop the blinking coroutine if it's running
                }
                spriteRenderer.color = originalColor; // Reset color to original
                _playerColl.isTrigger = true;
                _rb.velocity = new Vector3(0, 9f);
                _anim.Play("PlayerDead");
                StartCoroutine(LooseSound());
                StartCoroutine(RestartGame());
            }
        }
        isInvincible = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.WATERTAG || target.tag == MyTags.BOSSTAG && !BossHealth._isDead)
        {
            _lifesScore = 0;
            _lifeText.text = "x" + _lifesScore;
            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine); // Stop the blinking coroutine if it's running
            }
            spriteRenderer.color = originalColor; // Reset color to original
            _playerColl.isTrigger = true;
            if(!_isDead){
            _rb.velocity = new Vector3(0, 9f);
            StartCoroutine(LooseSound());}
            
            _anim.Play("PlayerDead");// Reset color to original
            _isDead = true;
            StartCoroutine(RestartGame());
        }
        else if (target.tag == MyTags.HEARTTAG)
        {
            _soundManager.PlayBonusSound();
            _lifesScore++;
            _lifeText.text = "x" + _lifesScore;
        }
    }

    private IEnumerator BlinkingEffect()
    {
        isInvincible = true;

        float blinkDuration = 2.0f;
        float blinkInterval = 0.1f;
        Color transparentColor = originalColor;
        transparentColor.a = 0.2f;

        for (float i = 0; i < blinkDuration; i += blinkInterval * 2)
        {
            spriteRenderer.color = transparentColor;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.color = originalColor;
        isInvincible = false;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("GamePlay");
        isInvincible = false;
        FireballStar.canfire = false;
        ScoreScript._score = 0;
        // Assuming _isDead is defined elsewhere in your code
        _isDead = false;
    }

    IEnumerator LooseSound()
    {
        _backManager.PlayLooseSound();
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0f;
    }
}