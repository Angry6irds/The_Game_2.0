using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Standar : MonoBehaviour
{
    [Header("Movimiento del personaje provicional")]
    [SerializeField] private float velCaminata;
    [SerializeField] private float Rotacion;
    [SerializeField] private Animator animador;
    [SerializeField] private float horizontal, vertical;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float fuerzaDeSalto;
    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        transform.Rotate(0, horizontal * Time.deltaTime * Rotacion, 0);
        transform.Translate(0, 0, vertical * Time.deltaTime * velCaminata);
    }
}
