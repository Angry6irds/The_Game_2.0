using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CinematicaControl : MonoBehaviour
{
    [Header("Referencias de video")]
    public VideoPlayer videoPlayer;
    public GameObject videoPantalla;

    [Header("Fade")]
    public CanvasGroup pantallaNegra;
    public float duracionFade = 1.5f;

    [Header("Sistema de diálogo")]
    public JugadorDialogo jugadorDialogo;

    [HideInInspector] public bool CinematicaTerminada = false;

    public void IniciarCinematica(List<DialogoConAutor> dialogosDoctora, List<DialogoConAutor> dialogosJugador)
    {
        StartCoroutine(FlujoCinematica(dialogosDoctora, dialogosJugador));
    }

    private IEnumerator FlujoCinematica(List<DialogoConAutor> doctora, List<DialogoConAutor> jugador)
    {
        CinematicaTerminada = false;

        yield return StartCoroutine(Fade(1));

        videoPantalla.SetActive(true);
        videoPlayer.Play();

        yield return StartCoroutine(Fade(0));

        jugadorDialogo.ActivarDialogos(doctora, null);
        while (jugadorDialogo.dialogoUI.panelDialogo.activeSelf)
            yield return null;

        yield return StartCoroutine(Fade(1));

        videoPlayer.Stop();
        videoPantalla.SetActive(false);

        yield return StartCoroutine(Fade(0));

        jugadorDialogo.ActivarDialogos(jugador, null);
        while (jugadorDialogo.dialogoUI.panelDialogo.activeSelf)
            yield return null;

        CinematicaTerminada = true;
    }

    private IEnumerator Fade(float objetivoAlpha)
    {
        float tiempo = 0;
        float alphaInicial = pantallaNegra.alpha;

        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracionFade;
            pantallaNegra.alpha = Mathf.Lerp(alphaInicial, objetivoAlpha, t);
            yield return null;
        }

        pantallaNegra.alpha = objetivoAlpha;
    }
}