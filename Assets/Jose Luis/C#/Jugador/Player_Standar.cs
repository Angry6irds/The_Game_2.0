using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Standar : MonoBehaviour
{
    [Header("Movimiento del personaje provicional")]
    [SerializeField] private float velCaminata;
    [SerializeField] private float velEscalera;
    [SerializeField] private float Rotacion;
    [SerializeField] private Animator animador;
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private float velActual;
    [SerializeField] private Rigidbody rb;
    void Start()
    {
        animador = GetComponent<Animator>();
        velActual = velCaminata;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, horizontal * Time.deltaTime * Rotacion, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * velActual);
        Vector3 movimiento = transform.forward * vertical * velActual * Time.deltaTime;
        rb.MovePosition(rb.position + movimiento);
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



