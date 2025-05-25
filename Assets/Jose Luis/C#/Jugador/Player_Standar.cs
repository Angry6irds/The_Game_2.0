using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Standar : MonoBehaviour
{
    [Header("Movimiento del personaje provisional")]
    [SerializeField] private float velCaminata;
    [SerializeField] private float velEscalera;
    [SerializeField] private float Rotacion;
    [SerializeField] private Animator animador;
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float gravedad = 9.8f;
    [SerializeField] private float velActual;
    [SerializeField] private Vector3 velocidad;
    [SerializeField] private CharacterController controller;
    //[SerializeField] private Rigidbody rb;
    [Header("Control externo")]
    public bool puedeMoverse = true;
    public float impulsoJetpack = 0f;
    [Header("Salto tradicional")]
    public float fuerzaSalto = 10f;
    private bool saltoSolicitado = false;
    void Start()
    {
        animador = GetComponent<Animator>();
        velActual = velCaminata;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!puedeMoverse) return;

        // Entrada de movimiento
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Rotación y desplazamiento
        transform.Rotate(0, horizontal * Time.deltaTime * Rotacion, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * velActual);

        Vector3 movimiento = transform.forward * vertical * velActual;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            saltoSolicitado = true;
        }

        // Gravedad y Jetpack
        if (controller.isGrounded && impulsoJetpack <= 0f)
        {
            velocidad.y = -gravedad * Time.deltaTime; // Fijar al suelo
        }
        else
        {
            velocidad.y += -gravedad * Time.deltaTime; // Caída o empuje adicional
        }

        if (saltoSolicitado)
        {
            velocidad.y = fuerzaSalto;
            saltoSolicitado = false;
        }

        // Impulso del Jetpack
        velocidad.y += impulsoJetpack;
        impulsoJetpack = 0f;

        // Movimiento total
        Vector3 movimientoFinal = (movimiento + velocidad) * Time.deltaTime;
        controller.Move(movimientoFinal);

        // Animación
        float velocidadTotal = new Vector2(horizontal, vertical).magnitude;
        animador.SetFloat("velocidad", velocidadTotal);

        Debug.Log("Velocidad para animación: " + velocidadTotal);
    }



    //estas 2 variables son para conectar con el script Escaleras
    public void SetVelocidadTemporal(float nuevaVel)
    {
        velActual = nuevaVel;
    }

    public void RestaurarVelocidad()
    {
        velActual = velCaminata;
    }

}