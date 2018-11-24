using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GUIText : MonoBehaviour
{

    public Text RoundNumUI, ScoreUI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //RoundNumUI.text = "Round: " + Spawner.waveNumber;
        ScoreUI.text = "Score: SCOORE";


    }
}
