using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Awake()
    {
        Game.Instance.Player = gameObject;
    }
}
