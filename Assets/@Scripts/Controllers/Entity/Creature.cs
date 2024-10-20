using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Data;
using static Define;

public class Creature : BaseObject
{
    public CreatureData CreatureData { get; private set; }

    #region Stat
    public float Hp { get; set; }
    public CreatureStat MaxHp;
    public CreatureStat MoveSpeed;
    public CreatureStat RunSpeed;
    #endregion

    protected State _state = State.Idle;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        DataTemplateID = templateID;

        if (ObjectType == EObjectType.Hero)
            CreatureData = Managers.Data.HeroDic[templateID];

        Hp = CreatureData.MaxHp;
        MaxHp = new CreatureStat(CreatureData.MaxHp);
        MoveSpeed = new CreatureStat(CreatureData.MoveSpeed);
        RunSpeed = new CreatureStat(CreatureData.RunSpeed);

        _state = State.Idle;
    }

    void Update()
    {
        DetectObjectsInRange();

        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                UpdateAttack();
                UpdateInteract();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
        }
    }

    protected virtual void UpdateInteract() { }
    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttack() { }
    protected virtual void DetectObjectsInRange() { }
}
