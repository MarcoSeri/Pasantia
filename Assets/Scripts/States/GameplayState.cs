using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayState : GameBaseState
{
    [SerializeField] private GameController gamecontroler;
    [SerializeField] private BarcoController barcocontroler;
    [SerializeField] private ColliderController borde;
   
    private void Awake() {
        base.Awake();
        borde.gameObject.SetActive(false);
    }
    public override void EnterState()
    {
        gamecontroler.OnGame = true;
        borde.gameObject.SetActive(true);
        borde.BoatCrashed += EndGame;
        gamecontroler.SetUp();
    }

    public override void ExitState()
    {
        gamecontroler.OnGame = false;
        borde.BoatCrashed -= EndGame;
        borde.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
    }

    private void EndGame()
    {
        StateManager.ChangeState(2);     
    }
}
