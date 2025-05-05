using UnityEngine;
using System.Collections;

public class DoubleJumpPowerUp : Agarrar_Objetos
{
    public override void Activate(GameObject player)
    {
        PlayerMovement2D pm = player.GetComponent<PlayerMovement2D>();
        if (pm != null)
        {
            Debug.Log("Activando doble salto para el jugador.");
            StartCoroutine(DoubleJumpRoutine(pm));
        }
        else
        {
            Debug.Log("No se encontró el componente PlayerMovement2D en el jugador.");
        }
    }

    private IEnumerator DoubleJumpRoutine(PlayerMovement2D pm)
    {
        pm.doubleJumpActive = true;
        yield return new WaitForSeconds(duration);
        pm.doubleJumpActive = false;
    }
}