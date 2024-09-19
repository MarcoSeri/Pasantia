using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public List<GameBaseState> States;
    private int currentStateIndex=0;
    private GameBaseState currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = States[currentStateIndex];
        currentState.EnterState();  
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void NextState()
    {
        currentState.ExitState();
        currentStateIndex++;
        currentState = States[currentStateIndex];
        currentState.EnterState();
    }

    public void ChangeState(int index)
    {
        currentState.ExitState();
        currentState = States[index];
        currentState.EnterState();
    }
    //Saber el estado actual CurrentState()
    //Pasar al siguiente estado NextState() -> currentState.Exit, Current=next, currentState.Enter
    //Volver al estado anterior BackState()
    //Ir a un state en especifico ChangeState(State)
}