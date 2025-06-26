using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrailManager
{

    public static Dictionary<string, object> trailDict;

    public static void setTrail(Dictionary<string, object> trail)
    {
        TrailManager.trailDict = trail;
        Debug.Log("Trail initialized! " + trail);
    }
}
