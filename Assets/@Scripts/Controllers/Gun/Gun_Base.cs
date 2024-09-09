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
    public CreatureStat Intersection;
    public CreatureStat Weight;
    public CreatureStat Recoil;
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ObjectType = EObjectType.Gun;

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
        Intersection = new CreatureStat(GunData.Intersection);
        Weight = new CreatureStat(GunData.Weight);
        Recoil = new CreatureStat(GunData.Recoil);
    }


}
