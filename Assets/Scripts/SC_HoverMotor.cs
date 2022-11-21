using UnityEngine;
using System.Collections;

public class SC_HoverMotor : MonoBehaviour
{

    public float speed = 90f;
    public float strafeSpeed = 45f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;
    private float powerInput;
    private float turnInput;
    private CharacterController _characterController;
    private Rigidbody carRigidbody;
    Vector2 rotation = Vector2.zero;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public Transform _cam;
    private Vector3 moveDirection = Vector3.zero;
    private bool _StrafingMode = false;


    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        carRigidbody = GetComponent<Rigidbody>();
        rotation.y = transform.eulerAngles.y;
    }
    
    void Update()
    {
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Player and Camera rotation

        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        _cam.transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, 0));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_StrafingMode)
            {
                _StrafingMode = true;
                moveDirection = Vector3.zero;
            }
            else
                _StrafingMode = false;
        }
        if(_StrafingMode)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = strafeSpeed * Input.GetAxis("Vertical");
            float curSpeedY = strafeSpeed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            //_characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        if (!_StrafingMode)
        {
            carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
            carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        }
    }
}
