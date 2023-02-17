using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipControl : MonoBehaviour
{
    Vector3 _fingerStartPos;
    bool _tmpIsHeld = false;
    Touch _touch;
    Camera _cam;
    float _camToCubeDistance;
    Rigidbody _rb;
    TrajectoryDrawing _trajectory;
    [SerializeField] float _forceMultiplier;
    void Start()
    {
        _trajectory = GetComponent<TrajectoryDrawing>();
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _camToCubeDistance = Mathf.Abs(_cam.transform.position.z - transform.position.z);
    }

    void Update()
    {
        /* if (Input.touchCount > 0)
         {
             _touch = Input.GetTouch(0);
             if (_touch.phase == TouchPhase.Began)
                 _fingerStartPos = _cam.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, _camToCubeDistance));

             if (_touch.phase == TouchPhase.Ended && Input.touchCount == 1)
             {
                 Vector3 touchPos = _cam.ScreenToWorldPoint(new Vector3(_touch.position.x, _touch.position.y, _camToCubeDistance));
                 _rb.AddForce((_fingerStartPos.x - touchPos.x), (_fingerStartPos.y - touchPos.y), 0, ForceMode.Impulse);
             }
         } */

        if (Input.GetKeyDown(KeyCode.Mouse0))
            TouchStart();
        
        if (_tmpIsHeld)
            WhileTouch();

        if (Input.GetKeyUp(KeyCode.Mouse0))
            ReleaseTouch();
    }

    private void TouchStart()
    {
        _fingerStartPos = GetTouchWorldPoint();
        _tmpIsHeld = true;
    }
    private void WhileTouch()
    {
        Vector3 touchPos = GetTouchWorldPoint();
        Vector3 pushForce = GetFingerPullDirection(touchPos) * _forceMultiplier;
        if (transform.parent)
            transform.parent.up = pushForce;
        _trajectory.CalculateTrajectory(pushForce);
    }
    private void ReleaseTouch()
    {
        LaunchRocket();
        _trajectory.ClearTrajectory();
        _tmpIsHeld = false;
    }
    private Vector3 GetTouchWorldPoint()
    {
        return _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camToCubeDistance));
    }

    private Vector3 GetFingerPullDirection(Vector3 touchPos)
    {
        return new Vector3((_fingerStartPos.x - touchPos.x), (_fingerStartPos.y - touchPos.y), 0);
    }
    private void LaunchRocket()
    {
        Landable landing = GetComponentInParent<Landable>();
        landing?.Release();
        Vector3 touchPos = GetTouchWorldPoint();
        _rb.AddForce(GetFingerPullDirection(touchPos) * _forceMultiplier, ForceMode.Impulse);
    }
}
