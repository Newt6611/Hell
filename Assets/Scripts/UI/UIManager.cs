using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject PausePanel;
    private bool isPause;

    [Header("Events")]
    [SerializeField] private VoidGameEventSO continueGame_channel;

    [SerializeField] private InputReader inputReader;

    private void OnEnable() 
    {
        inputReader.pauseEvent += PauseActoin;
        inputReader.exitMenuEvent += PauseActoin;

        continueGame_channel.eventRaised += ContinueGame;
    }

    private void OnDisable() 
    {
        inputReader.pauseEvent -= PauseActoin;
        inputReader.exitMenuEvent -= PauseActoin;

        continueGame_channel.eventRaised -= ContinueGame;
    }

    private void PauseActoin()
    {
        isPause = !isPause;

        if(isPause)
        {
            Time.timeScale = 0;
            ShowPausePanel();
            inputReader.DisablePlayer();
        }
        else
        {
            Time.timeScale = 1;
            ClosePausePanel();
            inputReader.EnablePlayer();
        }
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        isPause = false;
        ClosePausePanel();
        inputReader.EnablePlayer();
    }

    private void ShowPausePanel()
    {
        PausePanel.SetActive(true);
    }

    private void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }
}
