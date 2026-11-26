using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour{

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
	public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnBindingRebind;



    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Right,
        Move_Left,
        Interact,
        InteractAlternate,
        Pause,
    }

    private PlayerInput_Actions playerInput_Actions;

    private void Awake(){

        Instance = this;

        playerInput_Actions = new PlayerInput_Actions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInput_Actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInput_Actions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInput_Actions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Right:
                return playerInput_Actions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Left:
                return playerInput_Actions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInput_Actions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInput_Actions.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInput_Actions.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInput_Actions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInput_Actions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInput_Actions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Right:
                inputAction = playerInput_Actions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Move_Left:
                inputAction = playerInput_Actions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Interact:
                inputAction = playerInput_Actions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInput_Actions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInput_Actions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
            {
                callback.Dispose();
                playerInput_Actions.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInput_Actions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            }).Start();
    }
}
