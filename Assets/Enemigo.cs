using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    [Header("Comportamiento")]
    [SerializeField] private int rutina;
    [SerializeField] private float tiempoEspera;
    [SerializeField] private Quaternion angulo;
    [SerializeField] private float grado;
    [SerializeField] private Vector3 posicionInicial;

    [Header("Deteccion y Ataque")]
    [SerializeField] private GameObject target;
    [SerializeField] private float rangoVision = 10f;
    [SerializeField] private float rangoPersecucion = 5f;
    [SerializeField] private float distanciaMinima = 1.2f;
    [SerializeField] private float tiempoEntreAtaques = 2f;

    [Header("Velocidades")]
    [SerializeField] private float velocidadCaminata = 1f;
    [SerializeField] private float velocidadCarrera = 2f;

    [Header("Componentes")]
    [SerializeField] private NavMeshAgent agente;

    private bool puedeAtacar = true;

    void Start()
    {
        target = GameObject.Find("Player_Standar");
        posicionInicial = transform.position;
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidadCaminata;
        agente.angularSpeed = 720f;
    }

    void Update()
    {
        if (target != null)
        {
            ComportamientoEnemigo();
        }
    }

    private void ComportamientoEnemigo()
    {
        if (target.CompareTag("Invisible")) return;
        float distanciaJugador = Vector3.Distance(transform.position, target.transform.position);
        float distanciaInicio = Vector3.Distance(transform.position, posicionInicial);

        if (distanciaJugador <= rangoPersecucion)
        {
            if (distanciaJugador > distanciaMinima)
            {
                // Perseguir jugador
                agente.speed = velocidadCarrera;
                agente.SetDestination(target.transform.position);
            }
            else
            {
                // Detenerse y atacar
                agente.ResetPath();
                Vector3 direccion = (target.transform.position - transform.position).normalized;
                Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionDeseada, 720 * Time.deltaTime);

                if (puedeAtacar)
                {
                    StartCoroutine(AtaqueJugador());
                }
            }
        }
        else if (distanciaJugador > rangoVision && distanciaInicio > rangoPersecucion)
        {
            // Regresar a base
            agente.speed = velocidadCaminata;
            agente.SetDestination(posicionInicial);
        }
        else
        {
            // Patrullaje
            MovimientoAleatorio();
        }
    }

    private void MovimientoAleatorio()
    {
        tiempoEspera += Time.deltaTime;
        if (tiempoEspera >= 4f)
        {
            rutina = Random.Range(0, 2);
            tiempoEspera = 0f;
        }

        if (rutina == 1)
        {
            Vector3 destino = transform.position + (transform.forward * 3f);
            agente.speed = velocidadCaminata;
            agente.SetDestination(destino);
        }
    }

    private IEnumerator AtaqueJugador()
    {
        puedeAtacar = false;
        Debug.Log("¡El enemigo ataca al jugador!");
        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    // Visualiza los rangos en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision); // Rango de vision

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoPersecucion); // Rango de persecucion

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima); // Rango de ataque
    }
}