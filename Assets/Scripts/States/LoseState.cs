using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseState : GameBaseState
{
    [SerializeField] private Button Start;
    [SerializeField] private Button Salir;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private UiControllerLose uiController;
    [SerializeField] private GameController Gameplay;

    public override void EnterState()
    {
        SoundManager.PlaySound(SoundManager.Sound.BoteRoto, 1);
        uiManager.Open("lose");
        uiController.ShowFinalScore(Gameplay.Points);
        Start.onClick.AddListener(OnStartPressed);
        Salir.onClick.AddListener(OnSalirPressed);
    }

    void OnStartPressed()
    {
        StateManager.ChangeState(3);
    }
    void OnSalirPressed()
    {

    }

    public override void ExitState()
    {
        Start.onClick.RemoveListener(OnStartPressed);
        Salir.onClick.RemoveListener(OnSalirPressed);
        uiManager.Close("lose");
    }

    public override void UpdateState()
    {

    }

}
