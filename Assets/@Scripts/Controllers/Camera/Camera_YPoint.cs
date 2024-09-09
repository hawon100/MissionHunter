using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_YPoint : MonoBehaviour
{
    public Transform _firstTransform;
    public Transform _thirdTransform;
    public Transform _shoulderTransform;

    void Start()
    {
        _firstTransform = Util.FindChild(gameObject, "First").transform;
        _thirdTransform = Util.FindChild(gameObject, "Third").transform;
        _shoulderTransform = Util.FindChild(gameObject, "Shoulder").transform;
    }
}
