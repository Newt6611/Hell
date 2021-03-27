using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject PausePanel;
    private bool isPause;

    [Header("Events")]
    [SerializeField] private LoadSceneEventSO loadSceneEvent;
    [SerializeField] private VoidGameEventSO continueGame_channel;

    [SerializeField] private PlayerSceneTransitionSO fadeInOut;

    [Header("Scenes")]
    [SerializeField] private GameSceneSO[] main_menu;


    [SerializeField] private InputReader inputReader;

    private Animator animator;


    // Temps
    private Vector2 playerNextPosition;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        inputReader.pauseEvent += PauseActoin;
        inputReader.exitMenuEvent += PauseActoin;

        continueGame_channel.eventRaised += ContinueGame;

        fadeInOut.request += FadeIn;
    }

    private void OnDisable() 
    {
        inputReader.pauseEvent -= PauseActoin;
        inputReader.exitMenuEvent -= PauseActoin;

        continueGame_channel.eventRaised -= ContinueGame;
        fadeInOut.request -= FadeIn;
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

    public void BackToMainMenu()
    {
        loadSceneEvent.RaiseEvent(main_menu, false);
    }

    private void ShowPausePanel()
    {
        PausePanel.SetActive(true);
    }

    private void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }


    // Fade In Out
    private void FadeIn(Vector2 position)
    {
        animator.SetTrigger("FadeOut");
        playerNextPosition = position;
    }

    public void SetPlayerPosition()
    {
        if(playerNextPosition != Vector2.zero)
        {
            Player.Instance.SetTransform(playerNextPosition);
            playerNextPosition = Vector2.zero;
        }
        else
        {
            Debug.Log("Player Has No Next Transition Spot!");
        }
    }
}
