using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseState : GameBaseState
{
    [SerializeField] private Button Start;
    [SerializeField] private Button Salir;
    [SerializeField] private UiManager uiManager;
    
    public override void EnterState()
    {
        uiManager.Open("lose");
        Start.onClick.AddListener(OnStartPressed);
        Salir.onClick.AddListener(OnSalirPressed);
    }

    void OnStartPressed()
    {
        StateManager.ChangeState(1);
    }
    void OnSalirPressed()
    {

    }

    public override void ExitState()
    {
        uiManager.Close("lose");
    }

    public override void UpdateState()
    {

    }

}
