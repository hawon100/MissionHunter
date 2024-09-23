using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Rotation : MonoBehaviour
{
    public Transform YPivot;

    private void LateUpdate()
    {
        transform.rotation = YPivot.rotation;
    }
}
