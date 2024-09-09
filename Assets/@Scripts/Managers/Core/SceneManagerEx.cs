using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	public void LoadScene(Define.EScene type)
	{
		SceneManager.LoadScene(GetSceneName(type));
	}

	private string GetSceneName(Define.EScene type)
	{
		string name = System.Enum.GetName(typeof(Define.EScene), type);
		return name;
	}
}
