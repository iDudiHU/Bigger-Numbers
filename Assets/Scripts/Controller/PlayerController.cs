using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Default")]
    [Tooltip("The speed at witch the player moves")]
    [SerializeField]
    private float moveSpeedDef = 20f;
    [Tooltip("The speed at witch the player rotates (calculated in degrees)")]
    [SerializeField]
    private float lookSpeedDef = 60f;
    [Tooltip("The power at witch the player jumps")]
    [SerializeField]
    private float jumpPowerDef = 8f;
    [Tooltip("The strength of gravity")]
    [SerializeField]
    private float gravityDef = 9.81f;

    [Header("Settings")]
    [Tooltip("The speed at witch the player moves")]
    [SerializeField]
    private float moveSpeed;
    [Tooltip("The speed at witch the player rotates (calculated in degrees)")]
    [SerializeField]
    private float lookSpeed;
    [Tooltip("The power at witch the player jumps")]
    [SerializeField]
    private float jumpPower;
    [Tooltip("The strength of gravity")]
    [SerializeField]
    private float gravity;

    [Header("Jump Timing")]
    public float jumpTimeLeniency = 0.1f;
    float timeToStopBeingLenient = 0f;
    [Header("Required References")]
    [Tooltip("The player shooter script that fires projectiles")]
    public Shooter playerShooter;
    bool doubleJumpAvailable = false;
    // The character controller component player 
    private CharacterController controller;
    public Health playerHealth;
    public List<GameObject> disableWhileDead;
    private InputManager inputManager;


	/// <summary>
	/// Description:
	/// Standard Unity function called once before the first Update call
	/// Input:
	/// none
	/// Return:
	/// void (no return)
	/// </summary>
	/// 

	private void Awake()
	{
        OnRespawn();
	}
	void Start()
    {
        SetUpCharacterController();
        SetUpInputManager();
    }

    //private is a default therefore no need to write it
    void SetUpCharacterController()
	{
        controller = GetComponent<CharacterController>();
        if (controller == null)
		{
			Debug.LogError("The player controller script does not have a character controller on the same game object!");
		}
	}
    void SetUpInputManager()
    {
        inputManager = InputManager.instance;
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a input manager!");
        }
    }
    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    Vector3 moveDirection;
    void ProcessMovement()
	{
        //Get the input from the input manager
        float leftRightInput = inputManager.horizontalMoveAxis;
        float forwardBackwardInput = inputManager.verticalMoveAxis;
        bool jumpPressed = inputManager.jumpPressed;

		//handle control of the players while on ground
		if (controller.isGrounded)
		{
            doubleJumpAvailable = true;
            timeToStopBeingLenient = Time.time + jumpTimeLeniency;
            //Set the movement direction to be recieved input, set y to 0 since we are on the ground
            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);
            // Set the move direction in relation to the transform
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * moveSpeed;

			if (jumpPressed)
			{
                moveDirection.y = jumpPower;
			}
		}
		else
		{
            moveDirection = new Vector3(leftRightInput * moveSpeed, moveDirection.y, forwardBackwardInput * moveSpeed);
            moveDirection = transform.TransformDirection(moveDirection);

			if (jumpPressed && Time.time < timeToStopBeingLenient)
			{
                moveDirection.y = jumpPower;
            }
			else if (jumpPressed && doubleJumpAvailable)
			{
                moveDirection.y = jumpPower;
                doubleJumpAvailable = false;
			}

		}

        moveDirection.y -= gravity * Time.deltaTime;

		if (controller.isGrounded && moveDirection.y < 0)
		{
            moveDirection.y = -0.3f;
		}

        controller.Move(moveDirection * Time.deltaTime);

	}

    void ProcessRotation()
	{
        float horizontalLookInput = inputManager.horizontalLookAxis;
        Vector3 playerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(playerRotation.x, playerRotation.y + horizontalLookInput * lookSpeed * Time.deltaTime, playerRotation.z));
	}

    public void OnDeath()
	{
        if (playerHealth.currentHealth <= 0)
        {
            foreach (GameObject ingameObject in disableWhileDead)
                {
                    ingameObject.SetActive(false);
                }
            return;
        }
    }

    void OnRespawn()
	{
        SetUpPlayer();
        foreach (GameObject ingameObject in disableWhileDead)
            {
                ingameObject.SetActive(true);
            }
            return;
        
    }

    public void SetUpPlayer()
	{
        moveSpeed = moveSpeedDef;
        lookSpeed = lookSpeedDef;
        jumpPower = jumpPowerDef;
        gravity = gravityDef;
	}
    public void AddSpeed()
	{
        moveSpeed += 1f;
	}

    public void AddJumpHeight()
	{
        jumpPower += 1f;
	}

}
