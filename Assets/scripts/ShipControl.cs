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
        {
            _fingerStartPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camToCubeDistance));
            _tmpIsHeld = true;
        }


        if (_tmpIsHeld)
        {
            Vector3 touchPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camToCubeDistance));
            _trajectory.CalculateTrajectory(new Vector3((_fingerStartPos.x - touchPos.x), (_fingerStartPos.y - touchPos.y), 0));
        }


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _rb.isKinematic = false;
            Vector3 touchPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camToCubeDistance));
            _rb.AddForce((_fingerStartPos.x - touchPos.x), (_fingerStartPos.y - touchPos.y), 0, ForceMode.Impulse);
            _tmpIsHeld = false;
        }
    }

    
    public void TrajecorySimulationStep(GameObject cloneOfThis)
    {
        GravityManagement.Instance.PullToAll(cloneOfThis.GetComponent<Rigidbody>());
    }

}
