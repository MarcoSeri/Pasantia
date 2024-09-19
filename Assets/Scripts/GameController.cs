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
    int maxPointsReached = 0;
    float dificulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        maxPointsReached = score(barco.transform.position.z*10, maxPointsReached);
        ui.ShowScore(maxPointsReached);
        MoveCamera();
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
