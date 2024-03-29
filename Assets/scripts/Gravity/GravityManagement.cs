using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManagement : MonoBehaviour
{
    static public GravityManagement Instance { get; private set; }

    List<GravitySystemObject> _gravityObjects;
    //public List<GravitySystemObject> GravityObjects => _gravityObjects;

    [SerializeField] float _minDistanceClampBorder = 3; // too low distance causes very high force pulling objects far away
    [SerializeField] float _minDistanceToMagnetize = 1;

    public float MinDistanceClampBorder { get { return _minDistanceClampBorder; } }
    public float MinDistanceToMagnetize { get { return _minDistanceToMagnetize; } }
    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _gravityObjects = new List<GravitySystemObject>();
    }
    private void FixedUpdate()
    {
        foreach (GravitySystemObject go in _gravityObjects)
        {
            go.PullObjects();
        }
    }

    public void Pull(Rigidbody pulled, Rigidbody to)
    {
        Vector3 direction = (to.position - pulled.position).normalized;
        float force = GravityManagementUtils.GetForceBetween(pulled, to, true);
        pulled.AddForce(direction * force, ForceMode.Force);
    }

    public void PullToAll(Rigidbody pulled)
    {
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            if (go.gameObject.activeInHierarchy && go.gameObject != pulled.gameObject
                && GravityManagementUtils.ObjectsCanGravitate(pulled.position, (goRb = go.Rigidbody).position))
            {
                Pull(pulled, goRb);
            }
        }
    }

    public void MagnetizeAll(Rigidbody magnetic) 
    {
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            if (go.gameObject.activeInHierarchy && go.gameObject != magnetic.gameObject
                && GravityManagementUtils.ObjectsCanGravitate((goRb = go.Rigidbody).position, magnetic.position))
            {
                Pull(goRb, magnetic);
            }
        }
    }
    public Vector3 GetVelocityChangeNextPhysicsCall(GravitySystemObject pulled, Vector3 pulledObjPosNow, Vector3 additionalForce)
    {
        Vector3 resultingVelocity = Vector3.zero;
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            goRb = go.Rigidbody;
            if (go.gameObject.activeInHierarchy && go.gameObject != pulled.gameObject && 
                GravityManagementUtils.ObjectsCanGravitate(goRb.position, pulledObjPosNow) &&
                go.CanPullObject(pulled))
            {
                Vector3 direction = (goRb.position - pulledObjPosNow).normalized;
                float force = GravityManagementUtils.GetForceBetween(pulledObjPosNow, pulled.Rigidbody.mass, goRb.position, goRb.mass, true);
                resultingVelocity += GravityManagementUtils.GetVelocityFromForce(force, pulled.Rigidbody.mass, ForceMode.Force) * direction;
            }
        }
        
        return resultingVelocity + GravityManagementUtils.GetVelocityFromForce(additionalForce, pulled.Rigidbody.mass, ForceMode.Impulse);
    }
    public Vector3 GetVelocityChangeNextPhysicsCall(GravitySystemObject pulled, Vector3 pulledObjPosNow)
    {
        Vector3 resultingVelocity = Vector3.zero;
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            goRb = go.Rigidbody;
            if (go.gameObject.activeInHierarchy && go.gameObject != pulled.gameObject &&
                GravityManagementUtils.ObjectsCanGravitate(goRb.position, pulledObjPosNow) &&
                go.CanPullObject(pulled))
            {
                Vector3 direction = (goRb.position - pulledObjPosNow).normalized;
                float force = GravityManagementUtils.GetForceBetween(pulledObjPosNow, pulled.Rigidbody.mass, goRb.position, goRb.mass, true);
                resultingVelocity += GravityManagementUtils.GetVelocityFromForce(force, pulled.Rigidbody.mass, ForceMode.Force) * direction;
            }
        }

        return resultingVelocity;
    }

     public bool RemoveFromGravitySystem(GravitySystemObject gravityObject)
     {
        foreach (GravitySystemObject go in _gravityObjects)
            if (go.CanPullObject(gravityObject))
                go.RemoveFromPulledObjects(gravityObject);

         return _gravityObjects.Remove(gravityObject); 
     }
  
    public void AddToGravitySystem(GravitySystemObject gravityObject)
    {
        _gravityObjects.Add(gravityObject);
    }
}
