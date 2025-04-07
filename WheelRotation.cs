using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform frontWheel;
    public Transform rearWheel;

    public float rotationSpeed = 500f;
    public float maxSteeringAngle = 30f;

    private float frontWheelRotation = 0f;
    private float rearWheelRotation = 0f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float steeringInput = Input.GetAxis("Horizontal");

        float rotationAmount = moveInput * rotationSpeed * Time.deltaTime;
        float steeringAngle = maxSteeringAngle * steeringInput;

        frontWheelRotation += rotationAmount;
        rearWheelRotation += rotationAmount;

        frontWheel.localRotation = Quaternion.Euler(frontWheelRotation, steeringAngle, 0);
        rearWheel.localRotation = Quaternion.Euler(rearWheelRotation, 0, 0);
    }
}
