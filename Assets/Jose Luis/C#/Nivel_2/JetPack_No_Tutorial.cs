using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPack_No_Tutorial : MonoBehaviour
{
    [Header("Configuración de Jetpack")]
    [SerializeField] private float duracionCombustible = 5f;
    [SerializeField] private float fuerzaJetpack = 6f;
    [SerializeField] private KeyCode teclaVuelo = KeyCode.Space;

    [Header("Recoger cristal")]
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

    void Update()
    {
        if (!jetpackRecogido && Input.GetKeyDown(teclaRecojer))
        {
            IntentarRecoger();
        }

        if (jetpackRecogido && puedeVolar)
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
                    return;
                }
            }
        }

        Debug.Log("No hay jugador cerca para recoger el jetpack.");
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

            jugador.GetComponent<Player_Standar>().impulsoJetpack = fuerzaJetpack;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}
