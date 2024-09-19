using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuState : GameBaseState
{
    [SerializeField] private Button Start;
    [SerializeField] private Button Salir;
    [SerializeField] private UiManager uiManager;
   
    public override void EnterState()
    {
        uiManager.Open("Menu");
        Start.onClick.AddListener(OnStartPressed);
        Salir.onClick.AddListener(OnSalirPressed);
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

        //Si toco Start Al state manager pasar a un next state (Statemanager.nextstate())
    }
    public override void ExitState()
    {
        Start.onClick.RemoveListener(OnStartPressed);
        Salir.onClick.RemoveListener(OnSalirPressed);
        uiManager.Close("Menu");
    }

}
