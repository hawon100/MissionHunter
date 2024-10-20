using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Gun_AKM : Gun_AssaultRifle
{
    float time = 5f;
    float curTime = 0f;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            yield return null;

            if (curTime <= 0)
            {
                Destroy(gameObject);
                curTime = 0;
                yield break;
            }
        }
    }
}
