using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private NewActions inputActions;

    public event EventHandler OnPlayerJumpStarted;
    public event EventHandler OnPunchStarted;
    public event EventHandler OnInteractStarted;
    public event EventHandler OnJurnalStarded;
    public event Action <Vector2> OnArrowTouth;
    public event Action OnEscStarted;


    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
        Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);

        inputActions = new NewActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Jump.started += JumpPressed;
        inputActions.Player.Combat.started += PunchStarted;
        inputActions.Player.Interact.started += InteractStarted;
        inputActions.Inperface.Jurnal.started += JornalStarted;
        inputActions.MiniGames.Arrows.started += StartArrowTouth;
        inputActions.Inperface.Esc.started += EscStarted;
    }
    private void Update()
    {
        GetMousePosition();
    }

    public Vector2 GetMousePosition()
    {
        if (Mouse.current == null) return Vector2.zero;

        return Mouse.current.position.ReadValue();
    }

    public void StartArrowTouth(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        OnArrowTouth?.Invoke(dir);
    }

    public void JornalStarted(InputAction.CallbackContext context)
    {
        OnJurnalStarded?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public void JumpPressed(InputAction.CallbackContext obj)
    {
        OnPlayerJumpStarted?.Invoke(this, EventArgs.Empty);
    }

    public void LostControl()
    {
        inputActions.Player.Move.Disable();
    }

    public void ReturnControl()
    {
        inputActions.Player.Move.Enable();
    }
    private void EscStarted(InputAction.CallbackContext context)
    {
        OnEscStarted?.Invoke();
    }

    private void InteractStarted(InputAction.CallbackContext context)
    {
        OnInteractStarted?.Invoke(this, EventArgs.Empty);
    }

    private void PunchStarted (InputAction.CallbackContext context)
    {
        if (PlayerAnimation.Instance.isPunced == false)
        {
            OnPunchStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}