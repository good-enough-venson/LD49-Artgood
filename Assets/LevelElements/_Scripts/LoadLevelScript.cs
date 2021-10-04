using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour
{
    public string nextLevel;

    private void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}
