using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Encogimientio : MonoBehaviour
{
    [Header("Cambio de tamaño")]
    [SerializeField] private Transform jugador;
    [SerializeField] private Vector3 escalaNormal = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 escalaReducida = new Vector3(0.5f, 0.5f, 0.5f);
    private bool estaEncogido = false;

    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    [SerializeField] private float radioBusqueda = 2f;
    private bool objetoRecogido = false;

    [Header("Zonas restringidas")]
    [SerializeField] private LayerMask zonaPequena;

    [Header("Tutorial UI")]
    [SerializeField] private GameObject panelTutorial;
    [SerializeField] private TextMeshProUGUI textoTutorial;
    [TextArea(3, 5)]
    [SerializeField] private string mensajeTutorial;
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
            CerrarTutorial();
        }

        if (Input.GetKeyDown(KeyCode.R) && objetoRecogido && !tutorialActivo)
        {
            CambiarTamaño();
        }
    }

    private void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider colision in colliders)
        {
            if (colision.CompareTag("Player"))
            {
                Player_Standar movimiento = colision.GetComponent<Player_Standar>();
                if (movimiento != null)
                    movimiento.puedeMoverse = false;

                objetoRecogido = true;

                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                Debug.Log("Cristal recogido. Iniciando tutorial de encogimiento.");
                StartCoroutine(MostrarTutorial());
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger el objeto.");
    }

    private IEnumerator MostrarTutorial()
    {
        panelTutorial.SetActive(true);

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

    private void CerrarTutorial()
    {
        panelTutorial.SetActive(false);

        if (jugador != null)
        {
            Player_Standar movimiento = jugador.GetComponent<Player_Standar>();
            if (movimiento != null)
                movimiento.puedeMoverse = true;
        }

        tutorialActivo = false;
        tutorialListo = false;
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

    private void CambiarTamaño()
    {
        if (!estaEncogido)
        {
            jugador.localScale = escalaReducida;
            estaEncogido = true;
            Debug.Log("El jugador se ha encogido.");
        }
        else
        {
            if (!EstaEnZonaPequena())
            {
                jugador.localScale = escalaNormal;
                estaEncogido = false;
                Debug.Log("El jugador ha vuelto a su tamaño normal.");
            }
            else
            {
                Debug.LogWarning("No puedes agrandarte en una zona pequeña.");
            }
        }
    }

    private bool EstaEnZonaPequena()
    {
        Collider[] colisiones = Physics.OverlapSphere(jugador.position, 0.6f, zonaPequena);
        return colisiones.Length > 0;
    }
}