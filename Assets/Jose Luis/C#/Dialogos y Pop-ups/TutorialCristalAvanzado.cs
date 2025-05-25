using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialCristalAvanzado : MonoBehaviour
{
    [Header("Texto del tutorial")]
    [TextArea(3, 5)]
    public string mensaje;

    [Header("UI del tutorial")]
    public GameObject panelTutorial;
    public TextMeshProUGUI textoTutorial;
    public List<GameObject> imagenesTutorial; // Las imágenes a mostrar en orden

    [Header("Tiempo entre imágenes")]
    public float tiempoEntreImagenes = 1.2f;

    private bool jugadorEnTutorial = false;
    private GameObject jugador;
    private bool tutorialListo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador = other.gameObject;

            Player_Standar control = jugador.GetComponent<Player_Standar>();
            if (control != null)
                control.puedeMoverse = false;

            StartCoroutine(MostrarTutorial());

            gameObject.SetActive(false);
        }
    }

    private IEnumerator MostrarTutorial()
    {
        panelTutorial.SetActive(true);
        textoTutorial.text = mensaje;

        // Ocultar todas las imágenes primero
        foreach (GameObject img in imagenesTutorial)
            img.SetActive(false);

        // Mostrar imágenes una por una con pausa
        foreach (GameObject img in imagenesTutorial)
        {
            img.SetActive(true);
            yield return new WaitForSeconds(tiempoEntreImagenes);
        }

        // Una vez terminado, permitir cerrar el tutorial
        tutorialListo = true;
        jugadorEnTutorial = true;
    }

    private void Update()
    {
        if (jugadorEnTutorial && tutorialListo && Input.GetKeyDown(KeyCode.E))
        {
            panelTutorial.SetActive(false);

            Player_Standar control = jugador.GetComponent<Player_Standar>();
            if (control != null)
                control.puedeMoverse = true;

            jugadorEnTutorial = false;
        }
    }
}