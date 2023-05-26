using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitTrigger : MonoBehaviour
{
    [SerializeField] protected HitType _type;
    [SerializeField] int _damage;
    [SerializeField] Component _associatedComponent;
    Action<HitTrigger, HitTrigger> _action;
    public HitType Type { get { return _type; } }
    public int Damage { get { return _damage; } }
    public Component AssociatedComponent { get { return _associatedComponent; } }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<HitTrigger>(out HitTrigger otherHit)) return;
      
        KeyValuePair<HitType, HitType> hitPair = new KeyValuePair<HitType, HitType>(_type, otherHit.Type);
        if (HitPairsDictionary.hitPairAction.TryGetValue(hitPair, out _action)) 
            _action.Invoke(this, otherHit);
    }
}