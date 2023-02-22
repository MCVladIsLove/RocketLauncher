using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelCleaner
{
    CameraMovement _camMovement;
    float _bottomWorldBorderY;
    float _camToPlaySpaceDistance;
    List<CreatableDestroyable> _destroyableObjects;

    public LevelCleaner(List<CreatableDestroyable> destroyableObjects)
    {
        _camMovement = CameraInstance.mainCam.GetComponent<CameraMovement>();
        _camToPlaySpaceDistance = Mathf.Abs(CameraInstance.mainCam.transform.position.z);
        _bottomWorldBorderY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, 0, _camToPlaySpaceDistance)).y;
        _destroyableObjects = destroyableObjects;
    }

    public void Update()
    {
        _bottomWorldBorderY += _camMovement.PositionChange.y;

        // foreach (var v in _destroyableObjects.Select(x => x).Where(x => IsOutOfBounds(x.transform.position)))
        //    Debug.Log(v);

        /*            {
                        CreatableDestroyable current = e.Current;
                        e.MoveNext();
                        _destroyableObjects.Remove(current);
                        Object.Destroy(current.gameObject);
                        Debug.Log(e.Current);
                    }
                    else
                        e.MoveNext();*/
    }

    bool IsOutOfBounds(Vector3 objectPos)
    {
        return objectPos.y < _bottomWorldBorderY;
    }

}
