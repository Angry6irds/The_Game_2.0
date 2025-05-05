using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encogimientio : MonoBehaviour
{
    [Header("Cambio de tamaño")]
    [SerializeField] private Transform jugador;
    [SerializeField] private Vector3 escalaNormal = new Vector3 (1f, 1f, 1f);
    [SerializeField] private Vector3 escalaReducida = new Vector3 (0.5f, 0.5f, 0.5f); 
    private bool estaEncogido = false;

    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false;

    [Header("Zonas restringidas")]
    [SerializeField] private LayerMask zonaPequena; // Capas de zonas pequeñas

    [Header("Rango adquirido")]
    [SerializeField] private float radioBusqueda = 2f;
    private bool recogido = false;

    void Update()
    {
            if (!objetoRecogido && Input.GetKeyDown(teclaRecojer))
            {
                Recoger();
            }

            if (Input.GetKeyDown(KeyCode.R) && objetoRecogido)
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
                PruebaSaltar scriptSalto = colision.GetComponent<PruebaSaltar>();
                if (scriptSalto != null)
                {
                    objetoRecogido = true;
                    scriptSalto.ActivarDobleSalto();

                    // Oculta el objeto visual
                    MeshRenderer m = GetComponent<MeshRenderer>();
                    if (m != null) m.enabled = false;

                    Collider c = GetComponent<Collider>();
                    if (c != null) c.enabled = false;

                    Debug.Log("Cristal recogido y doble salto activado.");
                    return;
                }
            }
        }
        Debug.Log("No hay jugador cerca para recoger el objeto.");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
   
    void CambiarTamaño()
    {
        if (!estaEncogido)
        {
            
            jugador.localScale = escalaReducida;
            estaEncogido = true;
            Debug.Log("El jugador se ha encogido.");
        }
        else
        {
            
            if (!EstaEnZonaPequena()) // Solo agrandar si no está en una zona restringida
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

    bool EstaEnZonaPequena()
    {
        
        Collider[] colisiones = Physics.OverlapSphere(jugador.position, 0.6f, zonaPequena);
        return colisiones.Length > 0;
    }
}
