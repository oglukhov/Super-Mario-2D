using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;
    private Animator _anim;
    private string _coroutineName = "StartAttack";
    private BossSoundManager _bossSoundManager;

    void Awake(){
        _anim = GetComponent<Animator>();
        _bossSoundManager = GetComponent<BossSoundManager>();
    }
void Start(){
    StartCoroutine(_coroutineName);
}

void BackToIdle(){
    _anim.Play("BossIdle");
}

public void DeactivateBossScript(){
    StopCoroutine(_coroutineName);
    enabled = false;
}

void Attack(){
    _bossSoundManager.PlayThrow();
    GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity); 
    obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
}

IEnumerator StartAttack(){
    yield return new WaitForSeconds(Random.Range(2f, 5f));

    _anim.Play("BossAttack");
    StartCoroutine(_coroutineName);
}
}
