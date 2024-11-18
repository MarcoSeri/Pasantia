using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialState : GameBaseState
{
    [SerializeField] private Button Start;
    [SerializeField] private UiManager uiManager;

    public override void EnterState()
    {
        SoundManager.PlaySound(SoundManager.Sound.Click, 1);
        uiManager.Open("tutorial");
        Start.onClick.AddListener(OnStartPressed);
    }

    void OnStartPressed()
    {
        StateManager.NextState();
    }
    void OnSalirPressed()
    {
        Debug.Log("Cerrar juego");
    }

    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
        Start.onClick.RemoveListener(OnStartPressed);
        uiManager.Close("tutorial");
    }
}
