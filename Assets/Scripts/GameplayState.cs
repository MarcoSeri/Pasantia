using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayState : GameBaseState
{
    [SerializeField] private GameController gamecontroler;
    [SerializeField] private BarcoController barcocontroler;

    private void Awake() {
        gamecontroler.gameObject.SetActive(false);
        barcocontroler.gameObject.SetActive(false);
    }
    public override void EnterState()
    {
        gamecontroler.gameObject.SetActive(true);
        barcocontroler.gameObject.SetActive(true);
    }

    public override void ExitState()
    {
        gamecontroler.gameObject.SetActive(false);
        barcocontroler.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {

    }

    void Start()
    {

    }
}