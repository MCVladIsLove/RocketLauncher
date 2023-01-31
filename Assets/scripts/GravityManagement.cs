using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManagement : MonoBehaviour
{
    static public GravityManagement Instance { get; private set; }

    List<GravitySystemObject> _gravityObjects;
    public List<GravitySystemObject> GravityObjects => _gravityObjects;

    [SerializeField] float _minDistanceClampBorder = 3; // too low distance causes very high force pulling objects far away
    [SerializeField] float _minDistanceToMagnitize = 1;
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
        if (Vector3.Distance(pulled.position, to.position) > _minDistanceToMagnitize) 
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
}
