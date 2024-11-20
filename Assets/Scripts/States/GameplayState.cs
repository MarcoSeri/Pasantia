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
    [SerializeField] private UiManager uiController;
    public Action EndGames;

    private void Awake() {
        Application.targetFrameRate = 60;
        base.Awake();
        borde.gameObject.SetActive(false);
    }
    public override void EnterState()
    {
        uiController.Open("gameplay");
        SoundManager.PlaySound(SoundManager.Sound.Click, 1);
        barcocontroler.ResetPhysics();
        gamecontroler.OnGame = true;
        borde.gameObject.SetActive(true);
        spawner.StartBasicCoroutine();
        barcocontroler.BoatCrashed += EndGame;
        barcocontroler.ResetCamera();
        barcocontroler.ResetPos();

    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(0.25f);
    }

    public override void ExitState()
    {
        uiController.Close("gameplay");
        gamecontroler.OnGame = false;
        barcocontroler.BoatCrashed -= EndGame;
        borde.gameObject.SetActive(false);
        barcocontroler.StopCorroutine();
        spawner.StopSpawner();
        barcocontroler.SeUndioElBarco();
    }

    public override void UpdateState()
    {
    }

    private void EndGame()
    {
        StateManager.NextState();
    }
}
