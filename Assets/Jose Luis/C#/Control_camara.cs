using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Camara : MonoBehaviour
{
    [Header("Control de camara")]
    [SerializeField] private Transform Seguimiento;
    [SerializeField] private float Distancia_Objetivo_x;
    [SerializeField] private float Distancia_Objetivo_y;
    [SerializeField] private float Distancia_Objetivo_z;
    [SerializeField] private float Velocidad_Camara;
    [SerializeField] private bool Suavizado = true;
    private Vector3 Nueva_Posicion;
    private static Control_Camara instancia;
    public void Start()
    {

        if (instancia != null && instancia != this)
        {
            Destroy(gameObject); // Destruye duplicados
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject); // Haz que esta cámara persista

        if (Seguimiento == null)
        {
            GameObject jugador = GameObject.Find("Player"); // Cambia esto si el jugador tiene otro nombre
            if (jugador != null)
            {
                Seguimiento = jugador.transform;
            }
        }

        if (Seguimiento == null)
        {
            GameObject jugador = GameObject.Find("Player");
            if (jugador != null)
            {
                Seguimiento = jugador.transform;
                Debug.Log("Jugador encontrado y asignado a la cámara.");
            }
            else
            {
                Debug.LogWarning("No se encontró el jugador.");
            }
        }
    }
    void Update()
    {
        Nueva_Posicion = this.transform.position;
        Nueva_Posicion.x = Seguimiento.position.x - Distancia_Objetivo_x;
        Nueva_Posicion.x = Seguimiento.position.x;

        Nueva_Posicion.z = Seguimiento.position.z - Distancia_Objetivo_z;
        Nueva_Posicion.z = Seguimiento.position.z;

        Nueva_Posicion.y = Seguimiento.position.y - Distancia_Objetivo_y;
        Nueva_Posicion.y = Seguimiento.position.y;
        
        Nueva_Posicion.x = Seguimiento.position.x - (-Distancia_Objetivo_y - Distancia_Objetivo_z);
        Nueva_Posicion.y = Seguimiento.position.y - (-Distancia_Objetivo_x - Distancia_Objetivo_z);
        Nueva_Posicion.z = Seguimiento.position.z - (-Distancia_Objetivo_x - Distancia_Objetivo_y);


        if (Suavizado)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, Nueva_Posicion, Velocidad_Camara * Time.deltaTime);
        }
        else
        {
            this.transform.position = Nueva_Posicion;
        }
    }
}
