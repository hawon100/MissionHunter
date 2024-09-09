using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
	public Dictionary<int, HeroData> HeroDic { get; private set; } = new Dictionary<int, HeroData>();
	public Dictionary<int, GunData> GunDic { get; private set; } = new Dictionary<int, GunData>();

	public void Init()
	{
		HeroDic = LoadJson<HeroDataLoader, int, HeroData>("HeroData").MakeDict();
        GunDic = LoadJson<GunDataLoader, int, GunData>("GunData").MakeDict();
	}

	private Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
		return JsonConvert.DeserializeObject<Loader>(textAsset.text);
	}
}
