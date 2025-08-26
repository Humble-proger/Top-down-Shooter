using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputController _controller;

    public static InputManager Instance { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public Vector2 Move { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _controller = new PlayerInputController();
    }

    private void OnEnable()
    {
        _controller.Player.Enable();
        _controller.Player.MousePosition.performed += OnChengePosition;
        _controller.Player.MousePosition.canceled += OnChengePosition;
        _controller.Player.Move.performed += OnChengeMove;
        _controller.Player.Move.canceled += OnChengeMove;
    }

    private void OnDisable()
    {
        _controller.Player.Disable();
        _controller.Player.MousePosition.performed -= OnChengePosition;
        _controller.Player.MousePosition.canceled -= OnChengePosition;
        _controller.Player.Move.performed -= OnChengeMove;
        _controller.Player.Move.canceled -= OnChengeMove;
    }

    private void OnChengeMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Move = _controller.Player.Move.ReadValue<Vector2>();
    }

    private void OnChengePosition(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MousePosition = _controller.Player.MousePosition.ReadValue<Vector2>();
    }

    public InputAction GetShootAction() => _controller.Player.Shoot;
}
