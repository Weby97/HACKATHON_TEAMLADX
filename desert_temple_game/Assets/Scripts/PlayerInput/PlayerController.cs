using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Reference to game controls
    private GameControls gameControls;

    //Reference to player settings
    public static PlayerData playerSettings;

    private float speed;

    //Reference to a camera object
    [SerializeField] private GameObject _camera;

    //Rotation values
    private float rotX, rotY;

    //How high the player will jump
    [SerializeField] private float jumpForce = 5f;

    //Distance to the ground from the player
    private float distToGround;

    private GameObject currentWeapon;

    //Component references
    private Rigidbody _rb;
    private Collider _collider;
    private PlayerScript _playerScript;


    private void Awake()
    {
        //Initialize and enable player controls
        gameControls = new GameControls();
        gameControls.Player.Enable();
        _playerScript = GetComponent<PlayerScript>();
        //Subscribe functions
        gameControls.Player.Jump.started += OnJump;
        gameControls.Player.Fire.started += OnFire;
        gameControls.Player.Fire.canceled += OnFire;
        gameControls.Player.SwitchWeapons.performed += (ctx) => _playerScript.SendMessage("SwitchWeapon");
        gameControls.Player.Reload.performed += (ctx) => _playerScript.currentWeapon.SendMessage("Reload");
        playerSettings = Resources.Load<PlayerData>("DefaultPlayerSettings");
        speed = playerSettings.speed;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Set references
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        distToGround = _collider.bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = _playerScript.speed;
        currentWeapon = _playerScript.currentWeapon;

        //The vector that is returned from the move action
        Vector2 moveVector = gameControls.Player.Move.ReadValue<Vector2>();

        //Move the player
        transform.Translate(moveVector.x * speed * Time.fixedDeltaTime, 0, moveVector.y * speed * Time.fixedDeltaTime);

        //Vector returned from the look action
        Vector2 lookVector = gameControls.Player.Look.ReadValue<Vector2>();
        //Increase rotational values by the amount the mouse moved
        rotX += lookVector.x;
        rotY += lookVector.y;

        //Restrict y axis rotation
        rotY = Mathf.Clamp(rotY, -90f, 90f);

        //rotate the camera
        _camera.transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

        //rotate the player along the x axis only
        transform.rotation = Quaternion.Euler(0, rotX, 0);

        // Slow trap
        if (transform.position.x > -17.8 && transform.position.x < -7.2 && transform.position.z > -18.2 && transform.position.z < -7.5)
        {
          Debug.Log("Speed reduced! ");
          speed = 2.5f;
          Debug.Log("Speed");
        }

    }

    /// <summary>
    /// Determines whether the player is grounded or not
    /// </summary>
    /// <returns>A bool that is true if the player is on the ground</returns>
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        //If not on ground don't jump
        if (!IsGrounded())
            return;
        //Add jump force
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    void OnFire(InputAction.CallbackContext ctx)
    {
        currentWeapon.SendMessage("OnFire");
        if (ctx.canceled)
            currentWeapon.SendMessage("StopFire");

    }

}
