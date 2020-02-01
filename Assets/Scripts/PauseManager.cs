using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused;

    public GameObject paused;

    private void Start()
    {
        isPaused = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //attivare o disattivare la pausa
            ChangePauseStatus();
        }
    }

    public void ChangePauseStatus()
    {
        isPaused = !isPaused;
        UpdateGamePause();
    }

    void UpdateGamePause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            GameManager.isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            GameManager.isPaused = false;
        }
        paused.SetActive(isPaused);
        
    }
}
