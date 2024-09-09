using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region CreatureData
    [Serializable]
    public class CreatureData
    {
		public int DataID;
        public float MaxHp;
        public float MoveSpeed;
        public float RunSpeed;
    }
    #endregion

    #region HeroData
    [Serializable]
	public class HeroData : CreatureData
	{
		
	}

	[Serializable]
	public class HeroDataLoader : ILoader<int, HeroData>
	{
		public List<HeroData> heros = new List<HeroData>();

		public Dictionary<int, HeroData> MakeDict()
		{
			Dictionary<int, HeroData> dict = new Dictionary<int, HeroData>();
			foreach (HeroData heroData in heros)
				dict.Add(heroData.DataID, heroData);

			return dict;
		}
	}
	#endregion

	#region
	[Serializable]
	public class GunData
	{
		public int DataID;
		public string Name;
		public string Ammo;
		public float Damage;
		public float FireRate;
		public float Intersection;
		public float Weight;
		public float Recoil;
    }

    [Serializable]
    public class GunDataLoader : ILoader<int, GunData>
    {
        public List<GunData> gunDatas = new List<GunData>();

        public Dictionary<int, GunData> MakeDict()
        {
            Dictionary<int, GunData> dict = new Dictionary<int, GunData>();
            foreach (GunData gunData in gunDatas)
                dict.Add(gunData.DataID, gunData);

            return dict;
        }
    }
    #endregion
}