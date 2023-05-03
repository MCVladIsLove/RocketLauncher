using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    public void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && enabled)
        {
            LevelManager.Instance.CallGenerator();
            enabled = false;
        }
    }
}
