using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.Profiling;
using System.Collections.Generic;
using System.Collections;

public class Prueba_SaltoDoble : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private TMPro.TextMeshProUGUI textoTutorial;
    [SerializeField] private string mensajeTutorial;

    [SerializeField] private List<GameObject> imagenesTutorial;
    [SerializeField] private float tiempoEntreImagenes = 1.2f;

    private GameObject jugador;
    private bool tutorialActivo = false;
    private bool tutorialListo = false;
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    [SerializeField] private float radioBusqueda = 2f; // Rango para buscar al jugador
    private bool recogido = false;

    private IEnumerator MostrarTutorial()
    {
        if (panelTutorial != null) panelTutorial.SetActive(true);

        if (textoTutorial != null)
            yield return StartCoroutine(EscribirTextoGradualmente(mensajeTutorial));

        // Fade de imágenes una por una
        foreach (GameObject img in imagenesTutorial)
        {
            if (img != null)
            {
                CanvasGroup cg = img.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    img.SetActive(true); // Asegúrate que esté activo
                    yield return StartCoroutine(FadeInCanvasGroup(cg, 0.5f));
                }
                else
                {
                    img.SetActive(true); //por si no tiene CanvasGroup
                }

                yield return new WaitForSeconds(tiempoEntreImagenes);
            }
        }

        tutorialListo = true;
        tutorialActivo = true;
    }

    private void Update()
    {
        if (tutorialActivo && tutorialListo && Input.GetKeyDown(KeyCode.E))
        {
            if (panelTutorial != null) panelTutorial.SetActive(false);

            if (jugador != null)
            {
                Player_Standar p = jugador.GetComponent<Player_Standar>();
                if (p != null) p.puedeMoverse = true;
            }

            tutorialActivo = false;
        }

        // Esto sigue siendo necesario:
        if (!recogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }
    }

    private void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);

        foreach (Collider colision in colliders)
        {
            if (colision.CompareTag("Player"))
            {
                PruebaSaltar scriptSalto = colision.GetComponent<PruebaSaltar>();
                if (scriptSalto != null)
                {
                    scriptSalto.ActivarDobleSalto();
                    recogido = true;

                    // Guardamos al jugador
                    jugador = colision.gameObject;

                    // Ocultamos visualmente el cristal
                    MeshRenderer m = GetComponent<MeshRenderer>();
                    if (m != null) m.enabled = false;

                    Collider c = GetComponent<Collider>();
                    if (c != null) c.enabled = false;

                    // Iniciar tutorial
                    Player_Standar p = jugador.GetComponent<Player_Standar>();
                    if (p != null) p.puedeMoverse = false;

                    StartCoroutine(MostrarTutorial());

                    Debug.Log("Doble salto activado y tutorial iniciado.");
                    return;
                }
            }
        }

        Debug.Log("No hay jugador cerca para recoger el objeto.");
    }


    // Mostrar la esfera de busqueda en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
    private IEnumerator EscribirTextoGradualmente(string texto)
    {
        textoTutorial.text = "";
        foreach (char letra in texto)
        {
            textoTutorial.text += letra;
            yield return new WaitForSeconds(0.03f); // Velocidad de escritura
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
