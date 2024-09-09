using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Define;

public class BaseObject : InitBase
{
	public EObjectType ObjectType { get; protected set; } = EObjectType.None;

	public int DataTemplateID { get; set; }

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		return true;
	}

	public static Vector3 GetLookAtRotation(Vector3 dir)
	{
		// Mathf.Atan2를 사용해 각도를 계산하고, 라디안에서 도로 변환
		float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;

		// Z축을 기준으로 회전하는 Vector3 값을 리턴
		return new Vector3(0, 0, angle);
	}

	#region Battle
	public virtual void OnDamaged(BaseObject attacker)
	{

	}

	public virtual void OnDead(BaseObject attacker)
	{

	}
	#endregion
}
