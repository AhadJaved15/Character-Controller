using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 45f;
    public Animator animController;
    private CharacterController controller;

    private FloatingJoystick floatingJoystick;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        floatingJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
    }

    private void Update()
    {

        float horizontalInput = floatingJoystick.Horizontal;
        float verticalInput = floatingJoystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        controller.SimpleMove(movementDirection * speed);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            if (!animController)
                return;
            
            animController.SetBool("IsRun", true);
            
        }
        else
        {
            if (!animController)
                return;
            
            animController.SetBool("IsRun", false);
        }
    }
}
