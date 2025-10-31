using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Death = Animator.StringToHash("Death");
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineInputAxisController cinnemachineInput;

    [Header("Movement")] 
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpForce;

    [Header("Ground Check")] 
    [SerializeField] private Transform feet;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask isGround;
    
    private Vector3 verticalMove;
    private CharacterController ch_Controller;
    private Camera camera;
    private bool isGrounded;
    private Vector2 movementInput;
    
    void Start()
    {
        ch_Controller = GetComponent<CharacterController>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; //Bloquea el cursor
    }

    private void OnEnable()
    {
        InputController.Instance.OnJump += Jump;
        InputController.Instance.OnMove += UpdateMovement;
    }
    private void UpdateMovement(Vector2 inputVector)
    {
        movementInput = inputVector;
    }
    
    void Update()
    {
        GroundCheck();
        ApplyGravity();
        MoveAndRotate();
    }
    
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(feet.position, detectionRadius, isGround);
    }
    
    private void ApplyGravity()
    {
        if (isGrounded && verticalMove.y < 0)
        {
            verticalMove.y = -1;
        }
        else
        {
            verticalMove.y += gravity * Time.deltaTime;
        }
        
        ch_Controller.Move(verticalMove * Time.deltaTime);
    }
    
    private void MoveAndRotate()
    {
        //El cuerpo se rota a la vez que la cámara (se aplica la rotación de ella al jugador)
        transform.rotation = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0);

        if (movementInput.sqrMagnitude > 0)
        {
            //Cálculo del ángulo base a los inputs
            float angleToRotate = Mathf.Atan2(movementInput.x,movementInput.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            
            //Se rota el vector a dicho ángulo para que mire a donde apunte la cámara.
            Vector3 movementVector = Quaternion.Euler(0, angleToRotate, 0) * Vector3.forward;
            
            ch_Controller.Move(movementVector * movementSpeed * Time.deltaTime); //Aplica el movimiento.
        }
        
        animator.SetFloat(Speed, ch_Controller.velocity.magnitude);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            verticalMove.y = Mathf.Sqrt(-2 * jumpForce * gravity);
        }
    }

    public void OnDeath()
    {
        animator.SetTrigger(Death);
        this.enabled = false;
        cinnemachineInput.enabled = false;
    }

    private void OnDisable()
    {
        InputController.Instance.OnJump -=  Jump;
        InputController.Instance.OnMove -=  UpdateMovement;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, detectionRadius);
    }
}
