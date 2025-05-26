using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportadorPrueba : MonoBehaviour
{
    [Header("Controles")]
    [SerializeField] private KeyCode teclaRecolectar = KeyCode.E;
    [SerializeField] private KeyCode teclaTeletransporte = KeyCode.T;
    [SerializeField] private float radioRecoleccion = 2f;
    [SerializeField] private float radioTeletransporte = 5f;
    [SerializeField] private float tiempoEnfriamiento = 5f;

    [Header("Estado")]
    private bool objetoRecogido = false;
    private bool puedeTeletransportarse = true;
    private bool seleccionActiva = false;

    [Header("Jugador y visuales")]
    [SerializeField] private GameObject jugador;
    [SerializeField] private GameObject circuloZonaVisual; // un objeto visual tipo proyector o círculo

    [Header("Tags")]
    [SerializeField] private string tagObjetoRecolectable = "Objeto_Teletransportador";

    void Start()
    {
        if (circuloZonaVisual != null)
            circuloZonaVisual.SetActive(false);
    }

    void Update()
    {
        if (!objetoRecogido && Input.GetKeyDown(teclaRecolectar))
        {
            IntentarRecolectar();
        }

        if (objetoRecogido && Input.GetKeyDown(teclaTeletransporte) && puedeTeletransportarse)
        {
            ActivarSeleccionDeDestino();
        }

        if (seleccionActiva && Input.GetMouseButtonDown(0))
        {
            IntentarTeletransportar();
        }
    }

    void IntentarRecolectar()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioRecoleccion);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag(tagObjetoRecolectable))
            {
                objetoRecogido = true;
                col.gameObject.SetActive(false);
                Debug.Log("Objeto de teletransporte recogido.");
                break;
            }
        }
    }

    void ActivarSeleccionDeDestino()
    {
        seleccionActiva = true;
        if (circuloZonaVisual != null)
        {
            circuloZonaVisual.transform.position = jugador.transform.position;
            circuloZonaVisual.transform.localScale = Vector3.one * (radioTeletransporte * 2f);
            circuloZonaVisual.SetActive(true);
        }

        Debug.Log("Selecciona una posicion dentro del rango para teletransportarte.");
    }

    void IntentarTeletransportar()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            float distancia = Vector3.Distance(jugador.transform.position, hit.point);
            if (distancia <= radioTeletransporte)
            {
                jugador.transform.position = hit.point;
                Debug.Log("Jugador teletransportado.");
                StartCoroutine(CooldownTeletransporte());
            }
            else
            {
                Debug.Log("Punto fuera del rango de teletransporte.");
            }
        }

        seleccionActiva = false;
        if (circuloZonaVisual != null)
            circuloZonaVisual.SetActive(false);
    }

    IEnumerator CooldownTeletransporte()
    {
        puedeTeletransportarse = false;
        yield return new WaitForSeconds(tiempoEnfriamiento);
        puedeTeletransportarse = true;
        Debug.Log("Habilidad de teletransporte disponible otra vez.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioRecoleccion);

        if (jugador != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(jugador.transform.position, radioTeletransporte);
        }
    }
}