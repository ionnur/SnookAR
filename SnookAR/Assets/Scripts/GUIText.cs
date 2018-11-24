using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIText : MonoBehaviour
{

    public Text RoundNumUI, ScoreUI;
    public GameObject targetFoundUI;

    // Use this for initialization
    void Start()
    {
        targetFoundUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        //RoundNumUI.text = "Round: " + Spawner.waveNumber;
        ScoreUI.text = "Score: SCOORE";

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
