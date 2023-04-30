using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HitTriggerFunctions
{
    static public void SunHitSun(HitTrigger thisHit, HitTrigger other)
    {
        GameObject.Destroy(other.gameObject);
    }
}
