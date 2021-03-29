using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private const float maxTime = 140;
    private float timer;
    private bool isPaused = true;
    [SerializeField] private Image timerBar;
    [SerializeField] private Image[] stars;


    public void StartLevel()
    {
        Refresh();
        isPaused = false;
    }

    public void Refresh()
    {
        timer = maxTime;
        foreach (Image star in stars)
        {
            star.color = Color.white;
        }
    }

    public void CountDown()
    {
        if (!isPaused)
        {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / maxTime;
        }
    }

    private void SetThreeStar()
    {

    }

    public void SetStatus(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public int GetStar()
    {
        isPaused = true;
        if (timer >= 80)
        {
            return 3;
        }
        else if (timer >= 40)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
