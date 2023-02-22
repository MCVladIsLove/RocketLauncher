using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatableDestroyable : MonoBehaviour
{
    public void RemoveObject()
    {
        StartCoroutine(DestroyMyself());
    }
    IEnumerator DestroyMyself()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }


}
