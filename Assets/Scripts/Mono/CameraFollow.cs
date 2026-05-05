using UnityEngine;

//This script makes the camera follow the player in this top down 2d view.
public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D targetRb; // Get your player's Rigidbody2D

    [SerializeField] float followSpeed = 5f;
    [SerializeField] float lookAheadDistance = 3f; // How far ahead to look
    [SerializeField] float lookAheadSmooth = 2f;    // How fast the look-ahead shifts

    public Vector3 baseOffset = new Vector3(0, 0, -10);
    private Vector3 currentLookAhead;

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculate how far ahead to look based on velocity
        Vector3 targetVelocity = new Vector3(targetRb.linearVelocity.x, targetRb.linearVelocity.y, 0);
        Vector3 aheadTargetPos = targetVelocity.normalized * lookAheadDistance;

        // 2. Smoothly shift the look-ahead point so it doesn't "snap"
        currentLookAhead = Vector3.Lerp(currentLookAhead, aheadTargetPos, lookAheadSmooth * Time.deltaTime);

        // 3. Final target position = Player + Base Offset + Look-Ahead Offset
        Vector3 desiredPosition = target.position + baseOffset + currentLookAhead;

        // 4. Move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
