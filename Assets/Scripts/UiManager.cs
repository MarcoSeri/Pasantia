using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject gameplay;

    public void Open(string caso)
    {
        switch (caso)
        {
            case "Menu":
                Menu.SetActive(true);
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
        }
    }
    //Tener todos los controladores (UIMenuController, GameplayUIController ...)
    //Metodo que sea abrir un menu (Abrir UIMenuController ej) Usar switch(state) -> UIMenu.clore -> OtroUI.Open
    // por ahora Menu.setactive(false o true)

}
