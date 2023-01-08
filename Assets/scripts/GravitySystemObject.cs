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
    public bool Pullable { get { return _pullable; } 
        set { if (gameObject.GetComponent<Rigidbody>()) _pullable = value; } }

    [SerializeField] bool _magnetic;
    public bool Magnetic { get { return _magnetic; } set { _magnetic = value; } }

    GravityManagement _gravityManager;
    List<GameObject> _gravityObjects;
    Rigidbody _rb;
    void Start()
    {
        _gravityManager = GravityManagement.Instance;
        _gravityObjects = _gravityManager.GravityObjects;
        _gravityObjects.Add(this.gameObject);
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_startForceDirection, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        if (_magnetic)
            _gravityManager.MagnetizeAll(_rb);
    }

    public void TrajectorySimulationStep(GameObject clonedObject)
    {
        _gravityManager.PullToAll(clonedObject.GetComponent<Rigidbody>());
    }

}
