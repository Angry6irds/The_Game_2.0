using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JetPackPrueba : MonoBehaviour
{
    [Header("Configuracion de Jetpack")]
    [SerializeField] private float duracionCombustible = 5f;
    [SerializeField] private float fuerzaJetpack = 6f;
    [SerializeField] private KeyCode teclaVuelo = KeyCode.Space;

    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    [SerializeField] private float radioBusqueda = 2f;
    private bool jetpackRecogido = false;

    [Header("Jugador")]
    [SerializeField] private Transform jugador;
    private CharacterController controller;

    [Header("UI Combustible")]
    [SerializeField] private Image barraCombustible;

    private float combustibleActual;
    private bool puedeVolar = false;
    private Vector3 direccionVertical;
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
        if (!jetpackRecogido && Input.GetKeyDown(teclaRecojer))
        {
            IntentarRecoger();
        }
        if (tutorialActivo && tutorialListo && Input.GetKeyDown(KeyCode.E))
        {
            CerrarTutorial();
        }

        if (jetpackRecogido && puedeVolar && !tutorialActivo)
        {
            ControlarVuelo();
        }
    }

    void IntentarRecoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                jugador = col.transform;
                controller = jugador.GetComponent<CharacterController>();

                if (controller != null)
                {
                    jetpackRecogido = true;
                    puedeVolar = true;
                    combustibleActual = duracionCombustible;

                    // Ocultar objeto
                    MeshRenderer m = GetComponent<MeshRenderer>();
                    if (m != null) m.enabled = false;

                    Collider c = GetComponent<Collider>();
                    if (c != null) c.enabled = false;

                    Debug.Log("Jetpack recogido.");
                    Player_Standar movimiento = jugador.GetComponent<Player_Standar>();
                    if (movimiento != null)
                        movimiento.puedeMoverse = false;

                    StartCoroutine(MostrarTutorial());
                    return;
                }
            }
        }

        Debug.Log("No hay jugador cerca para recoger el jetpack.");
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

    void ControlarVuelo()
    {
        if (jugador == null || controller == null) return;

        bool enElSuelo = controller.isGrounded;

        if (enElSuelo)
        {
            combustibleActual = duracionCombustible;

            if (barraCombustible != null)
                barraCombustible.fillAmount = 1f;
        }

        if (Input.GetKey(teclaVuelo) && combustibleActual > 0f)
        {
            combustibleActual -= Time.deltaTime;

            if (barraCombustible != null)
                barraCombustible.fillAmount = combustibleActual / duracionCombustible;

            //Aplica impulso directamente (sin * Time.deltaTime)
            jugador.GetComponent<Player_Standar>().impulsoJetpack = fuerzaJetpack;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}