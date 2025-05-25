using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroVideoControl : MonoBehaviour
{
    public TransicionEscena transicion;
    public VideoPlayer videoPlayer;
    public string siguienteEscena = "Nivel_1_fase_1 Bake";

    private void Start()
    {
        StartCoroutine(ReproducirIntro());
    }

    private IEnumerator ReproducirIntro()
    {
        // Fade desde negro
        yield return StartCoroutine(transicion.FadeDesdeNegro());

        // Esperar a que el video termine
        videoPlayer.Play();
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Fade a negro
        yield return StartCoroutine(FadeANegroYContinuar());
    }

    private IEnumerator FadeANegroYContinuar()
    {
        CanvasGroup cg = transicion.panelFade.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = transicion.panelFade.AddComponent<CanvasGroup>();

        cg.alpha = 0f;
        float t = 0f;
        float duracion = transicion.duracionFade;

        while (t < duracion)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(t / duracion);
            yield return null;
        }

        SceneManager.LoadScene(siguienteEscena);
    }
}