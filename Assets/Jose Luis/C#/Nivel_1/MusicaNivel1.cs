using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaNivel1 : MonoBehaviour
{
    public static MusicaNivel1 instancia;

    [Header("Nombre exacto de la escena donde debe detenerse la música")]
    [SerializeField] private string nombreEscenaFinNivel1 = "Nivel2_Fase1";

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        if (escena.name == nombreEscenaFinNivel1)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

    public void DetenerMusica()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Destroy(gameObject);
    }
}