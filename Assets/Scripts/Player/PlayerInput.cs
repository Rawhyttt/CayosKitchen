using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour{

    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;

    public event EventHandler OnPauseAction;

    private PlayerInput_Actions playerInput_Actions;

    private void Awake(){

        Instance = this;

        playerInput_Actions = new PlayerInput_Actions();
        playerInput_Actions.Enable();

        playerInput_Actions.Player.Interact.performed += Interact_Performed;
		playerInput_Actions.Player.InteractAlternate.performed += InteractAlternate_Performed;
        playerInput_Actions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInput_Actions.Player.Interact.performed -= Interact_Performed;
        playerInput_Actions.Player.InteractAlternate.performed -= InteractAlternate_Performed;
        playerInput_Actions.Player.Pause.performed -= Pause_performed;

        playerInput_Actions.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
	
    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInput_Actions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
