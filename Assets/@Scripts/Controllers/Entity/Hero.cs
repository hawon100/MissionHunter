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

    public float detectionRadius = 5.0f;  // ��ü ���� �ݰ�
    public string gunLayer = "Gun";   // ã������ ������Ʈ�� �±�
    public LayerMask detectionLayer;      // ������ ���̾� (�ʿ�� ����)

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _uihero = Util.FindChild(gameObject, "UI_Hero").GetComponent<UI_Hero>();
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
        // ��ü ���� ���� ���� ��� �ݶ��̴��� ã��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        bool objectFound = false;

        // ã�� �ݶ��̴� �� Ư�� �±׸� ���� ������Ʈ�� �ִ��� Ȯ��
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(gunLayer))
            {
                // Ư�� �±׸� ���� ������Ʈ�� ��ȣ�ۿ�
                InteractWithObject(hitCollider.gameObject);
                objectFound = true;
                break;  // ���� ������Ʈ�� �־ �ϳ��� ó���Ϸ��� break
            }
        }

        // Ư�� �±׸� ���� ������Ʈ�� ã�� ������ ���
        if (!objectFound)
        {
            OnDetectionFailure();
        }
    }

    void InteractWithObject(GameObject targetObject)
    {
        // ��ȣ�ۿ� ó�� ����
        Debug.Log("Interacting with: " + targetObject.name);

        // ����: ��ȣ�ۿ����� ������Ʈ ��Ȱ��ȭ
        // �̰��� ���� �� �ʿ��� ���� �߰�
        _uihero.ItemInteractActive(true);
    }

    void OnDetectionFailure()
    {
        // ���� ���� �� ó���� ����
        Debug.Log("No object with the tag '" + gunLayer + "' found within range.");

        // ����: �ٸ� ó�� (��� �޽���, ���� ���� �ٸ� ���� ��)
        // �̰��� ���� �� �ʿ��� ���� �߰�
        _uihero.ItemInteractActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}