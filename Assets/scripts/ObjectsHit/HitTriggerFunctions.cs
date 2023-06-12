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

    static public void PortalHitSomething(HitTrigger thisHit, HitTrigger other)
    {
        Portal portal = (Portal)thisHit.AssociatedComponent;
        portal.Teleport(other.gameObject);
    }

    static public void StarHitPlayer(HitTrigger thisHit, HitTrigger other)
    {
        GameObject.Destroy(thisHit.gameObject);
        Player.Instance.StarsCount += 1;
    }

}
