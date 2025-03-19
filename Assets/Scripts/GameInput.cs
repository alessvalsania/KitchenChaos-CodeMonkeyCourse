using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        // Create the player input actions
        playerInputActions = new PlayerInputActions();
        // Enable the player input actions
        playerInputActions.Enable();

        // Add the interact event to the interact action
        // += is used to add a new method to the event
        // Because it's a callback we can call the method directly after the +=
        // playerInputActions.Player.Interact.performed += ctx => { Debug.Log("Interacted with the counter"); };
        // Also we can define the method outside and call it's signature or definition as this example 

        playerInputActions.Player.Interact.performed += Interact_performed;

    }

    // this is the method that is going to happen when the interact action is performed
    // it's parameters should look like this one
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // The event can be null if there are no subscribers
        // If its null can throw an exception NullpointerException
        // Check if the event is not null
        /* This is the same as the if statement below
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
        */
        OnInteractAction?.Invoke(this, EventArgs.Empty);

        // We are saynig here basically that in this interaction
        // We are the object that it's calling it
        // And we are not sending any arguments
    }

    public Vector2 GetMovementVectorNormalized()
    {
        // Get the input from the player with the new input system
        Vector2 input = playerInputActions.Player.Move.ReadValue<Vector2>();


        // Old input system
        // Vector2 input = new Vector2(0, 0);

        // if (Input.GetKey(KeyCode.W))
        // {
        //     input.y += 1;
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     input.y -= 1;
        // }
        // if (Input.GetKey(KeyCode.A))
        // {
        //     input.x -= 1;
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     input.x += 1;
        // }

        input = input.normalized;
        return input;
    }
}
