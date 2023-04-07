using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Transform _highestObject;
    Vector3 _aboveTopObject;
    public Vector3 AboveTopObject { get { return _aboveTopObject; } }

    private void Awake()
    {
        _aboveTopObject = new Vector3(_highestObject.position.x, _highestObject.position.y + _highestObject.lossyScale.y / 2, 0);
    }
}
