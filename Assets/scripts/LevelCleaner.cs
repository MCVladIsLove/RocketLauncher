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
    List<CreatableDestroyable> _objectsToDestroy;

    public LevelCleaner(List<CreatableDestroyable> destroyableObjects)
    {
        _camMovement = CameraInstance.mainCam.GetComponent<CameraMovement>();
        _camToPlaySpaceDistance = Mathf.Abs(CameraInstance.mainCam.transform.position.z);
        
        float bottomY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, 0, _camToPlaySpaceDistance)).y;
        float topY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, CameraInstance.mainCam.scaledPixelHeight, _camToPlaySpaceDistance)).y;
        float visiblePlaySpaceHeight = topY - bottomY;
        _bottomWorldBorderY = bottomY - visiblePlaySpaceHeight;
        
        _destroyableObjects = destroyableObjects;
    }

    public void Update()
    {
        _bottomWorldBorderY += _camMovement.PositionChange.y;
        foreach (CreatableDestroyable cd in _destroyableObjects.Select(x => x).Where(x => IsOutOfBounds(x.transform.position)).ToList())
        {
            _destroyableObjects.Remove(cd);
            cd.RemoveObject();
        }
    }

    bool IsOutOfBounds(Vector3 objectPos)
    {
        return objectPos.y < _bottomWorldBorderY;
    }

}
