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
    [SerializeField] private ObstacleSpawner spawner;
    public Action EndGames;

    private void Awake() {
        base.Awake();
        borde.gameObject.SetActive(false);
    }
    public override void EnterState()
    {
        gamecontroler.OnGame = true;
        borde.gameObject.SetActive(true);
        spawner.StartBasicCoroutine();
        barcocontroler.BoatCrashed += EndGame;
        gamecontroler.SetUp();
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.25f);
    }

    public override void ExitState()
    {
        gamecontroler.OnGame = false;
        barcocontroler.BoatCrashed -= EndGame;
        borde.gameObject.SetActive(false);
        barcocontroler.DeleteForce();
        barcocontroler.StopCorroutine();
        spawner.StopSpawner();
    }

    public override void UpdateState()
    {
    }

    private void EndGame()
    {
        StateManager.NextState();
    }
}
