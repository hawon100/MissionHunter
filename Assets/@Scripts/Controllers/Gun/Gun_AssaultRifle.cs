using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;

public class Gun_AssaultRifle : Gun_Base
{
    public CameraController fpsCam;
    public Transform attackPoint;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        GunType = GunType.Assault_Rifle;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);
    }

    public override void Shoot()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(fpsCam._cam.transform.position, fpsCam._cam.transform.forward, out hitInfo, Range.Value))
        {
            Debug.DrawRay(fpsCam._cam.transform.position, fpsCam._cam.transform.forward, Color.red, Range.Value);
        }
    }

    public override void Reload()
    {

    }
}
