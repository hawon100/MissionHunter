using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.Third_Person;

    [SerializeField]
    Transform _delta = null;

    [SerializeField]
    GameObject _player = null;

    private Camera_YPoint _camYpoint;
    [SerializeField] private Transform _camPos;
    private Transform _camYPivotPos;
    private float _currentYRotation = 0f;
    private float _minY = -40.0f;
    private float _maxY = 30.0f;
    private bool _isPersonView = false; // true : first | false : third
    private bool _isFirstPerosn = false;
    private bool _isThirdPerosn = false;

    private void Start()
    {
        _camYpoint = Util.FindChild(gameObject, "YPivot").GetOrAddComponent<Camera_YPoint>();
        _camPos = Util.FindChild(Util.FindChild(gameObject), "Main Camera").transform;
        _camYPivotPos = Util.FindChild(gameObject, "YPivot").transform;
  
        _isThirdPerosn = true;
    }

    public void SetPlayer(GameObject player) { _player = player; }
    public void SetTransform(Transform First, Transform Third, Transform Shoulder) { _camYpoint._firstTransform = First; _camYpoint._thirdTransform = Third; _camYpoint._shoulderTransform = Shoulder; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            _isPersonView = !_isPersonView;

            if (_isPersonView)
            {
                _isFirstPerosn = true;
            }
            else if (!_isPersonView)
            {
                _isThirdPerosn = true;
            }
        }

        if(_isPersonView)
        {
            _mode = Define.CameraMode.First_Person;
        }
        else if(!_isPersonView)
        {
            _mode = Define.CameraMode.Third_Person;
        }
    }

    void LateUpdate()
    {
        if (_player != null) transform.position = _player.transform.position;
        HandleCamera();

        switch (_mode)
        {
            case Define.CameraMode.Third_Person:
                SetThirdPersonView(_camYpoint._thirdTransform);
                if(_isThirdPerosn)
                {
                    _camPos.position = _delta.position;
                    _isThirdPerosn = false;
                }
                break;
            case Define.CameraMode.First_Person:
                SetFirstPersonView(_camYpoint._firstTransform);
                if (_isFirstPerosn)
                {
                    _camPos.position = _delta.position;
                    _isFirstPerosn = false;
                }
                break;
        }
    }

    private void HandleCamera()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        // 캐릭터 회전
        transform.Rotate(0, x, 0);

        // 카메라 회전 (현재 Y 회전값에 더해줌)
        _currentYRotation -= y;
        _currentYRotation = Mathf.Clamp(_currentYRotation, _minY, _maxY);

        _camYPivotPos.localEulerAngles = new Vector3(_currentYRotation, _camYPivotPos.localEulerAngles.y, _camYPivotPos.localEulerAngles.z);
    }  

    public void SetFirstPersonView(Transform delta)
    {
        _mode = Define.CameraMode.First_Person;
        _delta = delta;
    }

    public void SetThirdPersonView(Transform delta)
    {
        _mode = Define.CameraMode.Third_Person;
        _delta = delta;
    }
}