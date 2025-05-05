using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravedad : MonoBehaviour
{
    [Header("Cambiar gravedad")]
    [SerializeField] private KeyCode invertir = KeyCode.F;
    private bool gravedadInvertida = false; 

    [Header("Recojer cristal")]
    [SerializeField] private KeyCode teclaRecojer = KeyCode.E;
    private bool objetoRecogido = false; 

    [Header ("rotacion jugador")]
    [SerializeField] private Transform jugador; 

    void Start()
    {
       
        jugador = GameObject.FindGameObjectWithTag("Player").transform;  // Rota jugador
    }

    void Update()
    {
       
        if (Input.GetKeyDown(teclaRecojer) && !objetoRecogido)
        {
            RecogerObjeto(); 
        }

        if (Input.GetKeyDown(invertir) && objetoRecogido)
        {
            StartCoroutine(CambioGravedadConRetraso(1f)); //segundos de la corrutina
        } 
    }

    void RecogerObjeto()
    {
        objetoRecogido = true;
        Debug.Log("Objeto recogido. Ahora puedes cambiar la gravedad.");

       
        MeshRenderer mesh = GetComponent<MeshRenderer>();  // El objeto desaparesera visualmente
        if (mesh != null) mesh.enabled = false;

       
        Collider colision = GetComponent<Collider>(); // Desactivar el Collider para que no vuelva a activarlo
        if (colision != null) colision.enabled = false;
    }

    
    IEnumerator CambioGravedadConRetraso(float segundos)
    {
        Debug.Log("invertir gravedad...3...2...1...");

        yield return new WaitForSeconds(segundos);

        

        if (gravedadInvertida)
        {
            
            jugador.position += Vector3.down;
            Physics.gravity = new Vector3(0, -9.8f, 0); // Gravedad normal hacia abajo
            RotarJugador(0f); // Volver a la rotación normal
            gravedadInvertida = false;
            Debug.Log("Gravedad normal restaurada.");
        }
        else
        {
            jugador.position += Vector3.up;
            Physics.gravity = new Vector3(0, 9.8f, 0); // Gravedad invertida hacia arriba
            RotarJugador(180f); // Rotar para que los pies estén arriba
            gravedadInvertida = true;
            Debug.Log("Gravedad invertida.");
        }
    }

    void RotarJugador(float rotacionX = 180f)
    {
        if (jugador != null)
        {
            
            jugador.rotation = Quaternion.Euler(rotacionX, 0f, 0f); // Rotar el jugador para que los pies estén arriba o abajo
        }
        else
        {
            Debug.LogWarning("No se encontro el jugador. ve si tiene el tag 'Player'.");
        }
    }
}
