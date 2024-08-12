using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text _coinTextScore;
    public static int _score;
    private PlayerSoundManager _soundManager;


    void Start (){
        _coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
        _soundManager = GameObject.Find("Player").GetComponent<PlayerSoundManager>();
    }

    void OnTriggerEnter2D (Collider2D target){
        if(target.tag == MyTags.COINTAG)
        {
            _soundManager.PlayCoinSound();
            target.gameObject.SetActive(false);
            _score++;
            _coinTextScore.text = "x" + _score;
        }
    }
}
