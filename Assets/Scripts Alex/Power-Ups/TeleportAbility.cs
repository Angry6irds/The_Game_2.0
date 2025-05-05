using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    public float teleportDistance = 2f;
    public int maxTeleports = 2;
    public float cooldownTime = 20f;
    private int teleportCount = 0;
    private bool isOnCooldown = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!isOnCooldown)
            {
                CharacterController controller = GetComponent<CharacterController>();
                Vector3 direction = (controller != null && controller.velocity.magnitude > 0.1f) ? controller.velocity.normalized : transform.forward;
                Debug.Log("Teleporting. Position before: " + transform.position + ", direction: " + direction);
                if (controller != null) controller.enabled = false;
                transform.position += direction * teleportDistance;
                if (controller != null) controller.enabled = true;
                teleportCount++;
                Debug.Log("Teleport activated. Count: " + teleportCount);
                if (teleportCount >= maxTeleports)
                {
                    Debug.Log("Max teleports reached. Starting cooldown.");
                    StartCoroutine(Cooldown());
                }
            }
            else
            {
                Debug.Log("Teleport ability is on cooldown.");
            }
        }
    }

    IEnumerator Cooldown()
    {
        isOnCooldown = true;
        Debug.Log("Cooldown started for " + cooldownTime + " seconds.");
        yield return new WaitForSeconds(cooldownTime);
        teleportCount = 0;
        isOnCooldown = false;
        Debug.Log("Cooldown ended. Teleports reset.");
    }
}