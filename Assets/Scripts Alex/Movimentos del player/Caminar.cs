using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public bool doubleJumpActive = false;
    private bool doubleJumpUsed = false;
    public float teleportDistance = 2f;

    void Update()
    {
        //Debug.Log("isGrounded: " + controller.isGrounded + " | doubleJumpActive: " + doubleJumpActive + " | doubleJumpUsed: " + doubleJumpUsed);
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJumpUsed = false;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z) * speed;
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (!controller.isGrounded && doubleJumpActive && !doubleJumpUsed && Input.GetButtonDown("Jump"))
        {
            //Debug.Log("Double jump triggered");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            doubleJumpUsed = true;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move((move + velocity) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.T))
        {
            CharacterController cc = GetComponent<CharacterController>();
            Vector3 tpDirection = (cc != null && cc.velocity.magnitude > 0.1f) ? cc.velocity.normalized : transform.forward;
            transform.position += tpDirection * teleportDistance;
        }
    }
}