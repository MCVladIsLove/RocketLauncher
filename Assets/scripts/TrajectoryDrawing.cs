using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class TrajectoryDrawing : MonoBehaviour
{
    PhysicsScene _clonedScenePhysics;
    Scene _clonedScene;
    GameObject _cloneOfThis;
    Rigidbody _cloneRb;
    Rigidbody _originalRb;
    LineRenderer _lineRenderer;
    readonly string _hiddenSceneName = "Hidden scene for Trajectories";

    [SerializeField] bool _simulateManually;
    [SerializeField] int _stepsToSimulate;

    public UnityEvent<GameObject> OnSimulationStep; // Event, invoking inside CalculateTrajectory. Handles only object movement on every frame.

    private void Start()
    {
        CreateScene();
        _originalRb = GetComponent<Rigidbody>();
        SetClone();
        _lineRenderer = GetComponent<LineRenderer>();
        SceneManager.MoveGameObjectToScene(_cloneOfThis, _clonedScene);
    }

    void Update()
    {
        if (!_simulateManually)
            CalculateTrajectory(Vector3.zero);
    }

    public void CalculateTrajectory(Vector3 initialForce)
    {
        SinchronizeClone();
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, this.gameObject.transform.position);

        _cloneRb.AddForce(initialForce, ForceMode.Impulse);
        OnSimulationStep?.Invoke(_cloneOfThis);

        _clonedScenePhysics.Simulate(Time.fixedDeltaTime);

        for (int i = 1; i < _stepsToSimulate; i++)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(i, _cloneOfThis.transform.position);
            OnSimulationStep?.Invoke(_cloneOfThis);
            _clonedScenePhysics.Simulate(Time.fixedDeltaTime);
        }
    }

    private void SinchronizeClone()
    {
        _cloneOfThis.transform.position = gameObject.transform.position;
        _cloneOfThis.transform.rotation = gameObject.transform.rotation;
        _cloneRb.position = _originalRb.position;
        _cloneRb.velocity = _originalRb.velocity;
    }

    void CreateScene()
    {
        if (!(_clonedScene = SceneManager.GetSceneByName(_hiddenSceneName)).isLoaded)
        { 
            _clonedScene = SceneManager.CreateScene(_hiddenSceneName, new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        }

        _clonedScenePhysics = _clonedScene.GetPhysicsScene();
    }

    private void SetClone()
    {
        _cloneOfThis = new GameObject(gameObject.name + "_trajectory");
        _cloneOfThis.transform.position = gameObject.transform.position;
        _cloneOfThis.transform.rotation = gameObject.transform.rotation;
        _cloneRb = _cloneOfThis.AddComponent<Rigidbody>();
        _cloneRb.mass = _originalRb.mass;
        _cloneRb.isKinematic = false;
        _cloneRb.useGravity = false;
    }

    public void ClearTrajectory()
    {
        _lineRenderer.positionCount = 0;
    }

}
