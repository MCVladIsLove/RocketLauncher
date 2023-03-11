using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    BoxCollider _collider;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Game.Instance.CallGenerator();
        }
    }

    public void MoveTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
