using System;
using UnityEngine;

public class Play : MonoBehaviour
{
    public TransicionEscena transicion; // Asignar el objeto con el PanelFade

    public void menu()
    {
        CargarEscenaConFade("Menu");
    }

    public void jugar()
    {
        CargarEscenaConFade("Video_intro");
    }

    public void opciones()
    {
        CargarEscenaConFade("Opciones");
    }

    public void creditos()
    {
        CargarEscenaConFade("Creditos");
    }

    public void salir()
    {
        Debug.Log("Estas saliendo del juego, vuelve pronto :D");
        Application.Quit();
    }

    private void CargarEscenaConFade(string nombreEscena)
    {
        if (transicion != null)
        {
            transicion.IniciarTransicion(nombreEscena);
        }
        else
        {
            Debug.LogWarning("Transición no asignada, cargando escena directamente.");
            UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscena);
        }
    }
}