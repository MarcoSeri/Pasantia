using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class GameBaseState : MonoBehaviour
{
    GameManager gameManager;
    protected StateManager StateManager;
    private void Awake()
    {
        StateManager = GetComponent<StateManager>();            
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();

}
