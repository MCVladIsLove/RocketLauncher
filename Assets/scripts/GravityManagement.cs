using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManagement : MonoBehaviour
{
    static public GravityManagement Instance { get; private set; }

    List<GravitySystemObject> _gravityObjects;
    public List<GravitySystemObject> GravityObjects => _gravityObjects;

    [SerializeField] float _minDistanceClampBorder = 3; // too low distance causes very high force pulling objects far away
    [SerializeField] float _minDistanceToMagnetize = 1;
    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _gravityObjects = new List<GravitySystemObject>();
    }

    public void Pull(Rigidbody pulled, Rigidbody to)
    {
        if (DistanceHigherThanMin(pulled.position, to.position)) 
        {
            Vector3 direction = (to.position - pulled.position).normalized;
            float force = GetForceBetween(pulled, to);
            pulled.AddForce(direction * force, ForceMode.Force);
        }
    }

    public void PullToAll(Rigidbody pulled)
    {
        foreach (GravitySystemObject go in _gravityObjects)
        {
            if (go.enabled && go.gameObject != pulled.gameObject && go.Magnetic)
            {
                Pull(pulled, go.GetComponent<Rigidbody>());
            }
        }
    }

    public void MagnetizeAll(Rigidbody magnetic)
    {
        foreach (GravitySystemObject go in _gravityObjects)
        {
            if (go.enabled && go.gameObject != magnetic.gameObject && go.Pullable)
            {
                Instance.Pull(go.GetComponent<Rigidbody>(), magnetic);
            }
        }
    }

    public float GetForceBetween(Rigidbody pulled, Rigidbody to)
    {
        float distance = (pulled.position - to.position).magnitude;
        distance = Mathf.Clamp(distance, _minDistanceClampBorder, distance);

        return pulled.mass * to.mass / (distance * distance);
    }
    public float GetForceBetween(Vector3 pulledObjPos, float pulledObjMass, Vector3 magneticObjPos, float magneticObjMass)
    {
        float distance = (pulledObjPos - magneticObjPos).magnitude;
        distance = Mathf.Clamp(distance, _minDistanceClampBorder, distance);

        return pulledObjMass * magneticObjMass / (distance * distance);
    }
    public float GetVelocityFromForce(float force, float affectedObjectMass, ForceMode forceMode) 
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force * Time.fixedDeltaTime / affectedObjectMass;    // Точно
            case ForceMode.Acceleration:
                return force * Time.fixedDeltaTime;                         // Не точно
            case ForceMode.VelocityChange:
                return force;                                               // Не точно
            default:
                return force / affectedObjectMass;                          // Импульс (не точно)
        }
    }
    public Vector3 GetVelocityFromForce(Vector3 force, float affectedObjectMass, ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force * Time.fixedDeltaTime / affectedObjectMass;    // Точно
            case ForceMode.Acceleration:
                return force * Time.fixedDeltaTime;                         // Не точно
            case ForceMode.VelocityChange:
                return force;                                               // Не точно
            default:
                return force / affectedObjectMass;                          // Импульс (не точно)
        }
    }

    public Vector3 GetVelocityChangeNextPhysicsCall(Rigidbody pulled, Vector3 pulledObjPosNow, Vector3 additionalForce)
    {
        Vector3 resultingVelocity = Vector3.zero;
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            goRb = go.GetComponent<Rigidbody>();
            if (go.enabled && go.gameObject != pulled.gameObject && go.Magnetic && DistanceHigherThanMin(pulledObjPosNow, goRb.position))
            {
                Vector3 direction = (goRb.position - pulledObjPosNow).normalized;
                float force = GetForceBetween(pulledObjPosNow, pulled.mass, goRb.position, goRb.mass);
                resultingVelocity += GetVelocityFromForce(force, pulled.mass, ForceMode.Force) * direction;
            }
        }
        
        return resultingVelocity + GetVelocityFromForce(additionalForce, pulled.mass, ForceMode.Impulse);
    }
    public Vector3 GetVelocityChangeNextPhysicsCall(Rigidbody pulled, Vector3 pulledObjPosNow)
    {
        Vector3 resultingVelocity = Vector3.zero;
        Rigidbody goRb;
        foreach (GravitySystemObject go in _gravityObjects)
        {
            goRb = go.GetComponent<Rigidbody>();
            if (go.enabled && go.gameObject != pulled.gameObject && go.Magnetic && DistanceHigherThanMin(pulledObjPosNow, goRb.position))
            {
                Vector3 direction = (goRb.position - pulledObjPosNow).normalized;
                float force = GetForceBetween(pulledObjPosNow, pulled.mass, goRb.position, goRb.mass);
                resultingVelocity += GetVelocityFromForce(force, pulled.mass, ForceMode.Force) * direction;
            }
        }

        return resultingVelocity;
    }

    public bool DistanceHigherThanMin(Vector3 first, Vector3 second)
    {
        return Vector3.Distance(first, second) > _minDistanceToMagnetize;
    }
}
