using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static Define;

public class Hero : Creature
{
    private CameraController cameraController;

    private float _yVelocity;
    private CharacterController _characterController;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        cameraController = gameObject.GetOrAddComponent<CameraController>();
        _characterController = gameObject.GetOrAddComponent<CharacterController>();

        ObjectType = EObjectType.Hero;

        return true;
    }

    public override void SetInfo(int templateID)
    {
        base.SetInfo(templateID);

        _state = State.Moving;
    }

    protected override void UpdateDie() { }

    protected override void UpdateMoving()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(h, 0, v);
        velocity = cameraController.transform.TransformDirection(velocity).normalized;

        _yVelocity -= Time.deltaTime * 9.8f;
        velocity.y = _yVelocity;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _characterController.Move(velocity * Time.deltaTime * RunSpeed.Value);
        }
        else
        {
            _characterController.Move(velocity * Time.deltaTime * MoveSpeed.Value);
        }
    }

    protected override void UpdateIdle()
    {

    }

    protected override void UpdateAttack()
    {
        
    }
}