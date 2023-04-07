using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    public void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && enabled)
        {
            Game.Instance.CallGenerator();
            enabled = false;
        }
    }
}
