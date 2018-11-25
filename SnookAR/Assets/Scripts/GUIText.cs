using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIText : MonoBehaviour
{
    public Text RoundNumUI, ScoreUI, HealthUI;
    public GameObject targetFoundUI, NextWaveButtonPannel;

    // Use this for initialization
    void Start()
    {
        targetFoundUI.SetActive(false);
        NextWaveButtonPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //RoundNumUI.text = "Round: " + Spawner.waveNumber;

        ScoreUI.text = "Score: " + ClickForce.score;
        HealthUI.text = "Health: " + ClickForce.health;
        RoundNumUI.text = "Round: " + Spawner.waveNumber;


        if (DefaultTrackableEventHandler.isTracking == true)
        {
            targetFoundUI.SetActive(false);
            Time.timeScale = 1;
        }
        if (DefaultTrackableEventHandler.isTracking == false)
        {
            targetFoundUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (Spawner.ballNumber == 0)
        {
            NextWaveButtonPannel.SetActive(true);
        }
        if (Spawner.ballNumber > 0)
        {
            NextWaveButtonPannel.SetActive(false);
        }
    }
}
