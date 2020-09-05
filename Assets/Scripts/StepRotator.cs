using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StepRotator : Singleton<StepRotator>, IDragHandler
{
    [SerializeField] private GameObject step;
    [SerializeField] private GameObject pivot;
    [SerializeField] private Vector2 rotationSensitivity;
    [SerializeField] private Vector2 xRotationLimits;
    [SerializeField] private Vector2 zRotationLimits;

    private Rigidbody pivotBody;

    public bool IsRotationEnabled { get; set; } = true;

    private void Start()
    {
        pivotBody = pivot.GetComponent<Rigidbody>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsRotationEnabled) return;
        RotateStep(eventData.delta);

        pivotBody.AddForce(new Vector3(eventData.delta.x, 0, eventData.delta.y) * 0.03f, ForceMode.Impulse);
    }

    private void RotateStep(Vector2 delta)
    {
        step.transform.RotateAround(pivot.transform.position, Vector3.back, delta.x * rotationSensitivity.x);
        step.transform.RotateAround(pivot.transform.position, Vector3.right, delta.y * rotationSensitivity.y);
        ClampRotation(delta);
    }

    private void ClampRotation(Vector2 delta)
    {
        ClampRotationOnAxis(delta.x, Vector3.back, zRotationLimits, rotationSensitivity.x);
        ClampRotationOnAxis(delta.y, Vector3.right, xRotationLimits, rotationSensitivity.y);
    }

    private void ClampRotationOnAxis(float angleDelta, Vector3 axis, Vector2 limits, float sensitivity)
    {
        Vector3 stepUpProjection = step.transform.up;
        stepUpProjection.x *= 1 - Mathf.Abs(axis.x);
        stepUpProjection.y *= 1 - Mathf.Abs(axis.y);
        stepUpProjection.z *= 1 - Mathf.Abs(axis.z);
        float angle = Vector3.Angle(stepUpProjection, Vector3.up);
        if (angle <= limits.x || angle >= limits.y)
        {
            step.transform.RotateAround(pivot.transform.position, axis, -angleDelta * sensitivity);
        }
    }

    public void SetNewStep(GameObject newStep)
    {
        step = newStep;
    }
}
