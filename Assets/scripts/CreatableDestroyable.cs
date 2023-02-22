using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatableDestroyable : MonoBehaviour
{
    public void RemoveObject()
    {
        Destroy(this);
    }
}
