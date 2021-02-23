using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName="InputReader", menuName="Input/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IGameMenuActions
{
    // Player
    public event Action<Vector2> movementEvent;
    public event Action attackEvent;
    public event Action jumpEvent;
    public event Action runEvent;
    public event Action pauseEvent;

    // Menu
    public event Action nevigationEvent;
    public event Action pointEvent;
    public event Action submitEvent;
    public event Action cancelEvent;
    public event Action exitMenuEvent;
    

    // OnControlChange
    public event Action controlsChangeEvent;

    // Switch Action Map
    public event Action switchActionMapEvent;

    public GameInput input;

    private void OnEnable() 
    {
        if(input == null)
        {
            input = new GameInput();
            input.Player.SetCallbacks(this);
            input.GameMenu.SetCallbacks(this);
        }

        EnablePlayer();    
    }

    public void EnablePlayer()
    {
        input.Player.Enable();
        input.GameMenu.Disable();
    }

    public void DisablePlayer()
    {
        input.Player.Disable();
        input.GameMenu.Enable();
    }



    // Change Controls
    public void OnControlsChanged()
    {
        controlsChangeEvent?.Invoke();
    }





    // Player
    public void OnWASD(InputAction.CallbackContext context)
    {
        movementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
            attackEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed)
            jumpEvent?.Invoke();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed)
            runEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
            pauseEvent?.Invoke();
    }

    
    


    // Menu
    public void OnNevigation(InputAction.CallbackContext context)
    {
        nevigationEvent?.Invoke();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        pointEvent?.Invoke();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if(context.performed)
            submitEvent?.Invoke();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.performed)
            cancelEvent?.Invoke();
    }

    public void OnExitMenu(InputAction.CallbackContext context)
    {
        if(context.performed)
            exitMenuEvent?.Invoke();
    }
}
