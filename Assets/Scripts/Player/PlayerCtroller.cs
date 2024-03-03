using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCtroller : MonoBehaviour
{
    public PlayerInputController mInputController;
    public Vector2 mInputDirection;

    private void Awake()
    {
        mInputController = new PlayerInputController();
    }

    private void OnEnable()
    {
        mInputController.Enable();
    }

    private void OnDisable()
    {
        mInputController.Disable();
    }

    public void Update()
    {
        mInputDirection = mInputController.Gameplay.Move.ReadValue<Vector2>();
    }
}
