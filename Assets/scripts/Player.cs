using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static public Player Instance { get; private set; }

    int _starsCount = 0;
    int _hp = 100;

    public int StarsCount { get { return _starsCount; } set { _starsCount = value; } }

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
       // if (_hp <= 0)
           // Die();
    }
    public void Die()
    {
        Debug.Log("Death");
    }

    private void OnDestroy()
    {
        // game over logic
        Debug.Log("Game OVER");
    }
}
