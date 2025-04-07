using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;              // Viteza de mișcare
    public float jumpHeight = 2f;             // Înălțimea săriturii
    public float gravity = -9.8f;             // Gravitația
    public float rotationSpeed = 0.5f;        // Viteza de rotație extrem de mică pentru o rotație foarte lentă

    private CharacterController controller;   // Referința la CharacterController
    private Vector3 velocity;                 // Viteza la care se mișcă jucătorul
    private bool isGrounded;                  // Verifică dacă jucătorul stă pe sol

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Preia componenta CharacterController
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Input pentru mișcare
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Mișcarea jucătorului
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Dacă este pe sol și se mișcă în jos, aplică o valoare mică pentru a preveni căderea continuă
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Săritură
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplică gravitația
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotește jucătorul doar când există input
        if (move != Vector3.zero)
        {
            // Direcția în care jucătorul vrea să se îndrepte
            Quaternion targetRotation = Quaternion.LookRotation(move);

            // Folosim RotateTowards pentru a face rotația extrem de lentă
            // Am ajustat mult viteza de rotație pentru o mișcare extrem de lentă
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 0.2f);
        }
    }
}
