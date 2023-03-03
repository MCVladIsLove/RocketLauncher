using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    List<CreatableDestroyable> _levelObjects;
    LevelCleaner _lvlCleaner;
    LevelGenerator _lvlGenerator;
    CameraMovement _camMovement;


    float _bottomPlaySpaceY;
    float _topPlaySpaceY;
    float _playSpaceHeight;
    float _playSpaceWidth;
    float _camToPlaySpaceDistance;

    public float BottomPlaySpaceY { get { return _bottomPlaySpaceY; } }
    public float PlaySpaceHeight { get { return _playSpaceHeight; } }
    public float PlaySpaceWidth { get { return _playSpaceWidth; } }

    static public Game Instance { get; private set; }

    void Start()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _camMovement = CameraInstance.mainCam.GetComponent<CameraMovement>();
        _camToPlaySpaceDistance = Mathf.Abs(CameraInstance.mainCam.transform.position.z);

        _bottomPlaySpaceY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, 0, _camToPlaySpaceDistance)).y;
        _topPlaySpaceY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, CameraInstance.mainCam.scaledPixelHeight, _camToPlaySpaceDistance)).y;
        _playSpaceHeight = _topPlaySpaceY - _bottomPlaySpaceY;

        _levelObjects = GetLevelObjects();
        _lvlCleaner = new LevelCleaner(_levelObjects);
        _lvlGenerator = new LevelGenerator();
        _lvlGenerator.GenerateLevel();

    }

    void Update()
    {
        _bottomPlaySpaceY += _camMovement.PositionChange.y;
        _lvlCleaner.Update();
    }

    List<CreatableDestroyable> GetLevelObjects()
    {
        return new List<CreatableDestroyable>(FindObjectsOfType<CreatableDestroyable>());
    }

}
