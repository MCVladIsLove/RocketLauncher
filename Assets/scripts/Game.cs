using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    List<CreatableDestroyable> _levelObjects;
    LevelCleaner _lvlCleaner;
    LevelGenerator _lvlGenerator;

    void Start()
    {
        _levelObjects = GetLevelObjects();
        _lvlCleaner = new LevelCleaner(_levelObjects);
        _lvlGenerator = new LevelGenerator();
    }

    void Update()
    {
        _lvlCleaner.Update();
    }

    List<CreatableDestroyable> GetLevelObjects()
    {
        return new List<CreatableDestroyable>(FindObjectsOfType<CreatableDestroyable>());
    }

}
