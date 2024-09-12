using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] BarcoController barco;
    [SerializeField] UiController ui;
    
    int maxPointsReached = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        maxPointsReached = score(barco.transform.position.z*10, maxPointsReached);
        ui.ShowScore(maxPointsReached);
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
}
