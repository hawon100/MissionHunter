using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	private static Managers s_instance;
	private static Managers Instance { get { Init(); return s_instance; } }

    #region Content
	private GameManager _game = new GameManager();
	private ObjectManager _object = new ObjectManager();

	public static GameManager Game { get { return Instance?._game; } }
	public static ObjectManager Object { get { return Instance?._object; } }
    #endregion

    #region Core
    private DataManager _data = new DataManager();
	private PoolManager _pool = new PoolManager();
	private ResourceManager _resource = new ResourceManager();
	private SceneManagerEx _scene = new SceneManagerEx();
	private SoundManager _sound = new SoundManager();
	private UIManager _ui = new UIManager();
	private InputManager _input = new InputManager();

	public static DataManager Data { get { return Instance?._data; } }
	public static PoolManager Pool { get { return Instance?._pool; } }
	public static ResourceManager Resource { get { return Instance?._resource; } }
	public static SceneManagerEx Scene { get { return Instance?._scene; } }
	public static SoundManager Sound { get { return Instance?._sound; } }
	public static UIManager UI { get { return Instance?._ui; } }
	public static InputManager Input { get { return Instance?._input; } }
	#endregion


	public static void Init()
	{
		if (s_instance == null)
		{
			GameObject go = GameObject.Find("@Managers");
			if (go == null)
			{
				go = new GameObject { name = "@Managers" };
				go.AddComponent<Managers>();
			}

			DontDestroyOnLoad(go);

			// 초기화
			s_instance = go.GetComponent<Managers>();
		}
	}

    private void Update()
    {
        _input.OnUpdate();
    }
}
