using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private GameControls gameControls;

    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject _camera;
    [SerializeField] [Range(0, 1)] private float sensititvity;
    private float rotX, rotY;

    [SerializeField] private float jumpForce = 2f;
    private float distToGround;

    private Rigidbody _rb;
    private Collider _collider;


    private void Awake()
    {
        gameControls = new GameControls();
        gameControls.Player.Enable();
        gameControls.Player.Jump.started += OnJump;
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        distToGround = _collider.bounds.extents.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = gameControls.Player.Move.ReadValue<Vector2>();

        transform.Translate(moveVector.x * speed * Time.fixedDeltaTime, 0, moveVector.y * speed * Time.fixedDeltaTime);

        Vector2 lookVector = gameControls.Player.Look.ReadValue<Vector2>();
        rotX += lookVector.x;
        rotY += lookVector.y;

        rotY = Mathf.Clamp(rotY, -90f, 90f);

        _camera.transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

        transform.rotation = Quaternion.Euler(0, rotX, 0);

    }

    bool IsGrounded() 
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (!IsGrounded())
            return;
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }
}
