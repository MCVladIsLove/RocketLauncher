using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HitTriggerFunctions
{
    static public void SunHitBurn(HitTrigger thisHit, HitTrigger other)
    {
        GameObject.Destroy(other.gameObject);
    }
    static public void LittlePlanetHitPlayer(HitTrigger thisHit, HitTrigger other)
    {
        GameObject.Destroy(thisHit.gameObject);
        Player.Instance.TakeDamage(thisHit.Damage);
    }

}
