using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class GravitySystemObject : MonoBehaviour
{
    [SerializeField] protected Vector3 _startForceDirection;
    [SerializeField] protected Rigidbody _rotateAroundGameObject;

    protected GravityManagement _gravityManager;
    protected Rigidbody _rb;
    public Rigidbody Rigidbody { get { return _rb; } }

    protected LinkedList<GravitySystemObject> _objectsToPull;

    virtual protected void Start()
    {
        _gravityManager = GravityManagement.Instance;
        EnableGravitation();
    }
    virtual protected void Awake()
    {
        _objectsToPull = new LinkedList<GravitySystemObject>();
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

    public void PullObjects()
    {
        foreach (GravitySystemObject pulled in _objectsToPull)
        {
            if (pulled.gameObject.activeInHierarchy && 
                GravityManagementUtils.ObjectsCanGravitate(pulled.Rigidbody.position, _rb.position))
            {
                _gravityManager.Pull(pulled.Rigidbody, _rb);
            }
        }
    }

    public bool CanPullObject(GravitySystemObject go)
    {
        return _objectsToPull.Contains(go);
    }

    public void DisableGravitation()
    {
        _gravityManager.RemoveFromGravitySystem(this);
    }

    public void EnableGravitation()
    {
        _gravityManager.AddToGravitySystem(this);
    }

    public void AddToPulledObjects(GravitySystemObject pulled)
    {
        if (pulled != this)
            _objectsToPull.AddLast(pulled);
    }
    public void RemoveFromPulledObjects(GravitySystemObject pulled)
    {
        _objectsToPull.Remove(pulled);
    }

    void OnDestroy()
    {
        if (_gravityManager != null)
            DisableGravitation();
    }

}
