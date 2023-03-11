using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    Level[] _levels;
    CameraMovement _camMovement;
    int _verticalSpaceBetweenObjects = 1;
    int _horizontalCount = 3;
    public LevelGenerator(CameraMovement camMovement)
    {
        _levels = Resources.LoadAll<Level>("Levels");
        _camMovement = camMovement;
    }
    public Transform GenerateLevel(Transform highestPlanet)
    {
        Level lvl = _levels[Random.Range(0, _levels.Length)];
        float levelBottomY = _camMovement.GetWorldBottomYIfCameraFollows(highestPlanet);
        float levelTopY = levelBottomY + Game.Instance.PlaySpaceHeight;
        float bottomBorderToGenerate = highestPlanet.position.y + highestPlanet.lossyScale.y + _verticalSpaceBetweenObjects;
        float activePlaySpaceHeight = levelTopY - bottomBorderToGenerate;
        float segmentWidth = Game.Instance.PlaySpaceWidth / _horizontalCount;
        int verticalCount = Mathf.Max(lvl.LeftZoneObjects.Length, lvl.MiddleZoneObjects.Length, lvl.RightZoneObjects.Length);

        Rect generationSpace = new Rect(highestPlanet.position.x - Game.Instance.PlaySpaceWidth / 2, bottomBorderToGenerate, segmentWidth, activePlaySpaceHeight);
        GameObject tmp = highestPlanet.gameObject;
        tmp = CreateLevelObjectsColumn(lvl.LeftZoneObjects, generationSpace, verticalCount);
        if (tmp != null && tmp.transform.position.y > highestPlanet.position.y)
            highestPlanet = tmp.transform;

        generationSpace.x += segmentWidth;
        tmp = CreateLevelObjectsColumn(lvl.MiddleZoneObjects, generationSpace, verticalCount);
        if (tmp != null && tmp.transform.position.y > highestPlanet.position.y)
            highestPlanet = tmp.transform;

        generationSpace.x += segmentWidth;
        tmp = CreateLevelObjectsColumn(lvl.RightZoneObjects, generationSpace, verticalCount);
        if (tmp != null && tmp.transform.position.y > highestPlanet.position.y)
            highestPlanet = tmp.transform;

        return highestPlanet;
    }

    GameObject CreateLevelObject(GameObject prefab, Vector3 position)
    {
        GameObject instance = GameObject.Instantiate(prefab, position, Quaternion.identity);
        Game.Instance.AddLevelObject(instance.GetComponent<CreatableDestroyable>());
        return instance;
    }

    GameObject CreateLevelObjectsColumn(GameObject[] objects, Rect generationSpace, int verticalCount)
    {
        GameObject highest = null;
        for (int i = objects.Length - 1; i >= 0; i--)
        {
            if (objects[i] == null)
                continue;

            float y = generationSpace.y + generationSpace.height / verticalCount * (verticalCount - i - 1);
            highest = CreateLevelObject(objects[i], 
                new Vector3(Random.Range(generationSpace.xMin, generationSpace.xMax),
                            Random.Range(y, y + generationSpace.height / verticalCount),
                            0));
        }
        return highest;
    }

}
