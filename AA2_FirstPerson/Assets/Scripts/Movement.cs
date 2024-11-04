using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float air = 5f;         
    public float jumpForce = 5f;          
    public LayerMask groundLayer;         
    public float raycastLength = 1.1f;    

    private Rigidbody rb;                 
    private bool isGrounded;              

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGround();

        if (isGrounded)
        {
            MovePlayer();
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        if (!isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 moveDirection = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * air;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime * air) ;
        }
    }

    void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(transform.position, Vector3.down * raycastLength, Color.red);
        if (Physics.Raycast(ray, out hit, raycastLength, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized;
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
