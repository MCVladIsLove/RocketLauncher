using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelCleaner
{
    LinkedList<Level> _levels;
    int _levelsExistAtTheSameTime;

    public LevelCleaner(LinkedList<Level> levels, int maxLevelsToExistAtTheSameTime)
    {
        _levels = levels;
        _levelsExistAtTheSameTime = maxLevelsToExistAtTheSameTime;
    }

    public void Clean()
    {
        if (MoreLvlsThanMax())
        {
            GameObject.Destroy(_levels.First.Value.gameObject, 0.5f);
            _levels.RemoveFirst();
        }
    }

    bool MoreLvlsThanMax()
    {
        return _levels.Count > _levelsExistAtTheSameTime;
    }
}
