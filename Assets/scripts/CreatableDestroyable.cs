using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatableDestroyable : MonoBehaviour
{
    private void Start()
    {
        Game.Instance.AddLevelObject(this);
    }

    public void RemoveObject()
    {
        StartCoroutine(DestroyMyself());
    }
    IEnumerator DestroyMyself()
    {
        yield return new WaitForSecondsRealtime(2f);
        Game.Instance.RemoveLevelObject(this);
        Destroy(gameObject);
    }
}
