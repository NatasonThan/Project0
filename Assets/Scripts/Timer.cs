using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    float elapsedTime;

    public int ElapsedTime
    {
        get { return (int)elapsedTime; }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }
}
