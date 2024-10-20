using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_TitleScene : UI_Scene
{
	float time = 3f;
	float curTime = 0f;

	enum Texts
	{
		DisplayText,
	}
   
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindTexts(typeof(Texts));

        GetText((int)Texts.DisplayText).text = $"{(int)time}";

        StartLoadAssets();

		return true;
    }

	void StartLoadAssets()
	{
		Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
		{
			Debug.Log($"{key} {count}/{totalCount}");

			if (count == totalCount)
			{
				Managers.Data.Init();
                GetText((int)Texts.DisplayText).text = $"{time}";
                StartCoroutine(Timer());
            }
        });
	}

	IEnumerator Timer()
	{
		curTime = time;
		while(curTime > 0)
		{
			curTime -= Time.deltaTime;
            GetText((int)Texts.DisplayText).text = $"{(int)curTime}";
            yield return null;

            if (curTime <= 0)
            {
				MapManager.LoadScene(EScene.GameScene);
                curTime = 0;
                yield break;
            }
        }
	}
}
