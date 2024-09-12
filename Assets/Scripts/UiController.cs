using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TMP_Text distancia;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowScore(int score)
    {
        distancia.text = string.Format("Score: {0}", score);
    }
}
