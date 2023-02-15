using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Camera _cam;
    [SerializeField] Vector3 _relativePosition;
    [SerializeField] Transform _follow;
    [SerializeField, Range(0, 1)] float _lerpSpeed;
    public Vector3 RelativePosition { get { return _relativePosition; } set { _relativePosition = value; } }
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (RelativePositionNow() != _relativePosition)
            transform.position = Vector3.Lerp(transform.position, _follow.position + _relativePosition, _lerpSpeed);
    }

    Vector3 RelativePositionNow()
    {
        return transform.position - _follow.position;
    }

    public void SetFollow(Transform follow)
    {
        _follow = follow;
    }

}
