using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Gun_Base : BaseObject
{
    public GunType GunType { get; protected set; } = GunType.None;

    public GunData GunData { get; private set; }

    #region Stat
    public string Name;
    public string Ammo;
    public CreatureStat Damage;
    public CreatureStat FireRate;
    public CreatureStat Range;
    public CreatureStat Weight;
    public CreatureStat Recoil;
    public float Current_magazine;
    public CreatureStat Basic_magazine;
    public bool isTakeUp;
    public bool isItemization;
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ObjectType = EObjectType.Gun;
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.Input.MouseAction -= OnMouseEvent;

        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        DataTemplateID = templateID;

        if (ObjectType == EObjectType.Gun)
            GunData = Managers.Data.GunDic[templateID];

        Name = GunData.Name;
        Ammo = GunData.Ammo;
        Damage = new CreatureStat(GunData.Damage);
        FireRate = new CreatureStat(GunData.FireRate);
        Range = new CreatureStat(GunData.Range);
        Weight = new CreatureStat(GunData.Weight);
        Recoil = new CreatureStat(GunData.Recoil);
        Current_magazine = 0;
        Basic_magazine = new CreatureStat(GunData.Basic_magazine);
        isTakeUp = false;
        isItemization = true;
    }

    void Update()
    {
        OnReload();
        OnTakeWeapon();
    }

    void OnReload()
    {
        if (Basic_magazine.Value <= 0) return;

        if (Current_magazine <= 0)
        {
            Reload();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void OnTakeWeapon()
    {
        if (gameObject.GetComponentInParent<Hero>() != null)
        {
            isTakeUp = true;
        }
        else
        {
            isTakeUp = false;
        }
    }

    public virtual void OnMouseEvent(MouseEvent evt)
    {
        switch (evt)
        {
            case MouseEvent.PointerDown:
                break;
            case MouseEvent.PointerUp:
                break;
            case MouseEvent.Press:
                Shoot();
                break;
            case MouseEvent.Click:
                Shoot();
                break;
        }
    }

    public virtual void Shoot() { }
    public virtual void Reload() { }
}
