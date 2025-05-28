using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ZonaDialogo : MonoBehaviour
{
    public List<DialogoConAutor> mensajes = new List<DialogoConAutor>();

    [Header("Colisionador que se eliminará al terminar los diálogos (opcional)")]
    public GameObject colisionadorBloqueo;

    private bool yaActivado = false;

    [Header("Mostrar panel de comandos básicos")]
    public bool mostrarPanelComandos = false;

    [Header("Panel de comandos")]
    public GameObject panelComandos;
    public List<GameObject> imagenesComandos;
    public float tiempoEntreImagenes = 1.2f;

    private bool comandosListos = false;
    private bool esperandoCerrar = false;

    private void OnTriggerEnter(Collider other)
    {
        if (yaActivado) return;

        if (other.CompareTag("Player"))
        {
            JugadorDialogo jugador = other.GetComponent<JugadorDialogo>();
            if (jugador != null)
            {
                jugador.ActivarDialogos(mensajes, colisionadorBloqueo, this);
                yaActivado = true;
            }
        }
    }

    // 🔁 Este método lo llama el sistema de diálogo al terminar los textos
    public void IniciarPanelComandos()
    {
        StartCoroutine(MostrarPanelComandos());
    }

    private IEnumerator MostrarPanelComandos()
    {
        if (panelComandos != null) panelComandos.SetActive(true);

        foreach (GameObject img in imagenesComandos)
        {
            if (img != null)
            {
                CanvasGroup cg = img.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    img.SetActive(true);
                    yield return StartCoroutine(FadeInCanvasGroup(cg, 0.5f));
                }
                else
                {
                    img.SetActive(true);
                }

                yield return new WaitForSeconds(tiempoEntreImagenes);
            }
        }

        comandosListos = true;
        esperandoCerrar = true;
    }

    private void Update()
    {
        if (esperandoCerrar && comandosListos && Input.GetKeyDown(KeyCode.E))
        {
            if (panelComandos != null) panelComandos.SetActive(false);
            esperandoCerrar = false;
        }
    }

    private IEnumerator FadeInCanvasGroup(CanvasGroup cg, float duracion)
    {
        float tiempo = 0f;
        cg.alpha = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            cg.alpha = Mathf.Clamp01(tiempo / duracion);
            yield return null;
        }

        cg.alpha = 1f;
    }
}