using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    
    public Text score;

    [HideInInspector]
    public int scoreCount;

    public Text bestscore;

    [HideInInspector]
    public int bestScore;

    private bool paused = false;


    void Update()
    {
        score.text = scoreCount.ToString();
        bestscore.text = PlayerPrefs.GetInt("bestscore").ToString();

        if (bestScore < scoreCount)
        {
            PlayerPrefs.SetInt("bestscore", scoreCount);
        }

    }


}
