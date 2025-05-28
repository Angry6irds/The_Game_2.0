using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CierreFinal : MonoBehaviour
{
    [Header("Fade")]
    public CanvasGroup panelNegro;
    public float duracionFade = 1.5f;

    [Header("Texto final")]
    public TextMeshProUGUI textoDespedida;
    [TextArea(3, 5)]
    public string mensajeFinal = "Gracias por jugar...";

    [Header("Tiempo para leer el mensaje")]
    public float tiempoMostrarTexto = 3f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activado) return;

        if (other.CompareTag("Player"))
        {
            activado = true;
            StartCoroutine(FinalizarJuego());
        }
    }

    private IEnumerator FinalizarJuego()
    {
        float t = 0f;
        panelNegro.alpha = 0;
        panelNegro.gameObject.SetActive(true);

        // Fade a negro
        while (t < duracionFade)
        {
            t += Time.deltaTime;
            panelNegro.alpha = Mathf.Clamp01(t / duracionFade);
            yield return null;
        }

        panelNegro.alpha = 1f;

        // Mostrar mensaje (ya debe estar activo desde el inspector)
        if (textoDespedida != null)
        {
            textoDespedida.text = mensajeFinal;
            Debug.Log("Mensaje mostrado: " + mensajeFinal);
        }

        yield return new WaitForSeconds(tiempoMostrarTexto);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo para pruebas en editor
#else
        Application.Quit();
#endif
    }
}