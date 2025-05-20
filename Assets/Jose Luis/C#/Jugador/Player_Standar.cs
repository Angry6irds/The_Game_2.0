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
    void Start()
    {
        animador = GetComponent<Animator>();
        velActual = velCaminata;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!puedeMoverse) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, horizontal * Time.deltaTime * Rotacion, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * velActual);
        Vector3 movimiento = transform.forward * vertical * velActual * Time.deltaTime;
        //rb.MovePosition(rb.position + movimiento);

       if (controller.isGrounded)
        {
            velocidad.y = -gravedad * Time.deltaTime;
        }
        else
        {
            velocidad.y -= gravedad * Time.deltaTime;
        }

        controller.Move((movimiento + velocidad) * Time.deltaTime);

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