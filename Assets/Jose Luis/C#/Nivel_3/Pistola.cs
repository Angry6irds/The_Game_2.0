using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [Header("Recojer pistola")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Rango de recogida")]
    [SerializeField] private float radioBusqueda = 2f;

    [Header("Uso de pistola")]
    [SerializeField] private Transform puntoDisparo; 
    [SerializeField] private float distanciaMaxima = 10f;
    [SerializeField] private float fuerzaMovimiento = 600f;

    [Header("Visual de pistola")]
    [SerializeField] private GameObject pistolaVisual;

    [Header("Filtro por Layer")]
    [SerializeField] private LayerMask capaRaycasts;
    [SerializeField] private float radioDisparo = 1.5f;

    private Rigidbody objetoSujeto = null;
    private LineRenderer linea;

    void Start()
    {
        // buscar el LineRenderer dentro del visual de la pistola
        if (pistolaVisual != null)
        {
            linea = pistolaVisual.GetComponent<LineRenderer>();
            if (linea != null)
            {
                linea.enabled = false; // desactivar al inicio
            }
        }
    }

    void Update()
    {
        if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
        {
            Recoger();
        }

        if (objetoRecogido)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IntentarAgarrarObjeto();
                ActivarLinea(true);
            }

            if (Input.GetMouseButton(0) && objetoSujeto != null)
            {
                MoverObjeto();
                ActualizarLinea();
            }

            if (Input.GetMouseButtonUp(0))
            {
                SoltarObjeto();
                ActivarLinea(false);
            }
        }
    }

    void Recoger()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioBusqueda);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                objetoRecogido = true;

                if (pistolaVisual != null)
                {
                    pistolaVisual.SetActive(true);
                    pistolaVisual.transform.SetParent(col.transform); // hacer hijo del jugador
                    pistolaVisual.transform.localPosition = new Vector3(0.3f, 3.5f, 0.5f); // ajustar segun tu modelo
                    pistolaVisual.transform.localRotation = Quaternion.identity;
                }

                MeshRenderer m = GetComponent<MeshRenderer>();
                if (m != null) m.enabled = false;

                Collider c = GetComponent<Collider>();
                if (c != null) c.enabled = false;

                Debug.Log("Pistola recogida.");
                return;
            }
        }

        Debug.Log("No hay jugador cerca para recoger la pistola.");
    }

    public void IntentarAgarrarObjeto()
    {
        Ray rayo = new Ray(puntoDisparo.position, puntoDisparo.forward);

        if (Physics.SphereCast(rayo, radioDisparo, out RaycastHit hit, distanciaMaxima))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("raycasts"))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    objetoSujeto = rb;
                    objetoSujeto.useGravity = false;
                    objetoSujeto.drag = 10f;
                    Debug.Log("Objeto sujeto con SphereCast.");
                }
            }
        }
    }

    void MoverObjeto()
    {
        Vector3 puntoObjetivo = puntoDisparo.position + puntoDisparo.forward * 5f;
        Vector3 direccion = puntoObjetivo - objetoSujeto.position;
        objetoSujeto.velocity = direccion * Time.deltaTime * fuerzaMovimiento;
    }

    void SoltarObjeto()
    {
        if (objetoSujeto != null)
        {
            objetoSujeto.useGravity = true;
            objetoSujeto.drag = 1f;
            objetoSujeto = null;
            Debug.Log("Objeto liberado.");
        }
    }

    void ActivarLinea(bool estado)
    {
        if (linea != null)
        {
            linea.enabled = estado;
        }
    }

    void ActualizarLinea()
    {
        if (linea != null && objetoSujeto != null)
        {
            Vector3 inicio = puntoDisparo.position;
            Vector3 fin = objetoSujeto.position;
            Vector3 direccion = fin - inicio;
            float distancia = direccion.magnitude;

            float distanciaMaxima = 10f;

            if (distancia > distanciaMaxima)
            {
                fin = inicio + direccion.normalized * distanciaMaxima;
            }

            linea.SetPosition(0, inicio);
            linea.SetPosition(1, fin);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoDisparo.position, radioDisparo);
    }

}
