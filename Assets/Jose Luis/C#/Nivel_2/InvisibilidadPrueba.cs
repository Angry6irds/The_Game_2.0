using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Invisibilidad : MonoBehaviour
{
    [Header("Recolección del cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Activación de invisibilidad")]
    [SerializeField] private KeyCode teclaInvisibilidad = KeyCode.H;
    private bool enTemporizador = false;

    [Header("Rango de recogida")]
    [SerializeField] private float radioBusqueda = 2f;

    [Header("Duración de invisibilidad")]
    [SerializeField] private float duracionInvisibilidad = 5f;

    private GameObject jugador;

    [Header("Tutorial UI")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private TextMeshProUGUI textoTutorial;
    [TextArea(3, 5)][SerializeField] private string mensajeTutorial;
    [SerializeField] private List<GameObject> imagenesTutorial;
    [SerializeField] private float tiempoEntreImagenes = 1.2f;

    private bool tutorialActivo = false;
    private bool tutorialListo = false;

    void Update()
    {
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        if (tutorialActivo && tutorialListo && Input.GetKeyDown(KeyCode.E))
        {
            panelTutorial.SetActive(false);

            Player_Standar p = jugador.GetComponent<Player_Standar>();
            if (p != null) p.puedeMoverse = true;

            tutorialActivo = false;
        }

        if (objetoRecogido && !enTemporizador && !tutorialActivo && Input.GetKeyDown(teclaInvisibilidad))
        {
            StartCoroutine(TemporizadorInvisibilidad());
        }
    }

    void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider colision in colliders)
        {
            if (colision.CompareTag("Player"))
            {
                jugador = colision.gameObject;

                objetoRecogido = true;

                MeshRenderer mesh = GetComponent<MeshRenderer>();
                if (mesh != null) mesh.enabled = false;

                Collider tocar = GetComponent<Collider>();
                if (tocar != null) tocar.enabled = false;

                Player_Standar p = jugador.GetComponent<Player_Standar>();
                if (p != null) p.puedeMoverse = false;

                StartCoroutine(MostrarTutorial());

                Debug.Log("Cristal de invisibilidad recogido.");
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger el cristal.");
    }

    private IEnumerator MostrarTutorial()
    {
        if (panelTutorial != null) panelTutorial.SetActive(true);

        if (textoTutorial != null)
            yield return StartCoroutine(EscribirTextoGradualmente(mensajeTutorial));

        foreach (GameObject img in imagenesTutorial)
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

        tutorialListo = true;
        tutorialActivo = true;
    }

    IEnumerator TemporizadorInvisibilidad()
    {
        enTemporizador = true;

        if (jugador != null)
        {
            string tagOriginal = jugador.tag;
            jugador.tag = "Invisible";

            Renderer[] renders = jugador.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renders)
            {
                r.enabled = false;
            }

            Debug.Log("Jugador invisible.");

            yield return new WaitForSeconds(duracionInvisibilidad);

            jugador.tag = tagOriginal;
            foreach (Renderer r in renders)
            {
                r.enabled = true;
            }

            Debug.Log("Invisibilidad terminada.");
        }

        enTemporizador = false;
    }

    private IEnumerator EscribirTextoGradualmente(string texto)
    {
        textoTutorial.text = "";
        foreach (char letra in texto)
        {
            textoTutorial.text += letra;
            yield return new WaitForSeconds(0.03f);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}