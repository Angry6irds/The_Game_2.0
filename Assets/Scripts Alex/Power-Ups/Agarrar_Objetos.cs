using UnityEngine;
using System.Collections;

public abstract class Agarrar_Objetos : MonoBehaviour
{
    public float duration = 5f;
    protected GameObject playerInRange;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = other.gameObject;
            Debug.Log("Player entró al trigger del power-up.");
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
            Debug.Log("Player salió del trigger del power-up.");
        }
    }

    protected virtual void Update()
    {
        if (playerInRange != null)
        {
            Debug.Log("Player en rango: presiona Q para recoger el power-up.");
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Tecla Q presionada, activando power-up.");
                Activate(playerInRange);
                Destroy(gameObject);
            }
        }
    }

    public abstract void Activate(GameObject player);
}
