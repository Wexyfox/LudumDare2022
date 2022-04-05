using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypointTransforms;

    private void Awake()
    {
        waypointTransforms = new Transform[transform.childCount];
        for (int i = 0; i < waypointTransforms.Length; i++)
        {
            waypointTransforms[i] = transform.GetChild(i);
        }
    }
}
