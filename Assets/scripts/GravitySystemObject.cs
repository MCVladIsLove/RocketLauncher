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
    [SerializeField] protected List<GravitySystemObject> _rotatingObjects;

    virtual protected void Start()
    {
        _gravityManager = GravityManagement.Instance;
        EnableGravitation();

        if (_rotateAroundGameObject)
        {
            _rb.AddForce(GravityManagementUtils.GetOrbitalVelocity(_rotateAroundGameObject, _rb), ForceMode.VelocityChange);
        }
        else
            _rb.AddForce(_startForceDirection, ForceMode.Impulse);
        // here
    }
    virtual protected void Awake()
    {
        _objectsToPull = new LinkedList<GravitySystemObject>();
        _rb = GetComponent<Rigidbody>();

        SpawnRotatingObjects();
    }

    virtual protected void SpawnRotatingObjects()
    {
        GravitySystemObject tmp = this;
        Vector3 pos = this.transform.position;
        foreach (GravitySystemObject rotating in _rotatingObjects)
        {
            pos += (tmp.transform.lossyScale.y / 2 + rotating.transform.lossyScale.y / 2 * 1.8f) * Vector3.up;
            tmp = Instantiate(rotating, pos, Quaternion.identity);
            tmp._rotateAroundGameObject = _rb;

            AddToPulledObjects(tmp);
        }
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
