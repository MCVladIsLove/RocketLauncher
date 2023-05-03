using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player Instance { get; private set; }

    int _coinsCount = 0;
    int _hp = 100;
    void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void TakeDamage(int damageAmount)
    {
        _hp -= damageAmount;
        Debug.Log(_hp);
        if (_hp <= 0)
            Die();
    }
    public void Die()
    {
        Debug.Log("Death");
    }
}
