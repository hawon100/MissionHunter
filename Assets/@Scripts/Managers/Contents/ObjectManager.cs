using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class ObjectManager
{
	public HashSet<Hero> Heroes { get; } = new HashSet<Hero>();
	public HashSet<Gun_AssaultRifle> Gun_AssaultRifles { get; } = new HashSet<Gun_AssaultRifle>();

	#region Roots
	public Transform GetRootTransform(string name)
	{
		GameObject root = GameObject.Find(name);
		if (root == null)
			root = new GameObject { name = name };

		return root.transform;
	}

	public Transform HeroRoot { get { return GetRootTransform("@Heroes"); } }
	public Transform Gun_AssaultRifleRoot { get { return GetRootTransform("@Gun_AssaultRifles"); } }
	#endregion

	public GameObject SpawnGameObject(Vector3 position, string prefabName)
	{
		GameObject go = Managers.Resource.Instantiate(prefabName, pooling: true);
		go.transform.position = position;
		return go;
	}

	public T Spawn<T>(Vector3 position, Transform parent = null, bool pooling = false) where T : BaseObject
	{
		string prefabName = typeof(T).Name;

		GameObject go = Managers.Resource.Instantiate(prefabName, parent, pooling);
		go.name = prefabName;
		go.transform.position = position;

		BaseObject obj = go.GetComponent<BaseObject>();

		if (obj.ObjectType == EObjectType.Hero)
		{
			obj.transform.parent = HeroRoot;
			Hero hero = go.GetComponent<Hero>();
			Heroes.Add(hero);
		}
		else if(obj.ObjectType == EObjectType.Gun)
		{
			obj.transform.parent = Gun_AssaultRifleRoot;
			Gun_AssaultRifle gun_AssaultRifle = go.GetComponent<Gun_AssaultRifle>();
			Gun_AssaultRifles.Add(gun_AssaultRifle);
		}

		return obj as T;
	}

	public void Despawn<T>(T obj) where T : BaseObject
	{
		EObjectType objectType = obj.ObjectType;

		if (obj.ObjectType == EObjectType.Hero)
		{
			Hero hero = obj.GetComponent<Hero>();
			Heroes.Remove(hero);
		}
		else if(obj.ObjectType == EObjectType.Gun)
		{
            Gun_AssaultRifle gun_AssaultRifle = obj.GetComponent<Gun_AssaultRifle>();
            Gun_AssaultRifles.Remove(gun_AssaultRifle);
        }

        Managers.Resource.Destroy(obj.gameObject);
	}
}
