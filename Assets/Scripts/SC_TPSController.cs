using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_TPSController : MonoBehaviour
{
    public float speed = 6f;

    [SerializeField] public float jumpSpeed = 8.0f;
    [SerializeField] public float glidingSpeedReduction = 5;
    [SerializeField] private float _climbSpeed = 1.5f;

    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float sprintMultiplier = 2f;
    private float _crouchDivider = 2f;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canSprint = true;
    [HideInInspector] public bool canClimb = true;

    [SerializeField] private GameObject _glideSphere;
    public Transform _target { set; get; }
    private GameObject _compass;
    [SerializeField]
    private GameObject _initialObjective;

    private SC_Currency _currencyManager;

    private void Awake()
    {
        _target = _initialObjective.transform;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _currencyManager = FindObjectOfType<SC_Currency>();
        rotation.y = transform.eulerAngles.y;
        _compass = transform.GetChild(3).gameObject;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
        if (characterController.isGrounded)
        {
            //Sprint
            if (Input.GetKey(KeyCode.LeftShift) && canMove && canSprint)
            {
                curSpeedX *= sprintMultiplier;

                // drain stamina
                SC_StaminaController.instance.UseStamina(.1f);
            }

            // Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl) && canMove)
            {
                curSpeedX /= _crouchDivider;
                transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            // Jump
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //Gliding + gravity
        if (Input.GetKey(KeyCode.LeftShift) && !characterController.isGrounded && moveDirection.y < 0.1)
        {
            // Glidding visuals
            _glideSphere.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

            //Reduced gravity
            moveDirection = (forward * curSpeedX);
            moveDirection.y -= gravity / glidingSpeedReduction * Time.deltaTime;
        }
        else
        {
            // Apply base gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // No gliding visual
            _glideSphere.transform.localScale = Vector3.zero;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);

        if (Input.GetKey(KeyCode.C) && !_compass.activeSelf)
        {
            _compass.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.C) && _compass.activeSelf)
        {
            _compass.SetActive(false);
        }
    }
    public void ObjectiveSet(Transform NewTarget)
    {
        _target = NewTarget;
    }
    public Transform GetObjective()
    {
        return _target;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Climbing
        if (Input.GetKey(KeyCode.W) && hit.gameObject.tag == "Climbable" && canClimb)  //  && !characterController.isGrounded
        {
            moveDirection.y = _climbSpeed;

            // drain stamina only if climbing (and being in the air)
            if (!characterController.isGrounded)
                SC_StaminaController.instance.UseStamina(.1f);
        }

        //PickUpPot
        if (hit.gameObject.tag == "Pot")
        {
            Destroy(hit.gameObject);
            _currencyManager.AddMoney(50);
        }
        //PickUpCollectible
        if (hit.gameObject.tag == "Collectible")
        {
            Destroy(hit.gameObject);
            _currencyManager.AddCollectible(1);
        }
    }
}
