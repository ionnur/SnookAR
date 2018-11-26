using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GUIText : MonoBehaviour
{
    public Text RoundNumUI, ScoreUI, HealthUI, FinalScoreUI, FinalRoundUI;
    public GameObject targetFoundUI, NextWaveButtonPannel, FinalScorePanel;

    // Use this for initialization
    void Start()
    {
        targetFoundUI.SetActive(false);
        NextWaveButtonPannel.SetActive(false);
        FinalScorePanel.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        //RoundNumUI.text = "Round: " + Spawner.waveNumber;

        ScoreUI.text = "Score: " + ClickForce.score;
        HealthUI.text = "Health: " + ClickForce.health;
        RoundNumUI.text = "Round: " + (Spawner.waveNumber);

        FinalScoreUI.text = "Final Score: " + ClickForce.score;
        FinalRoundUI.text = "Round Reached: " + Spawner.waveNumber;


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

        if (Spawner.ballNumber <= 0)
        {
            NextWaveButtonPannel.SetActive(true);
        }
        if (Spawner.ballNumber > 0)
        {
            NextWaveButtonPannel.SetActive(false);
        }

        if (ClickForce.health <= 0)
        {
            FinalScorePanel.SetActive(true);
        }
    }


}
