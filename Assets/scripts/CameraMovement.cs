using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    bool _stabilized;
    float _spaceBetweenFollowAndWorldBottom;
    Camera _cam;
    Vector3 _positionChange;
   // Vector3 _previousStabilizedPosition;
    [SerializeField] Vector3 _relativePosition;
    [SerializeField] Transform _follow;
    [SerializeField] float _camSpeed;
    [SerializeField] float _precision;
    // public UnityEvent<Vector3, Vector3> OnCameraStabilized;

    public float SpaceBetweenFollowedAndBottom { get { return _spaceBetweenFollowAndWorldBottom; } }
    public Vector3 RelativePosition { get { return _relativePosition; } set { _relativePosition = value; } }
    public Vector3 PositionChange { get { return _positionChange; } }
    void Start()
    {
        _stabilized = true;
        _positionChange = Vector3.zero;
        _cam = GetComponent<Camera>();
        UpdateSpaceBetweenFollowedAndBottom();
       // _previousStabilizedPosition = _cam.transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(RelativePositionNow(), _relativePosition) >= _precision)
        {
            _positionChange = PositionChangeCalculate();
            transform.position += _positionChange;
            _stabilized = false;
        }
        else if (!_stabilized)
        {
            _stabilized = true;
            _positionChange = Vector3.zero;
          //  OnCameraStabilized?.Invoke();
        }
    }

    Vector3 RelativePositionNow()
    {
        return transform.position - _follow.position;
    }

    public void SetFollow(Transform follow)
    {
        _follow = follow;
    }

    Vector3 PositionChangeCalculate()
    {
        return (_follow.position + _relativePosition - transform.position) * _camSpeed * Time.deltaTime;
    }

    public void UpdateSpaceBetweenFollowedAndBottom()
    {
        _spaceBetweenFollowAndWorldBottom = CalculateSpaceBetweenFollowedAndBottom();
    }
    float CalculateSpaceBetweenFollowedAndBottom()
    {
        float camCenter = _cam.transform.position.y;
        float followedPosY = camCenter - _relativePosition.y;
        float bottomSeenWorldBorderY = _cam.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(_relativePosition.z))).y;
        return followedPosY - bottomSeenWorldBorderY; 
    }

    public float GetWorldBottomYIfCameraFollows(Transform followed)
    {
        return followed.position.y - _spaceBetweenFollowAndWorldBottom;
    }
}
