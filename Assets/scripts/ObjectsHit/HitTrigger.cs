using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour
{
    [SerializeField] protected HitType _type;
    Action<HitTrigger, HitTrigger> _action;
    public HitType Type { get { return _type; } }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<HitTrigger>(out HitTrigger otherHit)) return;
      
        KeyValuePair<HitType, HitType> hitPair = new KeyValuePair<HitType, HitType>(_type, otherHit.Type);
        if (HitPairsDictionary.hitPairAction.TryGetValue(hitPair, out _action)) 
            _action.Invoke(this, otherHit);
    }
}