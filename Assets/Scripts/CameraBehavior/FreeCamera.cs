using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] Transform target;

    [Header("Zoom Settings")]
    [SerializeField] float distance = 10f;
    [SerializeField] float zoomedDistance = 4f;
    [SerializeField] float normalFOV = 60f;
    [SerializeField] float zoomedFOV = 40f;

    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float minPitch = -45f;
    [SerializeField] float maxPitch = 45f;

    [Header("Follow Smoothing")]
    [SerializeField] float followSpeed = 5f;

    private float yaw;
    private float pitch;
    private bool isZoomedIn = false;
    private Vector3 currentVelocity;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            enabled = false;
            return;
        }

        if (target == null)
        {
            enabled = false;
            return;
        }

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        cam.fieldOfView = normalFOV;
    }

    void LateUpdate()
    {
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.K))
        {
            isZoomedIn = !isZoomedIn;
        }

        float currentDistance = isZoomedIn ? zoomedDistance : distance;
        cam.fieldOfView = isZoomedIn ? zoomedFOV : normalFOV;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredOffset = rotation * new Vector3(0f, 0f, -currentDistance);
        Vector3 desiredPosition = target.position + desiredOffset;

        Ray ray = new Ray(target.position, desiredOffset.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, currentDistance))
        {
            desiredPosition = hit.point - desiredOffset.normalized * 0.3f;
        }

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed;
            pitch -= mouseY * rotationSpeed;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }
}
