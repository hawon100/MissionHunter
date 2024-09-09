using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameScene : BaseScene
{
	[Inject] private readonly CameraController cameraController;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		SceneType = EScene.GameScene;

		// TODO
		Hero hero = Managers.Object.Spawn<Hero>(new Vector3(0, 5f, 0));
		hero.SetInfo(1);
		cameraController.SetPlayer(hero.gameObject);

        return true;
	}

	public override void Clear()
	{

	}
}
