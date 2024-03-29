using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class TrajectoryDrawing : MonoBehaviour
{
    GravitySystemObject _gravityObject;
    LineRenderer _lineRenderer;

    [SerializeField] bool _simulateManually;
    [SerializeField] int _stepsToSimulate;

    private void Start()
    {
        _gravityObject = GetComponent<GravitySystemObject>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!_simulateManually)
            CalculateTrajectory(Vector3.zero);
    }

    public void CalculateTrajectory(Vector3 initialForce)
    {
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, gameObject.transform.position);

        Vector3 nextPos = gameObject.transform.position;
        Vector3 velocity = _gravityObject.Rigidbody.velocity; 
        int i = 1;
        if (initialForce != Vector3.zero)
        {
            velocity += GravityManagement.Instance.GetVelocityChangeNextPhysicsCall(_gravityObject, nextPos, initialForce);
            nextPos += velocity * Time.fixedDeltaTime;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(i, nextPos);
            i++;
        }

        for (; i < _stepsToSimulate; i++)
        {
            velocity += GravityManagement.Instance.GetVelocityChangeNextPhysicsCall(_gravityObject, nextPos);
            nextPos += velocity * Time.fixedDeltaTime;

            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(i, nextPos);
        }
    }

    public void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }

}
