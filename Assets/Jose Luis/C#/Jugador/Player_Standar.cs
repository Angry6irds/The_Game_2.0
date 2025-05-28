using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Standar : MonoBehaviour
{
    [Header("Movimiento del personaje provisional")]
    [SerializeField] public float velCaminata;
    [SerializeField] private float velEscalera;
    [SerializeField] private float Rotacion;
    [SerializeField] private Animator animador;
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float gravedad = 9.8f;
    [SerializeField] public float velActual;
    [SerializeField] private Vector3 velocidad;
    [SerializeField] private CharacterController controller;
    //[SerializeField] private Rigidbody rb;
    [Header("Control externo")]
    public bool puedeMoverse = true;
    public float impulsoJetpack = 0f;
    [Header("Salto tradicional")]
    public float fuerzaSalto = 10f;
    private bool saltoSolicitado = false;
    private CamaraMixta camaraControl;
    void Start()
    {
        animador = GetComponent<Animator>();
        velActual = velCaminata;
        controller = GetComponent<CharacterController>();
        camaraControl = FindObjectOfType<CamaraMixta>();
        Debug.Log("CharacterController activo: " + controller.enabled);
    }

    void Update()
    {
        if (!puedeMoverse) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 direccionMovimiento = Vector3.zero;

        bool esPrimeraPersona = camaraControl != null && camaraControl.EnPrimeraPersona;

        if (esPrimeraPersona)
        {
            Vector3 adelante = Camera.main.transform.forward;
            Vector3 derecha = Camera.main.transform.right;

            adelante.y = 0f;
            derecha.y = 0f;

            adelante.Normalize();
            derecha.Normalize();

            direccionMovimiento = (adelante * vertical + derecha * horizontal).normalized;

            // Haz que el personaje mire hacia donde apunta la cámara (opcional)
            Vector3 direccionMirada = direccionMovimiento;
            if (direccionMirada != Vector3.zero)
                transform.forward = direccionMirada;
        }
        else
        {
            // Rotación clásica y movimiento en Z local
            if (horizontal != 0)
                transform.Rotate(0, horizontal * Time.deltaTime * Rotacion, 0);

            direccionMovimiento = transform.forward * vertical;
        }

        Vector3 movimiento = direccionMovimiento * velActual;

        // SALTO
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            saltoSolicitado = true;
        }

        // Gravedad y Jetpack
        if (controller.isGrounded && impulsoJetpack <= 0f)
        {
            velocidad.y = -gravedad * Time.deltaTime;
        }
        else
        {
            velocidad.y += -gravedad * Time.deltaTime;
        }

        if (saltoSolicitado)
        {
            velocidad.y = fuerzaSalto;
            saltoSolicitado = false;
        }

        velocidad.y += impulsoJetpack;
        impulsoJetpack = 0f;

        Vector3 movimientoFinal = new Vector3(movimiento.x, velocidad.y, movimiento.z);
        controller.Move(movimientoFinal * Time.deltaTime);

        // Animación
        float velocidadTotal = new Vector2(horizontal, vertical).magnitude;
        animador.SetFloat("velocidad", velocidadTotal);
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