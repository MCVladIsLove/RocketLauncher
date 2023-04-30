using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour
{
    [SerializeField] protected HitType _type;
    public HitType Type { get { return _type; } }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<HitTrigger>(out HitTrigger otherHit)) return;
        HitPairsDictionary.hitPairAction[new KeyValuePair<HitType, HitType>(_type, otherHit.Type)]?.Invoke(this, otherHit);
    }
}