using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class GravitySystemObject : MonoBehaviour
{
    [SerializeField] Vector3 _startForceDirection;
    [SerializeField] bool _pullable;
    [SerializeField] Rigidbody _rotateAroundGameObject;
    public bool Pullable { get { return _pullable; }
        set { if (gameObject.GetComponent<Rigidbody>()) _pullable = value; } }

    [SerializeField] bool _magnetic;
    public bool Magnetic { get { return _magnetic; } set { _magnetic = value; } }

    GravityManagement _gravityManager;
    Rigidbody _rb;
    void Start()
    {
        _gravityManager = GravityManagement.Instance;
        EnableGravitation(_pullable, _magnetic);
        _rb = GetComponent<Rigidbody>();
        if (_rotateAroundGameObject)
        {
            Vector3 perpendicular = (_rotateAroundGameObject.position - _rb.position).normalized;
            perpendicular = new Vector3(-perpendicular.y, perpendicular.x);
            perpendicular = Random.Range(0, 2) == 1 ? perpendicular : -perpendicular;
            float force = GravityManagementUtils.GetForceBetween(_rb, _rotateAroundGameObject, true);
            float distance = (_rb.position - _rotateAroundGameObject.position).magnitude;
            float velocity = Mathf.Sqrt(force * distance / _rb.mass);

            _rb.AddForce(perpendicular * velocity, ForceMode.VelocityChange);
        }
        else
            _rb.AddForce(_startForceDirection, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (_magnetic)
            _gravityManager.MagnetizeAll(_rb);
    }

    public void DisableGravitation()
    {
        _gravityManager.RemoveFromGravitySystem(this);
        _pullable = false;
        _magnetic = false;
    }

    public void EnableGravitation(bool pullable, bool magnetic)
    {
        _gravityManager.AddToGravitySystem(this);
        _pullable = pullable;
        _magnetic = magnetic;
    }

    void OnDestroy()
    {
        if (_gravityManager != null)
            DisableGravitation();
    }

}
