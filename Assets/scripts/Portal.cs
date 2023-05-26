using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal _teleportTo;
    Rigidbody _otherObjRb;

    public void Teleport(GameObject objToTeleport)
    {
        if (_teleportTo != null)
        {
            objToTeleport.transform.position = _teleportTo.gameObject.transform.position;
            _otherObjRb = objToTeleport.GetComponent<Rigidbody>();
            _otherObjRb.velocity = _teleportTo.transform.up * _otherObjRb.velocity.magnitude;

            // Оно надо??? Запутывает сильно
            /*rot = rot / 180 * Mathf.PI;
            Debug.Log(rot);
            float cos = Mathf.Cos(rot);
            float sin = Mathf.Sin(rot);
            float x = _otherObjRb.velocity.x * cos - _otherObjRb.velocity.y * sin;
            float y = _otherObjRb.velocity.y * cos + _otherObjRb.velocity.x * sin;

            _otherObjRb.velocity = new Vector3(x, y, _otherObjRb.velocity.z);*/
        }
    }
}
