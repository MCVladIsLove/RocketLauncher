using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    Level[] _levels;
    int _horizontalCount = 3;
    PlaySpaceGrid _playGrid;
    CameraMovement _cam;
    public LevelGenerator(float minVerticalSpaceBetweenCells, float minHorizontalSpaceBetweenCells, CameraMovement camMovement)
    {
        _levels = Resources.LoadAll<Level>("Levels");
        _cam = camMovement;
        _playGrid = new PlaySpaceGrid();
        _playGrid.Width = Game.Instance.PlaySpaceWidth;
        _playGrid.MinVerticalSpaceBetweenCells = minVerticalSpaceBetweenCells;
        _playGrid.MinHorizontalSpaceBetweenCells = minHorizontalSpaceBetweenCells;
        _playGrid.HorizontalCount = _horizontalCount;
    }
    public Transform GenerateLevel(Transform highestPlanet)
    {
        Level lvl = _levels[Random.Range(0, _levels.Length)];
        _playGrid.BottomY = highestPlanet.position.y + highestPlanet.lossyScale.y;
        float screenBottomY = _cam.GetWorldBottomYIfCameraFollows(highestPlanet);
        float levelTopY = Game.Instance.PlaySpaceHeight + screenBottomY;
        _playGrid.Height = levelTopY - _playGrid.BottomY;
        _playGrid.LeftX = highestPlanet.position.x - Game.Instance.PlaySpaceWidth / 2;
        _playGrid.VerticalCount = Mathf.Max(lvl.LeftZoneObjects.Length, lvl.MiddleZoneObjects.Length, lvl.RightZoneObjects.Length);

        highestPlanet = FillLevelByGrid(lvl.LeftZoneObjects, lvl.MiddleZoneObjects, lvl.RightZoneObjects).transform;

        return highestPlanet;
    }
    GameObject CreateLevelObject(GameObject prefab, Vector3 position)
    {
        GameObject instance = GameObject.Instantiate(prefab, position, Quaternion.identity);
        return instance;
    }
    GameObject CreateLevelObject(GameObject prefab, Vector3 position, bool checkIntersection)
    {
        RaycastHit rayHit;
        GameObject instance = GameObject.Instantiate(prefab, position, Quaternion.identity);
        if (checkIntersection && instance.GetComponent<Rigidbody>().SweepTest(position, out rayHit))
        {
            GameObject.Destroy(instance);
            return null;
        }
        return instance;
    }


    GameObject CreateLevelObjectsColumn(GameObject[] objects, int column)
    {
        GameObject highest = null;
        Vector2 cellCenter;

        for (int i = objects.Length - 1; i >= 0; i--)
        {
            if (objects[i] == null)
                continue;

            cellCenter = _playGrid.GetCellCenter(i, column);
            highest = CreateLevelObject(objects[i],
                cellCenter);
        }
        return highest;
    }

    GameObject FillLevelByGrid(GameObject[] leftColumn, GameObject[] middleColumn, GameObject[] rightColumn)
    {
        GameObject highest;
        GameObject tmp;
        highest = CreateLevelObjectsColumn(leftColumn, 0);

        tmp = CreateLevelObjectsColumn(middleColumn, 1);
        if (highest == null)
            highest = tmp;

        else if (tmp != null && tmp.transform.position.y > highest.transform.position.y)
            highest = tmp;

        tmp = CreateLevelObjectsColumn(rightColumn, 2);
        
        if (highest == null)
            highest = tmp;
        else if (tmp?.transform.position.y > highest?.transform.position.y)
            highest = tmp;


        /*for (int i = 0; i < _playGrid.HorizontalCount; i++)
            for (int j = 0; j < _playGrid.VerticalCount; j++)
            {
                Rect cellBorders = _playGrid.GetCellBorders(j, i);
                Debug.DrawLine(new Vector3(cellBorders.xMin, cellBorders.yMin, 0), new Vector3(cellBorders.xMax, cellBorders.yMin, 0), Color.white, 100);
                Debug.DrawLine(new Vector3(cellBorders.xMin, cellBorders.yMin, 0), new Vector3(cellBorders.xMin, cellBorders.yMax, 0), Color.white, 100);
                Debug.DrawLine(new Vector3(cellBorders.xMin, cellBorders.yMax, 0), new Vector3(cellBorders.xMax, cellBorders.yMax, 0), Color.white, 100);
                Debug.DrawLine(new Vector3(cellBorders.xMax, cellBorders.yMax, 0), new Vector3(cellBorders.xMax, cellBorders.yMin, 0), Color.white, 100);
            }
        */  //DRAWING CELLS
        return highest;
    }
}