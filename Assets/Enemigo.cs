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

    // Control para evitar ataques cada frame
    private bool puedeAtacar = true;

    void Start()
    {
        // Buscar al jugador y guardar la posicion inicial
        target = GameObject.Find("Jugador");
        posicionInicial = transform.position;

        // Obtener el agente de navegacion
        agente = GetComponent<NavMeshAgent>();

        // Asignar velocidad inicial
        agente.speed = velocidadCaminata;
    }

    void Update()
    {
        if (target != null)
        {
            ComportamientoEnemigo();
        }
    }

    // Metodo principal que decide que hacer segun la distancia
    private void ComportamientoEnemigo()
    {
        float distanciaJugador = Vector3.Distance(transform.position, target.transform.position);
        float distanciaInicio = Vector3.Distance(transform.position, posicionInicial);

        if (distanciaJugador <= rangoPersecucion && distanciaJugador > distanciaMinima)
        {
            // El jugador esta en rango para perseguirlo
            Debug.Log("Interaccion detectada: persiguiendo jugador");
            agente.speed = velocidadCarrera;
            agente.SetDestination(target.transform.position);
        }
        else if (distanciaJugador <= distanciaMinima)
        {
            // El enemigo esta muy cerca del jugador y puede atacar
            agente.ResetPath(); // detener movimiento
            transform.LookAt(target.transform); // mirar al jugador

            if (puedeAtacar)
            {
                StartCoroutine(AtaqueJugador());
            }
        }
        else if (distanciaJugador > rangoVision && distanciaInicio > rangoPersecucion)
        {
            // El jugador se alejo mucho, regresar a la base
            Debug.Log("Jugador fuera de rango, regresando a la base");
            agente.speed = velocidadCaminata;
            agente.SetDestination(posicionInicial);
        }
        else
        {
            // Patrullaje o movimiento aleatorio
            Debug.Log("Modo patrulla o reposo");
            MovimientoAleatorio();
        }
    }

    // Simula movimiento aleatorio del enemigo
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

    // Corrutina que simula ataque con tiempo de espera
    private IEnumerator AtaqueJugador()
    {
        puedeAtacar = false;
        Debug.Log("¡El enemigo ataca al jugador!");

        // Aqui puedes colocar daño al jugador o animacion

        yield return new WaitForSeconds(tiempoEntreAtaques);

        puedeAtacar = true;
    }
}