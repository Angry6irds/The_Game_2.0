using UnityEngine;

public class Encogimientio : MonoBehaviour
{
    [SerializeField] private Transform Jugador; 
    [SerializeField] private Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private bool isPickedUp = false; 

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && !isPickedUp)
        {
            PickUp();
        }
    }
    void PickUp()
    {

        Jugador.localScale = newScale;
        isPickedUp = true; 
        Debug.Log("Objeto recogido. Nueva escala del jugador: " + Jugador.localScale);
    }
}
