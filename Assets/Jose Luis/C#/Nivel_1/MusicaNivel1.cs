using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPorNivel : MonoBehaviour
{
    public static MusicaPorNivel instancia;

    [Header("Audios por nivel")]
    public AudioClip musicaNivel1;
    public AudioClip musicaNivel2;
    public AudioClip musicaNivel3;

    private AudioSource audioSource;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.loop = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        string nombre = escena.name.Trim().ToLower();

        if (nombre == "nivel_1_fase_1 bake")
        {
            CambiarMusica(musicaNivel1);
        }
        else if (nombre == "nivel_2_fase_1")
        {
            CambiarMusica(musicaNivel2);
        }
        else if (nombre == "Nivel_3_fase_1")
        {
            CambiarMusica(musicaNivel3);
        }
    }

    private void CambiarMusica(AudioClip nuevaMusica)
    {
        if (nuevaMusica == null) return;

        if (audioSource.clip != nuevaMusica)
        {
            audioSource.clip = nuevaMusica;
            audioSource.Play();
        }
    }

    public void DetenerMusica()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        audioSource.Stop();
        Destroy(gameObject);
    }
}