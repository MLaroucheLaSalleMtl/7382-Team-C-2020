using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Toggle(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Toggle(false);
    }
    private void Toggle(bool set)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(set);
        }
    }
}
