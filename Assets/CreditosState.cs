using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditosState : GameBaseState
{
    [SerializeField] private Button Volver;
    [SerializeField] private UiManager uiManager;
    public override void EnterState()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click, 1);
        uiManager.Open("creditos");
        Volver.onClick.AddListener(OnStartPressed);
    }

    public override void ExitState()
    {
        Volver.onClick.RemoveListener(OnStartPressed);
        uiManager.Close("creditos");
    }

    public override void UpdateState()
    {
    }

    void OnStartPressed()
    {
        StateManager.ChangeState(0);
    }
}
