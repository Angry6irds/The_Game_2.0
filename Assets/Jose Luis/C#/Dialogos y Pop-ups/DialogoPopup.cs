using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogoConAutor
{
    public enum Emisor { Jugador, Doctora }
    public Emisor autor;
    [TextArea(2, 4)] public string texto;
}

public class DialogoPopup : MonoBehaviour
{
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;
    public GameObject imagenDoctora;
    public float tiempoEntreLineas = 3f;
    [Header("Panel de comandos (opcional)")]
    public GameObject panelComandos;

    private Coroutine mostrandoDialogo;

    public void MostrarDialogos(List<DialogoConAutor> lineas, GameObject colisionadorABorrar = null, ZonaDialogo zonaDialogo = null)
    {
        if (mostrandoDialogo != null)
            StopCoroutine(mostrandoDialogo);

        mostrandoDialogo = StartCoroutine(MostrarSecuencia(lineas, colisionadorABorrar, zonaDialogo));
    }

    private IEnumerator MostrarSecuencia(List<DialogoConAutor> lineas, GameObject colisionadorABorrar, ZonaDialogo zonaDialogo)
    {
        panelDialogo.SetActive(true);

        foreach (DialogoConAutor linea in lineas)
        {
            if (imagenDoctora != null)
                imagenDoctora.SetActive(linea.autor == DialogoConAutor.Emisor.Doctora);

            yield return StartCoroutine(MostrarTextoGradualmente(linea.texto));
            yield return new WaitForSeconds(tiempoEntreLineas);
        }

        panelDialogo.SetActive(false);
        if (imagenDoctora != null) imagenDoctora.SetActive(false);

        if (colisionadorABorrar != null)
            Destroy(colisionadorABorrar);

        // ⚠️ Activar el panel de comandos desde aquí si es necesario
        if (zonaDialogo != null && zonaDialogo.mostrarPanelComandos)
            zonaDialogo.IniciarPanelComandos();
    }

    private IEnumerator MostrarTextoGradualmente(string texto)
    {
        textoDialogo.text = "";
        foreach (char letra in texto)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(0.03f);
        }
    }
}