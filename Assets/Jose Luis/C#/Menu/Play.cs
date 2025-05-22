using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void jugar()
    {
        SceneManager.LoadScene("Nivel_1_fase_1 Bake");
    }
    public void opciones()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void salir()
    {
        Debug.Log("Estas saliendo del juego, vuelve pronto :D");
        Application.Quit();
    }
}
