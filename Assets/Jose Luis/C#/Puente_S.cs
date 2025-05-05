using UnityEngine;

public class Puente_S : MonoBehaviour
{
    [Header("Referencias del Jugador")]
    [SerializeField] private Transform jugadorTransform; 
    [SerializeField] private Rigidbody jugadorRigidbody;
    [SerializeField] private CapsuleCollider jugadorCollider;

    [Header("Parámetros de Interacción")]
    [SerializeField] private Vector3 escalaNormal = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 escalaReducida = new Vector3(0.5f, 0.5f, 0.5f); 
    private bool isSmall = false;

    void Start()
    {
       
        if (jugadorTransform == null) Debug.LogError("Falta asignar el Transform del jugador en PlayerInteraction.");
        if (jugadorRigidbody == null) Debug.LogError("Falta asignar el Rigidbody del jugador en PlayerInteraction.");
        if (jugadorCollider == null) Debug.LogError("Falta asignar el CapsuleCollider del jugador en PlayerInteraction.");
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleScale();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleColliderTrigger();
        }
    }

    void ToggleScale()
    {
        if (jugadorTransform != null)
        {
            isSmall = !isSmall; // Alternar estado
            jugadorTransform.localScale = isSmall ? escalaReducida : escalaNormal;
            Debug.Log("Nueva escala del jugador: " + jugadorTransform.localScale);
        }
    }

    void ToggleColliderTrigger()
    {
        if (jugadorCollider != null)
        {
            jugadorCollider.isTrigger = !jugadorCollider.isTrigger;
            Debug.Log("isTrigger ahora está en: " + jugadorCollider.isTrigger);
        }
    }
}
