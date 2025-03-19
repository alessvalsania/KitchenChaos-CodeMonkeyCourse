using System;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Applying the Singleton pattern to the Player class
    public static Player Instance { get; private set; }
    // When use awake? and when use start?
    // Awake it's called in the initialization of the actual object
    // Start it's called for any other external object that is going to use this object
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There are multiple instances of Player");
        }
    }

    // Event to notify when the selected counter has changed
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    // Following the standards of C# we create a class for the args of the event
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    // Layer mask to check if the player can interact with the counter
    [SerializeField] private LayerMask counterLayerMask;

    private bool isWalking = false;
    private Vector3 lastMoveDirection;
    private ClearCounter selectedCounter;
    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction;
    }

    private void OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }


    }
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        // Get the input from the player with the new input system
        Vector2 input = gameInput.GetMovementVectorNormalized();

        // transform the 2D input to 3D input
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);

        // save the last move direction for better interaction
        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }

        // Get the player's current position
        Vector3 playerPosition = transform.position;

        // Define the distance that the player can interact with objects
        // Having it outside it's a good practice to change it easily
        float interactionDistance = 2f;

        // Check if the player can interact with an object
        // In this case, we are checking if the player can interact with a counter, using layerMask
        // There is also another way Using tags, but it's not recommended
        // Also there is a way using RaycastAll to get all the objects that the ray hits, it returns an array of RaycastHits
        if (Physics.Raycast(playerPosition, lastMoveDirection, out RaycastHit raycastHit, interactionDistance, counterLayerMask))
        {
            // Check if the object has the ClearCounter component
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Has ClearCounter
                SetSelectedCounter(clearCounter);

            }
            else
            {
                // Doesn't have ClearCounter
                SetSelectedCounter(null);
            }

        }
        else
        {
            // Doesn't hit anything
            SetSelectedCounter(null);
        }

    }

    private void SetSelectedCounter(ClearCounter newSelectedCounter)
    {
        // We are calling to all the subscribers that the selected Counter has changed
        // and also telling them which is the new selected counter
        if (selectedCounter != newSelectedCounter)
        {
            selectedCounter = newSelectedCounter;

            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
        }


    }

    private void HandleMovement()
    {
        // Get the input from the player with the new input system
        Vector2 input = gameInput.GetMovementVectorNormalized();

        // transform the 2D input to 3D input
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        if (moveDirection != Vector3.zero)
        {
            lastMoveDirection = moveDirection;
        }

        // Move speed defined in the serialized fields
        // also multiplied by Time.deltaTime to make the movement frame rate independent
        float moveDistance = moveSpeed * Time.deltaTime;
        // Get the player's capsule collider radius and height (By testing in the editor)
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        // Get the player's current position
        Vector3 playerPosition = transform.position;

        // Check if the player can move in the desired direction
        //Using capsule cast that interpretates that the player is a capsule
        bool canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        // Check if the player is walking
        isWalking = moveDirection != Vector3.zero;

        if (!canMove)
        {
            // cannot move towards the desired direction

            // Attempt only to move in the x direction
            Vector3 xDirection = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, xDirection, moveDistance);

            if (canMove)
            {
                // Can move in the x direction
                moveDirection = xDirection;

            }
            else
            {
                // Can't move in the x direction
                // Attempt only to move in the z direction
                Vector3 zDirection = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius, zDirection, moveDistance);

                if (canMove)
                {
                    // Can move in the z direction
                    moveDirection = zDirection;
                }
                else
                {
                    // Can't move in the z direction
                    // Can't move in any direction
                }
            }
        }

        // Move the player if he can
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        // Rotate the player to the desired direction 
        transform.forward = Vector3.Slerp(transform.forward, lastMoveDirection, rotateSpeed * Time.deltaTime);
    }


    // Used in Animator to check if the player is walking
    public bool IsWalking()
    {
        return isWalking;
    }


}
