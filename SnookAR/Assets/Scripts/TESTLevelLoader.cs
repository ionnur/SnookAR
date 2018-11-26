using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TESTLevelLoader : MonoBehaviour
{

    // Use this for initialization
    public void RestartGame()
    {
        Spawner.ballNumber = 0;
        ClickForce.health = 100;
        ClickForce.score = 0;
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Application.Quit();
    }
}