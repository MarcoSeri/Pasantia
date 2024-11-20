using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject gameplay; //preguntar por esto
    [SerializeField] GameObject lose;
    [SerializeField] GameObject tutorial;

    private void Awake() {
        lose.gameObject.SetActive(false);
        gameplay.gameObject.SetActive(false);
    }

    public void Open(string caso)
    {
        switch (caso)
        {
            case "Menu":
                Menu.SetActive(true);
                break;

            case "lose":
                lose.SetActive(true);
                break;
            case "tutorial":
                tutorial.SetActive(true);
                break;
            case "gameplay":
                gameplay.SetActive(true);
                break;
        }
    }
    public void Close(string caso)
    {
        switch (caso)
        {
            case "Menu":
                Menu.SetActive(false);
                break;

            case "lose":
                lose.SetActive(false);
                break;
            case "tutorial":
                tutorial.SetActive(false);
                break;
            case "gameplay":
                tutorial.SetActive(false);
                break;
        }
    }
    //Tener todos los controladores (UIMenuController, GameplayUIController ...)
    //Metodo que sea abrir un menu (Abrir UIMenuController ej) Usar switch(state) -> UIMenu.clore -> OtroUI.Open
    // por ahora Menu.setactive(false o true)

}
