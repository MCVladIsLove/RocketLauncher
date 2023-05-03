using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : GravitySystemObject
{
    protected override void Start()
    {
        base.Start();
        AddToPulledObjects(Player.Instance.GetComponent<GravitySystemObject>());
    }
}
