using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Image progressbar;
    public static string NextScene = "";

    public static void LoadScene(Define.EScene scene)
    {
        NextScene = GetSceneName(scene);
        SceneManager.LoadScene(GetSceneName(Define.EScene.LoadingScene));
    }

    static string GetSceneName(Define.EScene type)
    {
        string name = System.Enum.GetName(typeof(Define.EScene), type);
        return name;
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(NextScene);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;
            if (operation.progress < 0.9f)
            {
                progressbar.fillAmount = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressbar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressbar.fillAmount >= 1f)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
