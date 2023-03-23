using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landable : MonoBehaviour
{
    [SerializeField] GameObject _attached;
    Rigidbody _attachedRb;
    RigidbodyConstraints _nativeFreeze;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_attached)
        {
            _attached = collision.gameObject;
            if (_attached.GetComponentInParent<Landable>())
            {
                _attached = null;
                return;
            }

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
