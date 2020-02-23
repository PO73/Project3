using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleAndEnd : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown("space")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown("escape")) {
            Application.Quit();
        }
    }
}
