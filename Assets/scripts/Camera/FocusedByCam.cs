using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedByCam : MonoBehaviour
{
    [SerializeField] bool _canBeFocused = false;
    public bool CanBeFocused { get { return _canBeFocused; } set { _canBeFocused = value; } }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_canBeFocused && other.gameObject.tag == "Player")
        {
            CameraInstance.mainCam.GetComponent<CameraMovement>().SetFollow(transform);
        }
    }

    public void AllowFocus()
    {
        _canBeFocused = true;
    }
    public void DisallowFocus()
    {
        _canBeFocused = false;
    }
}
