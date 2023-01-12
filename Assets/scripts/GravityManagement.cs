using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManagement : MonoBehaviour
{
    static public GravityManagement Instance { get; private set; }

    List<GameObject> _gravityObjects;
    public List<GameObject> GravityObjects => _gravityObjects;

    [SerializeField] float _minDistanceClampBorder = 3; // too low distance causes very high force pulling objects far away
    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _gravityObjects = new List<GameObject>();
    }

    public void Pull(Rigidbody pulled, Rigidbody to)
    {
        GameObject go = pulled.gameObject;
        GameObject pullTo = to.gameObject;
        if (Vector3.Distance(go.transform.position, to.transform.position) > 1)
        {
            float distance = (pullTo.transform.position - go.transform.position).magnitude;
            Vector3 direction = (pullTo.transform.position - go.transform.position).normalized;
            distance = Mathf.Clamp(distance, _minDistanceClampBorder, distance);
            float force = pulled.mass * to.mass / (distance * distance);
            pulled.AddForce(direction * force, ForceMode.Force);
        }
    }

    public void PullToAll(Rigidbody pulled)
    {
        GravitySystemObject magneticGO;
        foreach (GameObject go in _gravityObjects)
        {
            magneticGO = go.GetComponent<GravitySystemObject>();
            if (magneticGO.enabled && go != pulled.gameObject && magneticGO.Magnetic)
            {
                Pull(pulled, go.GetComponent<Rigidbody>());
            }
        }
    }

    public void MagnetizeAll(Rigidbody magnetic)
    {
        GravitySystemObject pulledGravitySystemObj;
        foreach (GameObject go in _gravityObjects)
        {
            pulledGravitySystemObj = go.GetComponent<GravitySystemObject>();
            if (pulledGravitySystemObj.enabled && go != magnetic.gameObject && pulledGravitySystemObj.Pullable)
            {
                Instance.Pull(go.GetComponent<Rigidbody>(), magnetic);
            }
        }
    }


}
