using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour 
{
    [SerializeField] BarcoController barco;
    [SerializeField] UiController ui;
    [SerializeField] CameraMovement camara;
    [SerializeField] ObstacleSpawner spawner;

    [SerializeField] int[] pointToReach;
    [SerializeField] int[] dificulties;
    public int Points => maxPointsReached;
    public bool OnGame = false;
    public int maxPointsReached = 0;
    private int dificulty = 1;
    private int currentDificulty = 1;

    public int Dificulty
    {
        get { return dificulty; }
    }

    void Start()
    {
    }

    public void SetUp() 
    {
        barco.DeleteForce();
        //Physics.SyncTransforms();
        barco.CambiarMultiplicadorVelocidad(1);
        barco.bajarLaVelocidad = false;
        maxPointsReached = 0;
    }

    // Update is called once per frame
    void Update(){
        if (OnGame == true)
        {
            maxPointsReached = score(barco.transform.position.z*10, maxPointsReached);
            ui.ShowScore(maxPointsReached-40);
            MoveCamera();
        }
    }
    public int score(float pos, float pos_ant)
    {
        if (pos > pos_ant){
            return Mathf.RoundToInt(pos / 10) * 10;
        }
        else if (pos < pos_ant)
            return Mathf.RoundToInt(pos_ant);
        return 0;
    }

    public void MoveCamera()
    {
        if (maxPointsReached < 100)
            dificulty = 1;

        if (dificulty != currentDificulty)
        {
            if (dificulty == 2)
            {
                spawner.StarLifebuoyCoroutine();
                spawner.StartBoosterCoroutine();
            }

            if (dificulty == 3)
            {
                spawner.StartBuoyCoroutine();
                Debug.Log("Dificultad 3");
            }

            if (dificulty == 4)
            {
                spawner.StartBuqueCoroutine();
                Debug.Log("Dificultad 4");
            }

            currentDificulty = dificulty;
        }

        for (int i = 0; i < pointToReach.Length; i++)
        {
            if (maxPointsReached >= pointToReach[pointToReach.Length - 1] || maxPointsReached > pointToReach[i] && maxPointsReached < pointToReach[i+1])
            {
                dificulty = dificulties[i];
            }
        }
        camara.MoveCamera(dificulty);
    }
}
