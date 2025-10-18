using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 leftDirection = Vector3.left;
    public Vector3 rightDirection = Vector3.right;
    public Vector3 upDirection = Vector3.up;
    public Vector3 downDirection = Vector3.down;
    public float speed = 5f;

    [Header("Aiming")]
    public bool enableAiming = true;
    [Tooltip("Rotate the transform to face the mouse. If false and flipSpriteBasedOnAim is true, will flip the SpriteRenderer instead.")]
    public bool useRotation = true;
    [Tooltip("When not rotating, flipping the sprite horizontally based on mouse X position.")]
    public bool flipSpriteBasedOnAim = false;
    [Tooltip("Smoothness of the aim rotation (0 = instant).")]
    public float aimSmoothing = 0f;
    [Tooltip("Additional Z rotation offset in degrees.")]
    public float rotationOffset = 0f;

    [Tooltip("Optional: a child transform (visual model) to rotate instead of the root GameObject. If null, this script rotates the GameObject itself.")]
    public Transform modelToRotate;

    SpriteRenderer spriteRenderer;
    Camera mainCamera;
    Quaternion targetRotation;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        HandleMovement();

        if (enableAiming)
            HandleAiming();
    }

    void HandleMovement()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Transform>().position += leftDirection;
        }
            
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Transform>().position += rightDirection;
        }
            
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Transform>().position += upDirection;
        }
            
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Transform>().position += downDirection;
        }
            

        if (move != Vector3.zero)
            transform.Translate(move.normalized * speed * Time.deltaTime, Space.World);
    }

    void HandleAiming()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            return;

        Vector3 mouseScreen = Input.mousePosition;
        // Convert to world position. Use a Z distance from camera to object so ScreenToWorldPoint works for perspective too.
        float zDist = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreen.x, mouseScreen.y, zDist));

        Vector3 dir = mouseWorld - transform.position;
        dir.z = 0f;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        if (useRotation)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
            targetRotation = Quaternion.Euler(0f, 0f, angle);

            // Apply rotation to the assigned visual model if provided, otherwise rotate this transform
            if (modelToRotate != null)
            {
                if (aimSmoothing > 0f)
                    modelToRotate.rotation = Quaternion.Lerp(modelToRotate.rotation, targetRotation, Time.deltaTime * aimSmoothing);
                else
                    modelToRotate.rotation = targetRotation;
            }
            else
            {
                if (aimSmoothing > 0f)
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * aimSmoothing);
                else
                    transform.rotation = targetRotation;
            }
        }
        else if (flipSpriteBasedOnAim && spriteRenderer != null)
        {
            spriteRenderer.flipX = (dir.x < 0f);
        }
    }
}
