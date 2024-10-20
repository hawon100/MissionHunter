using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		SceneType = EScene.GameScene;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// TODO
		Hero hero = Managers.Object.Spawn<Hero>(new Vector3(0, 5f, 0));
		hero.SetInfo(1);
        Gun_AKM gun_akm = Managers.Object.Spawn<Gun_AKM>(new Vector3(1f, 0f, 1f));
        gun_akm.SetInfo(50001);
        gun_akm.fpsCam = hero.gameObject.GetComponent<CameraController>();

        return true;
	}

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
		{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
		else
		{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public override void Clear()
	{

	}
}
