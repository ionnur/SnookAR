using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTNextWave : MonoBehaviour {

    // Use this for initialization
    public void NextWaveLoad()
    {
        if (Spawner.ballNumber == 0 && Spawner.playWave == false)
        {
            Spawner.playWave = true;
        }
    }
}
