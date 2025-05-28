using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AumentoVelocidad : MonoBehaviour
{
    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Rango adquirido")]
    [SerializeField] private float radioBusqueda = 2f;

    private GameObject jugador;
    private Player_Standar movimientoJugador;

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
            tutorialActivo = false;
            tutorialListo = false;

            if (jugador != null)
            {
                var p = jugador.GetComponent<Player_Standar>();
                if (p != null) p.puedeMoverse = true;
            }
        }

        if (objetoRecogido && jugador != null && movimientoJugador != null && !tutorialActivo)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movimientoJugador.velActual = movimientoJugador.velCaminata * 4f;
            }
            else
            {
                movimientoJugador.velActual = movimientoJugador.velCaminata;
            }
        }
    }

    public void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                jugador = col.gameObject;
                movimientoJugador = jugador.GetComponent<Player_Standar>();

                if (movimientoJugador == null)
                {
                    Debug.LogError("El jugador no tiene el componente 'Player_Standar'.");
                    return;
                }

                objetoRecogido = true;

                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                // Bloquear movimiento y mostrar tutorial
                movimientoJugador.puedeMoverse = false;
                StartCoroutine(MostrarTutorial());

                Debug.Log("Cristal de velocidad recogido.");
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

        tutorialActivo = true;
        tutorialListo = true;
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}