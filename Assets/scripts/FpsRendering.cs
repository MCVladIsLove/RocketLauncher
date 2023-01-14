using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsRendering : MonoBehaviour
{
    Text _text;    
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = "FPS: 0";
    }

    void Update()
    {
        _text.text = "FPS: " + Mathf.FloorToInt(1 / Time.deltaTime);
    }
}
