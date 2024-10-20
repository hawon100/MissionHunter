using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    [Range(0f, 100f)] public float range;
    public string[] targetTags;

    private void Update()
    {
        Detected();
    }

    private void Detected()
    {
        var colliders = Physics.OverlapSphere(transform.position, range);

        foreach (var collider in colliders)
        {
            foreach(var tag in targetTags)
            {
                if (collider.CompareTag(tag))
                {
                    var targetObject = collider.gameObject;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
