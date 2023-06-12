using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HitPairsDictionary
{
    static public Dictionary<KeyValuePair<HitType, HitType>,  Action<HitTrigger, HitTrigger>> hitPairAction;

    static public void Init()
    {
        hitPairAction = new Dictionary<KeyValuePair<HitType, HitType>, Action<HitTrigger, HitTrigger>>();
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.Sun), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.Player), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.Planet), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.LittlePlanet), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.LittlePlanet, HitType.Player), HitTriggerFunctions.LittlePlanetHitPlayer);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Portal, HitType.Player), HitTriggerFunctions.PortalHitSomething);
        //hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Portal, HitType.Star), HitTriggerFunctions.PortalHitSomething);
        //У star пока нет Rigidbody, поэтому не работает. Напоминание для будущего...
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Star, HitType.Player), HitTriggerFunctions.StarHitPlayer);
    }
}
