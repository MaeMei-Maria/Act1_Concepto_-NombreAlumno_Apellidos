using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }
    public event Action OnJump;
    public event Action<Vector2> OnMove;
    public event Action<float> OnScroll;
    public event Action<int> OnNewWeapon;
    
    private PlayerInput playerInput;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerInput = GetComponent<PlayerInput>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        playerInput.actions["Jump"].started += OnJumpStarted; //Parecido al GetKeyDown
        playerInput.actions["Move"].performed += OnMoveUpdated; //Solo se ejecuta si hay cambios de valor
        playerInput.actions["Move"].canceled += OnMoveCanceled; //Parecido al GetKeyUp
        playerInput.actions["ChangeWeapon"].performed += OnScrollWeapon;
        playerInput.actions["Weapon1"].started += OnWeapon1;
        playerInput.actions["Weapon2"].started += OnWeapon2;
    }

    private void OnWeapon1(InputAction.CallbackContext ctx)
    {
        OnNewWeapon?.Invoke(0);
    }
    
    private void OnWeapon2(InputAction.CallbackContext ctx)
    {
        OnNewWeapon?.Invoke(1);
    }

    private void OnScrollWeapon (InputAction.CallbackContext ctx)
    {
        OnScroll?.Invoke(ctx.ReadValue<float>()); //Devuelve el sentido en el que se hace scroll (1/ -1)
    }

    private void OnMoveUpdated(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }
    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        OnMove?.Invoke(Vector2.zero);
    }
    
    private void OnJumpStarted(InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    private void OnDisable()
    {
        playerInput.actions["Jump"].started -= OnJumpStarted;
        playerInput.actions["Move"].performed -= OnMoveUpdated;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;
        playerInput.actions["ChangeWeapon"].performed -= OnScrollWeapon;
        playerInput.actions["Weapon1"].started -= OnWeapon1;
        playerInput.actions["Weapon2"].started -= OnWeapon2;
    }
}