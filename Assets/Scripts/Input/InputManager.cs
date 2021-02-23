using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [SerializeField] InputReader inputReader;
    private PlayerInput playerInput;

    private void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
    }


    private void OnControlsChanged(PlayerInput input)
    {
        inputReader.OnControlsChanged();
    }
}
