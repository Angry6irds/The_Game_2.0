using UnityEngine;
using UnityEngine.SceneManagement;

public class PosicionJugadorPorEscena : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += CambiarPosicionAlCargarEscena;
    }

    private void CambiarPosicionAlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        GameObject jugador = GameObject.Find("Player_Standar");
        if (jugador == null) return;

        switch (escena.name)
        {
            case "Nivel_1_fase_2":
                jugador.transform.position = new Vector3(10f, 10f, 6f);
                break;
            case "Nivel_1_fase_3":
                jugador.transform.position = new Vector3(4f, 5f, 0.8f);
                break;
            case "nivel_2_fase_1":
                jugador.transform.position = new Vector3(-12f, 42f, -150f);
                break;
            case "nivel_2_fase_2":
                jugador.transform.position = new Vector3(10f, 9f, -7f);
                break;
            case "nivel_2_fase_3":
                jugador.transform.position = new Vector3(3f, 70f, 46f);
                break;
            case "Nivel_3_fase_1":
                jugador.transform.position = new Vector3(10f, 10f, -7f);
                break;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= CambiarPosicionAlCargarEscena;
    }
}
