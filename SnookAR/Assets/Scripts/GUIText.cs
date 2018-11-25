using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIText : MonoBehaviour
{
    public static int Score, Health;
    public Text RoundNumUI, ScoreUI, HealthUI;
    public GameObject targetFoundUI;

    // Use this for initialization
    void Start()
    {
        targetFoundUI.SetActive(false);
        Health = 100;
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //RoundNumUI.text = "Round: " + Spawner.waveNumber;
        ScoreUI.text = "Score: " + Score;
        HealthUI.text = "Health: " + Health;
        //RoundNumUI.text = "Round: " + Spawner.waveNumber;

        if (DefaultTrackableEventHandler.isTracking == true)
        {
            targetFoundUI.SetActive(false);
        }
        if (DefaultTrackableEventHandler.isTracking == false)
        {
            targetFoundUI.SetActive(true);
        }
    }
}
