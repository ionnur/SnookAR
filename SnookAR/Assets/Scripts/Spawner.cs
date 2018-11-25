using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    //set prefabs
    public Rigidbody ballBlack;
    public Rigidbody ballBlue;
    public Rigidbody ballGreen;
    public Rigidbody ballRed;

    public static bool playWave = true;

    public static int ballNumber = 0;

    public static int waveNumber = 0;

    IEnumerator ballTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
        }
    }

    void spawnBall(Rigidbody ballColour)
    {
        Instantiate(ballColour,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity);
    }

    IEnumerator waveTimer()
    {
        while (true)
        {
            if (playWave == true)
            {
                waveNumber++;
                Debug.Log("waveNumber: " + waveNumber);

                //yield return new WaitForSeconds(5);
                //Debug.Log("Wait");
                int r = 2 * waveNumber + 3;
                int be = waveNumber - 2;
                float g = 0.5f * waveNumber - 1.5f;
                float bk = 0.25f * waveNumber - 1.5f;

                while (r >= 1)
                {
                    spawnBall(ballRed);
                    //Debug.Log(waveNumber + "Red");
                    r--;
                    ballNumber++;
                    Debug.Log("ballNumber" + ballNumber);
                    yield return new WaitForSeconds(1f);
                }

                while (be >= 1)
                {
                    spawnBall(ballBlue);
                    //Debug.Log(waveNumber + "Blue");
                    be--;
                    ballNumber++;
                    Debug.Log("ballNumber" + ballNumber);
                    yield return new WaitForSeconds(1f);
                }

                while (g >= 1)
                {
                    spawnBall(ballGreen);
                    //Debug.Log(waveNumber + "Green");
                    g--;
                    ballNumber++;
                    Debug.Log("ballNumber" + ballNumber);
                    yield return new WaitForSeconds(1f);
                }

                while (bk >= 1)
                {
                    spawnBall(ballBlack);
                    //Debug.Log(waveNumber + "Black");
                    bk--;
                    ballNumber++;
                    Debug.Log("ballNumber: " + ballNumber);
                    yield return new WaitForSeconds(1f);
                } 

                playWave = false;
            }
            yield return null;
        }
    }

    void Start () {
        StartCoroutine(waveTimer());
    //    spawnWave();
    }
}
