using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 3f;
    // public float jumpForce = 3f;
    [SerializeField] float rotationSpeed = 100f; // 회전 속도 (원하는 대로 조절 가능)
    private Rigidbody rb;

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // private void Jump()
    // {
    //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    // }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed *Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed *Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed *Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed *Time.deltaTime;
        }
        
        // if (Input.GetButtonDown("Jump"))
        // {
        //     Jump();
        // }
        if (Input.GetKey(KeyCode.R))
        {
            // Vector3.up은 (0, 1, 0) 즉, Y축을 기준축으로 회전합니다.
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        
    }

    

}
