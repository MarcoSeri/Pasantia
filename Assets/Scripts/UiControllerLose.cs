using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiControllerLose : MonoBehaviour
{

    [SerializeField] private TMP_Text Score;

    public void ShowFinalScore(int score)
    {
        Score.text = string.Format("GG hiciste {0} puntos ", score);
    }
}
