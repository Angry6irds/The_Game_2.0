using UnityEngine;

public class PruebaSaltar : MonoBehaviour
{
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private Rigidbody rb;

    private int saltosDisponibles = 1;

    [Header("Habilidad desbloqueable")]
    public bool dobleSaltoHabilitado = false;
    private Animator animador;

    void Start()
    {
        animador = GetComponent<Animator>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && saltosDisponibles > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            saltosDisponibles--;

            if (animador != null)
            {
                animador.SetTrigger("saltar");
            }
        }
    }
      void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Escalera"))
        {
            saltosDisponibles = dobleSaltoHabilitado ? 2 : 1;
        }
    }
     
    public void ActivarDobleSalto()
    {
        dobleSaltoHabilitado = true;
        Debug.Log("¡Doble salto desbloqueado!");
        saltosDisponibles = 2;
    }
}
