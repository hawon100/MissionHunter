using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
	public enum GunType
	{
		None,
		Pistol,
        Assault_Rifle,
        Designated_Marksman_Rifle
    }

	public enum CameraMode
	{
		None,
        First_Person,
		Third_Person,
    }

    public enum EScene
	{
		Unknown,
		TitleScene,
		LoadingScene,
		LobbyScene,
		GameScene,
	}

    public enum EStatModType
    {
        Add,
        PercentAdd,
        PercentMult,
    }

    public enum EUIEvent
	{
		Click,
		PointerDown,
		PointerUp,
		Drag,
	}

	public enum ESound
	{
		Bgm,
		Effect,
		Max,
	}

    public enum EObjectType
    {
        None,
        Env,
        Hero,
		Monster,
		Gun,
    }

    public enum Layer
	{
		None,
		Block = 6,
	}

	public enum State
	{
		None,
		Die,
		Moving,
		Idle,
		Attack,
	}

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
}
