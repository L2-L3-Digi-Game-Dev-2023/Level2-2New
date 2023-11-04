using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Tooltip("Maximum distance from the ground.")]
    public float distanceThreshold = 0.05f;
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public bool isCrouch { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    const float OriginOffset = .001f;
    Vector3 RaycastOrigin => transform.position + Vector3.up * OriginOffset;

    new Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        canRun = true;
    }

    void OnCollisionExit(Collision collision)
    {
        canRun = false;
    }

    void FixedUpdate()
    {
        canRun = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);
        //Debug.Log(targetVelocity);
        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        
    }
    
}