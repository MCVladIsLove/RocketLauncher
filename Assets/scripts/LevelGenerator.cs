using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    Level[] _levels;
    Level _generatedLvl;
    public LevelGenerator()
    {
        _levels = Resources.LoadAll<Level>("Levels/OtherLevels");
    }
    public Level GenerateLevel(Level previousLevel)
    {
        _generatedLvl = _levels[Random.Range(0, _levels.Length)];
        return GameObject.Instantiate<Level>(_generatedLvl, previousLevel.AboveTopObject, Quaternion.identity);
    }
    public Level GenerateStartLevel()
    {
        _generatedLvl = Resources.Load<Level>("Levels/Start/Start");
        return GameObject.Instantiate<Level>(_generatedLvl, Vector3.zero, Quaternion.identity);
    }
}