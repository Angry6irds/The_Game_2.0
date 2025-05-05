using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCrystalPowerUp : Agarrar_Objetos
{
    public override void Activate(GameObject player)
    {
        if (player.GetComponent<TeleportAbility>() == null)
            player.AddComponent<TeleportAbility>();
    }
}