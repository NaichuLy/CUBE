using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMechanic : MonoBehaviour
{
    [Header("Angulo de Vision")]
    [SerializeField] float minYaw = 90f;
    [SerializeField] float maxYaw = 120f;
    [SerializeField] float minPitch = -10f;
    [SerializeField] float maxPitch = 10f;

    [Header("Camera Reference")]
    [SerializeField] Transform cameraTransform;

    private Renderer _objectRenderer;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        _objectRenderer = GetComponent<Renderer>();

        if (_objectRenderer == null)
        {
            Debug.LogError("AngleBasedVisibility: No Renderer found on this object.");
        }
    }

    void Update()
    {
        Vector3 euler = cameraTransform.rotation.eulerAngles;
        float yaw = NormalizeAngle(euler.y);
        float pitch = NormalizeAngle(euler.x);

        bool inYawRange = yaw >= minYaw && yaw <= maxYaw;
        bool inPitchRange = pitch >= minPitch && pitch <= maxPitch;

        _objectRenderer.enabled = inYawRange && inPitchRange;
    }

    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
