using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour{

    public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;
    private PlayerInput_Actions playerInput_Actions;

    private void Awake(){
        playerInput_Actions = new PlayerInput_Actions();
        playerInput_Actions.Enable();

        playerInput_Actions.Player.Interact.performed += Interact_Performed;
		playerInput_Actions.Player.InteractAlternate.performed += InteractAlternate_Performed;
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
