using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExitGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;

        #else
            Application.Quit();

        #endif
    }
}
