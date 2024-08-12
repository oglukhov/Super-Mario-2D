using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;
    public Bounds cameraBounds;

    private Transform _target;
    private float _offsetZ;
    private Vector3 _lastTargetPosition;
    private Vector3 _currentVelocity;

    private bool _followsPlayer;

    void Awake(){
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCol.bounds;
    }

    void Start(){
        _target = GameObject.FindGameObjectWithTag(MyTags.PLAYERTAG).transform;
        _lastTargetPosition = _target.position;
        _offsetZ = (transform.position - _target.position).z;
        _followsPlayer = true;
    }

    void FixedUpdate(){
        if(_followsPlayer){
            Vector3 aheadTargetPos = _target.position + Vector3.forward * _offsetZ;
            if(aheadTargetPos.x >= transform.position.x) {
                Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref _currentVelocity, cameraSpeed);
                transform.position = new Vector3 (newCameraPosition.x, transform.position.y, newCameraPosition.z);
                _lastTargetPosition = _target.position;
            }
        }
    }
}
