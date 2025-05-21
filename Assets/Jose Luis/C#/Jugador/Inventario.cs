using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    public bool tieneCristal = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Presionaste E");
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider col in colliders)
            {
                Debug.Log("Encontrado: " + col.name);
                if (col.CompareTag("CristalBlanco"))
                {
                    TieneCristal(col.gameObject);
                    break;
                }
            }
        }
    }

    void TieneCristal(GameObject cristal)
    {
        tieneCristal = true;
        Destroy(cristal); 
        Debug.Log("Cristal recogido.");
    }
}
