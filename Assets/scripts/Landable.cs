using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landable : MonoBehaviour
{
    [SerializeField] GameObject _attached;
    Rigidbody _attachedRb;
    Rigidbody _rb;
    RigidbodyConstraints _nativeFreeze;
    Collider _collider;
    Collider _attachedCollider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_attached)
        {
            _attached = other.gameObject;
            if (_attached.GetComponentInParent<Landable>())
            {
                _attached = null;
                return;
            }           
            
           /* _attachedCollider = other;
            Vector3 vec = _collider.ClosestPoint(other.bounds.center);
            Debug.Log(vec);
            //other.attachedRigidbody.MovePosition(vec);
            other.transform.position.Set(vec.x, vec.y, 0);*/ // HERE CHANGE TO MAKE PROPER POSITIONING AFTER COLLISION

            Vector3 directionLandingToOtherGo = _attached.transform.position - transform.position;
            transform.up = directionLandingToOtherGo;
            _attachedRb = _attached.GetComponent<Rigidbody>();
            _nativeFreeze = _attachedRb.constraints;
            _attached.transform.SetParent(transform);
            _attachedRb.constraints = RigidbodyConstraints.FreezeAll;
            _attached.transform.up = directionLandingToOtherGo;
        }
    }
   
    public void Release()
    {
        _attachedRb.constraints = _nativeFreeze;
        _attached.transform.parent = null;
        _attached = null;
    }
}
