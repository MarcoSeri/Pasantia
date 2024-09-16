using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class GameBaseState : MonoBehaviour
{
    public abstract void EnterState(GameManager gameManager);
    public abstract void UpdateState(GameManager gameManager);
    public abstract void ExitState(GameManager gameManager);
}
