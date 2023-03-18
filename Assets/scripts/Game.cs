using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    List<CreatableDestroyable> _levelObjects;
    LevelCleaner _lvlCleaner;
    LevelGenerator _lvlGenerator;
    CameraMovement _camMovement;
    Transform _highestPlanet;
    GeneratorTrigger _generatorTrigger;

    float _bottomPlaySpaceY;
    float _playSpaceHeight;
    float _playSpaceWidth;
    float _camToPlaySpaceDistance;

    [SerializeField] float _minVerticalSpaceBetweenObjects;
    [SerializeField] float _minHorizontalSpaceBetweenObjects;

    public float BottomPlaySpaceY { get { return _bottomPlaySpaceY; } }
    public float TopPlaySpaceY { get { return _bottomPlaySpaceY + _playSpaceHeight; } }
    public float PlaySpaceHeight { get { return _playSpaceHeight; } }
    public float PlaySpaceWidth { get { return _playSpaceWidth; } }

    static public Game Instance { get; private set; }

    void Start()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _generatorTrigger = FindObjectOfType<GeneratorTrigger>();

        _camMovement = CameraInstance.mainCam.GetComponent<CameraMovement>();
        _camToPlaySpaceDistance = Mathf.Abs(CameraInstance.mainCam.transform.position.z);

        _bottomPlaySpaceY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, 0, _camToPlaySpaceDistance)).y;
        float topY = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, CameraInstance.mainCam.scaledPixelHeight, _camToPlaySpaceDistance)).y;
        _playSpaceHeight = topY - _bottomPlaySpaceY;
        _playSpaceWidth = CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(CameraInstance.mainCam.scaledPixelWidth, 0, _camToPlaySpaceDistance)).x - CameraInstance.mainCam.ScreenToWorldPoint(new Vector3(0, 0, _camToPlaySpaceDistance)).x;

        _levelObjects = GetLevelObjects();
        _lvlCleaner = new LevelCleaner(_levelObjects);
        _lvlGenerator = new LevelGenerator(_minVerticalSpaceBetweenObjects, _minHorizontalSpaceBetweenObjects, _camMovement);

        _highestPlanet = GetHighestPlanet();

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

    public void CallGenerator()
    {
        Vector3 newGeneratorPosition = _highestPlanet.transform.position - Vector3.up * _highestPlanet.transform.lossyScale.y;
        _generatorTrigger.MoveTo(newGeneratorPosition);
        _highestPlanet = _lvlGenerator.GenerateLevel(_highestPlanet);
    }

    private Transform GetHighestPlanet()
    {
        if (_levelObjects.Count < 0)
            return null;

        GameObject highest = _levelObjects[0].gameObject;
        foreach (CreatableDestroyable cd in _levelObjects)
            if (cd.transform.position.y > highest.transform.position.y)
                highest = cd.gameObject;

        return highest.transform;
    }

    public void AddLevelObject(CreatableDestroyable obj)
    {
        _levelObjects.Add(obj);
    }
    public void RemoveLevelObject(CreatableDestroyable obj)
    {
        _levelObjects.Remove(obj);
    }
}

