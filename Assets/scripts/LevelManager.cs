using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    LevelGenerator _lvlGenerator;
    LevelCleaner _lvlCleaner;
    LinkedList<Level> _levels;
    [SerializeField] int _levelsExistAtTheSameTime;

    static public LevelManager Instance { get; private set; }

    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _levels = new LinkedList<Level>();
        _lvlGenerator = new LevelGenerator();
        _lvlCleaner = new LevelCleaner(_levels, _levelsExistAtTheSameTime);
        AddLevel(_lvlGenerator.GenerateStartLevel());
        CallGenerator();
        CallGenerator();
    }

    public void CallGenerator()
    {
        AddLevel(_lvlGenerator.GenerateLevel(_levels.Last.Value));
        _lvlCleaner.Clean();
    }
    void AddLevel(Level level)
    {
        _levels.AddLast(level);
    }
}

