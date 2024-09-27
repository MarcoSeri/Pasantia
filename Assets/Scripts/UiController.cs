using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] private TMP_Text distancia;

    public void ShowScore(int score)
    {
        distancia.text = string.Format("Puntuacion: {0}", score);
    }

}
