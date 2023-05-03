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
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.Planet), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.Sun, HitType.LittlePlanet), HitTriggerFunctions.SunHitBurn);
        hitPairAction.Add(new KeyValuePair<HitType, HitType>(HitType.LittlePlanet, HitType.Player), HitTriggerFunctions.LittlePlanetHitPlayer);
    }
}
