
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject fireBullet;

    private PlayerSoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GetComponent<PlayerSoundManager>();
    }
    void Update(){
        ShootBullet();
    }
    void ShootBullet(){
        if(Input.GetKeyDown(KeyCode.F)){
            _soundManager.PlayerAttackSound();
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }
    }
}
   
