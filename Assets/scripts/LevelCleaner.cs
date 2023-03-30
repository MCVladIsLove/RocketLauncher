using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelCleaner
{
    List<CreatableDestroyable> _destroyableObjects;

    public LevelCleaner(List<CreatableDestroyable> destroyableObjects)
    {        
        _destroyableObjects = destroyableObjects;
    }

    public void Update()
    {
        foreach (CreatableDestroyable cd in _destroyableObjects.Select(x => x).Where(x => IsOutOfBounds(x.transform.position)).ToList())
        {
            cd.RemoveObject();
        }
    }

    bool IsOutOfBounds(Vector3 objectPos)
    {
        return objectPos.y < Game.Instance.BottomPlaySpaceY - Game.Instance.PlaySpaceHeight;
    }

}
