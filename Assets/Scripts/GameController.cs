using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    [SerializeField] BarcoController barco;
    [SerializeField] UiController ui;
    [SerializeField] CameraMovement camara;

    [SerializeField] int[] pointToReach;
    [SerializeField] float[] dificulties;
    public int Points => maxPointsReached;
    public bool OnGame = false;
    int maxPointsReached = 0;
    float dificulty = 1;

    void Start()
    {
    }

    public void SetUp() 
    {
        barco.DeleteForce();
        camara.transform.position = new Vector3(camara.transform.position.x, camara.transform.position.y,0);
        barco.transform.position = new Vector3(0, barco.transform.position.y, 0);
        barco.transform.rotation = new Quaternion(0, 0, 0, 0);
        maxPointsReached = 0;
        //barco.DeleteForce();
    }

    // Update is called once per frame
    void Update(){
        if (OnGame == true)
        {
            maxPointsReached = score(barco.transform.position.z*10, maxPointsReached);
            ui.ShowScore(maxPointsReached);
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
            dificulty = 1f;
 
        for (int i = 0; i < pointToReach.Length; i++)
        {
            if (maxPointsReached > pointToReach[i])
            {
                dificulty = dificulties[i];
            }
        }

        camara.MoveCamera(dificulty);
    }
}
