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
        gamecontroler.SetUp();
       // StartCoroutine(WaitToStart());
        gamecontroler.OnGame = true;
        borde.gameObject.SetActive(true);
        borde.BoatCrashed += EndGame;       
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.25f);
    }

    public override void ExitState()
    {
        gamecontroler.OnGame = false;
        borde.BoatCrashed -= EndGame;
        borde.gameObject.SetActive(false);
        barcocontroler.DeleteForce();
    }

    public override void UpdateState()
    {
    }

    private void EndGame()
    {
        StateManager.NextState();
    }
}
