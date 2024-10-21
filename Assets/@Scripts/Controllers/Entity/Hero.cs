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
    private UI_Hero _uihero;

    private float detectionRadius = 2.0f;  // 구체 범위 반경
    private string[] targetTags = { "Gun", "Melee", "Throw", "Food", "Drink" };   // 찾으려는 오브젝트의 태그 리스트

    public GameObject _First_Main_Weapon;
    public GameObject _Second_Main_Weapon;
    public GameObject _Sub_Weapon;
    public GameObject _Melee_Weapon;
    public GameObject _Throw_Weapon;

    public GameObject _weapon;
    public GameObject _equipment;
    public GameObject _consumable;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _uihero = Util.FindChild(gameObject, "UI_Hero").GetComponent<UI_Hero>();
        cameraController = gameObject.GetOrAddComponent<CameraController>();
        _characterController = gameObject.GetOrAddComponent<CharacterController>();

        var pack = Util.FindChild(Util.FindChild(gameObject, "Body"), "Belongings");
        _weapon = Util.FindChild(pack, "Weapon");
        _equipment = Util.FindChild(pack, "Equipment");
        _consumable = Util.FindChild(pack, "Consumable");

        _First_Main_Weapon = Util.FindChild(_weapon, "First_Main_Weapon");
        _Second_Main_Weapon = Util.FindChild(_weapon, "Second_Main_Weapon");
        _Sub_Weapon = Util.FindChild(_weapon, "Sub_Weapon");
        _Melee_Weapon = Util.FindChild(_weapon, "Melee_Weapon");
        _Throw_Weapon = Util.FindChild(_weapon, "Throw_Weapon");

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _characterController.Move(velocity * Time.deltaTime * RunSpeed.Value);
        }
        else
        {
            _characterController.Move(velocity * Time.deltaTime * MoveSpeed.Value);
        }
    }

    protected override void UpdateInteract()
    {

    }

    protected override void UpdateIdle()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            _state = State.Moving;
        }
    }

    protected override void UpdateAttack()
    {

    }

    protected override void DetectObjectsInRange()
    {
        // 구체 범위 내에 들어온 모든 콜라이더를 찾음
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        bool objectFound = false;

        // 찾은 콜라이더 중 특정 태그를 가진 오브젝트가 있는지 확인
        foreach (var hitCollider in hitColliders)
        {
            if (HasMatchingTag(hitCollider.gameObject))
            {
                // 특정 태그를 가진 오브젝트와 상호작용
                InteractWithObject(hitCollider.gameObject);
                objectFound = true;
                break;  // 여러 오브젝트가 있어도 하나만 처리하려면 break
            }
        }

        // 특정 태그를 가진 오브젝트를 찾지 못했을 경우
        if (!objectFound)
        {
            OnDetectionFailure();
        }
    }

    bool HasMatchingTag(GameObject obj)
    {
        // 오브젝트의 태그가 targetTags 배열 내에 있는지 확인
        foreach (var tag in targetTags)
        {
            if (obj.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    void InteractWithObject(GameObject targetObject)
    {
        // 상호작용 처리 로직
        Debug.Log("Interacting with: " + targetObject.name + " with tag: " + targetObject.tag);

        // 예시: 상호작용으로 오브젝트 비활성화
        // 이곳에 성공 시 필요한 로직 추가
        _uihero.ItemInteractActive(true);

        if (Input.GetKeyDown(KeyCode.F))
        {
            targetObject.transform.parent = _First_Main_Weapon.transform;
            targetObject.transform.rotation = _First_Main_Weapon.transform.rotation;
            targetObject.transform.position = _First_Main_Weapon.transform.position;
        }
    }

    void OnDetectionFailure()
    {
        // 감지 실패 시 처리할 로직
        Debug.Log("No object with the specified tags found within range.");

        // 예시: 다른 처리 (경고 메시지, 범위 내의 다른 동작 등)
        // 이곳에 실패 시 필요한 로직 추가
        _uihero.ItemInteractActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}