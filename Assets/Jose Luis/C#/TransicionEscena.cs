using UnityEngine;
using System.Collections;

public class TransicionEscena : MonoBehaviour
{
    public GameObject panelFade; // Panel con CanvasGroup
    public float duracionFade = 1.5f;
    public Player_Standar jugador;

    private bool enTransicion = false;

    public void IniciarTransicion(string nombreEscena)
    {
        if (!enTransicion)
            StartCoroutine(FadeYEjecutar(nombreEscena));
    }

    private IEnumerator FadeYEjecutar(string escena)
    {
        enTransicion = true;
        if (jugador != null) jugador.puedeMoverse = false;

        CanvasGroup cg = panelFade.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = panelFade.AddComponent<CanvasGroup>();

        cg.alpha = 0f;
        float t = 0f;
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(t / duracionFade);
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(escena);
    }

    public IEnumerator FadeDesdeNegro()
    {
        CanvasGroup cg = panelFade.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = panelFade.AddComponent<CanvasGroup>();

        cg.alpha = 1f;
        float t = duracionFade;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            cg.alpha = Mathf.Clamp01(t / duracionFade);
            yield return null;
        }

        if (jugador != null) jugador.puedeMoverse = true;
    }
}
