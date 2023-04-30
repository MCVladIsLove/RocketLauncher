using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    private void Awake()
    {
        HitPairsDictionary.Init();
    }
}
