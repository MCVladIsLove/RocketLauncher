using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [SerializeField] GameObject[] _leftZoneObjects;
    [SerializeField] GameObject[] _rightZoneObjects;
    [SerializeField] GameObject[] _middleZoneObjects;
    public GameObject[] MiddleZoneObjects { get { return _middleZoneObjects; } }
    public GameObject[] LeftZoneObjects { get { return _leftZoneObjects; } }
    public GameObject[] RightZoneObjects { get { return _rightZoneObjects; } }
}
