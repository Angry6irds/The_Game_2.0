using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    public bool tieneCristal = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider col in colliders)
            {
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
